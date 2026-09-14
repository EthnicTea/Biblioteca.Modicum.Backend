using Biblioteca.Modicum.Application.Feature.Commands.Libro;
using Biblioteca.Modicum.Application.Feature.Interfaces.Libro;
using Biblioteca.Modicum.Application.Models;
using Biblioteca.Modicum.Application.Models.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Biblioteca.Modicum.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LibroController : Controller
{
    private readonly IMediator _mediator;
    private readonly ILibroQueries _queries;

    public LibroController(IMediator mediator, ILibroQueries queries)
    {
        _mediator = mediator;
        _queries = queries;
    }

    [HttpGet("GetListaLibros")]
    public async Task<ResponseData<IEnumerable<LibroResponse>>> GetListaLibros()
    {
        return await _queries.GetListaLibros();
    }

    [HttpPost("CreateLibro")]
    public async Task<ResponseData<bool>> CreateLibro(CreateLibroCommand request, CancellationToken cancellationToken = default)
    {
        return await _mediator.Send(request);
    }

    [HttpPost("UpdateLibro")]
    public async Task<ResponseData<bool>> UpdateLibro(UpdateLibroCommand request, CancellationToken cancellationToken = default)
    {
        return await _mediator.Send(request);
    }

    [HttpPost("DeleteLibro")]
    public async Task<ResponseData<bool>> DeleteLibro(DeleteLibroCommand request, CancellationToken cancellationToken = default)
    {
        return await _mediator.Send(request);
    }

    [HttpPost("ReactiveLibro")]
    public async Task<ResponseData<bool>> ReactiveLibro(ReactiveLibroCommand request, CancellationToken cancellationToken = default)
    {
        return await _mediator.Send(request);
    }
}
