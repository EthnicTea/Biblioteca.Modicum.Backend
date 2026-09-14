using Biblioteca.Modicum.Application.Feature.Commands.Auth;
using Biblioteca.Modicum.Application.Models;
using Biblioteca.Modicum.Application.Models.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Biblioteca.Modicum.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IMediator _mediator;

    public AuthController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("Login")]
    public async Task<ResponseData<AuthResponse>> Login([FromBody] LoginCommand request)
    {
        return await _mediator.Send(request);
    }

    [HttpPost("Register")]
    public async Task<ResponseData<AuthResponse>> Register([FromBody] RegisterUserCommand request)
    {
        return await _mediator.Send(request);
    }
}