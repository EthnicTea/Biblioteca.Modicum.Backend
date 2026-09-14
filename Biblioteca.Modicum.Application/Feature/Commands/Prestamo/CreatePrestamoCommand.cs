using Biblioteca.Modicum.Application.Models;
using MediatR;

namespace Biblioteca.Modicum.Application.Feature.Commands.Prestamo;

public class CreatePrestamoCommand : IRequest<ResponseData<bool>>
{
    public int IdLibro { get; set; }
    public string IdUsuario { get; set; } = string.Empty;
    public int DiasPrestamo { get; set; } = 7; // Por defecto 7 días
    public string? Observaciones { get; set; }
}