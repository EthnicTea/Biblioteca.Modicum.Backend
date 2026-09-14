using Biblioteca.Modicum.Application.Feature.Commands.Auth;
using Biblioteca.Modicum.Application.Models;
using Biblioteca.Modicum.Application.Models.Responses;

namespace Biblioteca.Modicum.Application.Feature.Interfaces.Identity;

public interface IAuthService
{
    Task<ResponseData<AuthResponse>> LoginAsync(LoginCommand request);
    Task<ResponseData<AuthResponse>> RegisterAsync(RegisterUserCommand request);
}