using Biblioteca.Modicum.Application.Feature.Interfaces;
using Biblioteca.Modicum.Application.Feature.Interfaces.Identity;
using Biblioteca.Modicum.Application.Models;
using Biblioteca.Modicum.Application.Models.Responses;
using MediatR;

namespace Biblioteca.Modicum.Application.Feature.Commands.Auth;

public class RegisterUserCommand : IRequest<ResponseData<AuthResponse>>
{
    public string Nombre { get; set; } = string.Empty;
    public string UserName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}

public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, ResponseData<AuthResponse>>
{
    private readonly IAuthService _authService;

    public RegisterUserCommandHandler(IAuthService authService)
    {
        _authService = authService;
    }

    public async Task<ResponseData<AuthResponse>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(request.Email) || string.IsNullOrWhiteSpace(request.Password))
        {
            return new ResponseData<AuthResponse>
            {
                Exitoso = false,
                Descripcion = "El correo y la contraseña son obligatorios.",
                Resultado = null
            };
        }

        return await _authService.RegisterAsync(request);
    }
}