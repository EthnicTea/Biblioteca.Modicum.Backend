using Biblioteca.Modicum.Application.Models;
using MediatR;

namespace Biblioteca.Modicum.Application.Feature.Commands.Libro;
public class CreateLibroCommand : IRequest<ResponseData<bool>>
{
    public string Titulo { get; set; } = string.Empty;
    public string Calidad { get; set; } = string.Empty;
    public string Estado { get; set; } = string.Empty;
    public string Autor { get; set; } = string.Empty;
}

