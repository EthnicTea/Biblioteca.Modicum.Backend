using Biblioteca.Modicum.Application.Feature.Commands.Prestamo;
using Biblioteca.Modicum.Application.Feature.Interfaces.Prestamo;
using Biblioteca.Modicum.Application.Models;
using Biblioteca.Modicum.Application.Models.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Biblioteca.Modicum.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PrestamoController : ControllerBase
{
    private readonly IPrestamoQueries _prestamoQueries;
    private readonly IMediator _mediator;

    public PrestamoController(IPrestamoQueries prestamoQueries, IMediator mediator)
    {
        _prestamoQueries = prestamoQueries;
        _mediator = mediator;
    }

    [HttpGet("GetListaPrestamos")]
    public async Task<ResponseData<IEnumerable<ListaPrestamoResponse>>> GetListaPrestamos(CancellationToken cancellationToken = default)
    {
        return await _prestamoQueries.GetListaPrestamos(cancellationToken);
    }

    [HttpGet("GetPrestamoById/{idPrestamo}")]
    public async Task<ResponseData<PrestamoResponse>> GetPrestamoById(int idPrestamo, CancellationToken cancellationToken = default)
    {
        return await _prestamoQueries.GetPrestamoById(idPrestamo, cancellationToken);
    }

    [HttpPost("CreatePrestamo")]
    public async Task<ResponseData<bool>> CreatePrestamo([FromBody] CreatePrestamoCommand request)
    {
        return await _mediator.Send(request);
    }
}