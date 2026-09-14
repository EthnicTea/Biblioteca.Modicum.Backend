using Biblioteca.Modicum.Application.Feature.Interfaces.Identity;
using Biblioteca.Modicum.Application.Models;
using Biblioteca.Modicum.Application.Models.Responses;
using MediatR;

namespace Biblioteca.Modicum.Application.Feature.Commands.Auth;

public class LoginCommand : IRequest<ResponseData<AuthResponse>>
{
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}

public class LoginCommandHandler : IRequestHandler<LoginCommand, ResponseData<AuthResponse>>
{
    private readonly IAuthService _authService;

    public LoginCommandHandler(IAuthService authService)
    {
        _authService = authService;
    }

    public async Task<ResponseData<AuthResponse>> Handle(LoginCommand request, CancellationToken cancellationToken)
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

        return await _authService.LoginAsync(request);
    }
}