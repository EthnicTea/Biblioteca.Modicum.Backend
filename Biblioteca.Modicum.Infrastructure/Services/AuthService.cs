using Biblioteca.Modicum.Application.Configurations;
using Biblioteca.Modicum.Application.Feature.Commands.Auth;
using Biblioteca.Modicum.Application.Feature.Interfaces.Identity;
using Biblioteca.Modicum.Application.Models;
using Biblioteca.Modicum.Application.Models.Responses;
using Biblioteca.Modicum.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Biblioteca.Modicum.Infrastructure.Services;

public class AuthService : IAuthService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly JwtSettings _jwtSettings;

    public AuthService(
        UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager,
        IOptions<JwtSettings> jwtSettings)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _jwtSettings = jwtSettings.Value;
    }

    public async Task<ResponseData<AuthResponse>> LoginAsync(LoginCommand request)
    {
        // 1. Buscar usuario por email
        var user = await _userManager.FindByEmailAsync(request.Email);
        if (user == null)
        {
            return new ResponseData<AuthResponse>
            {
                Exitoso = false,
                Descripcion = "Credenciales incorrectas.",
                Resultado = null
            };
        }

        // 2. Validar contraseña
        var resultado = await _signInManager.CheckPasswordSignInAsync(user, request.Password, lockoutOnFailure: false);
        if (!resultado.Succeeded)
        {
            return new ResponseData<AuthResponse>
            {
                Exitoso = false,
                Descripcion = "Credenciales incorrectas.",
                Resultado = null
            };
        }

        // 3. Generar token JWT
        var token = await GenerateTokenAsync(user);

        return new ResponseData<AuthResponse>
        {
            Exitoso = true,
            Descripcion = "Inicio de sesión exitoso.",
            Resultado = new AuthResponse
            {
                Id = user.Id,
                UserName = user.UserName ?? string.Empty,
                Email = user.Email ?? string.Empty,
                Token = token
            }
        };
    }

    private async Task<string> GenerateTokenAsync(ApplicationUser user)
    {
        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub, user.Id),
            new(JwtRegisteredClaimNames.Email, user.Email ?? string.Empty),
            new(ClaimTypes.Name, user.UserName ?? string.Empty),
            new("Nombre", user.Nombre)
        };

        // Agregar roles como claims
        var roles = await _userManager.GetRolesAsync(user);
        foreach (var role in roles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role));
        }

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddMinutes(_jwtSettings.DurationInMinutes),
            Issuer = _jwtSettings.Issuer,
            Audience = _jwtSettings.Audience,
            SigningCredentials = creds
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);
    }

    public async Task<ResponseData<AuthResponse>> RegisterAsync(RegisterUserCommand request)
    {
        // 1. Verificar si el email o username ya existen
        var existingUser = await _userManager.FindByEmailAsync(request.Email);
        if (existingUser != null)
        {
            return new ResponseData<AuthResponse>
            {
                Exitoso = false,
                Descripcion = "El correo ya se encuentra registrado.",
                Resultado = null
            };
        }

        // 2. Instanciar el ApplicationUser con sus propiedades
        var user = new ApplicationUser
        {
            Nombre = request.Nombre,
            UserName = request.UserName,
            Email = request.Email
        };

        // 3. Crear el usuario (Identity hashea la clave automáticamente)
        var result = await _userManager.CreateAsync(user, request.Password);

        if (!result.Succeeded)
        {
            var errores = string.Join(" | ", result.Errors.Select(e => e.Description));
            return new ResponseData<AuthResponse>
            {
                Exitoso = false,
                Descripcion = $"No se pudo crear el usuario: {errores}",
                Resultado = null
            };
        }

        // 4. Generar el token de una vez para dejarlo logueado
        var token = await GenerateTokenAsync(user);

        return new ResponseData<AuthResponse>
        {
            Exitoso = true,
            Descripcion = "Usuario registrado exitosamente.",
            Resultado = new AuthResponse
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                Token = token
            }
        };
    }
}