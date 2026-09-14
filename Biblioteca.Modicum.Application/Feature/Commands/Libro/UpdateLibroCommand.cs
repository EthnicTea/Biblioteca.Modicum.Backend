using Biblioteca.Modicum.Application.Models;
using MediatR;

namespace Biblioteca.Modicum.Application.Feature.Commands.Libro;
public class UpdateLibroCommand : IRequest<ResponseData<bool>>
{
    public int IdLibro { get; set; }
    public string? Titulo { get; set; }
    public string? Calidad { get; set; }
    public string? Estado { get; set; }
    public string? Autor { get; set; }
}
