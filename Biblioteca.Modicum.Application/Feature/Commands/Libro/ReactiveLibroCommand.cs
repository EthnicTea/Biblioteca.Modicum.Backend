using Biblioteca.Modicum.Application.Models;
using MediatR;

namespace Biblioteca.Modicum.Application.Feature.Commands.Libro;
public class ReactiveLibroCommand : IRequest<ResponseData<bool>>
{
    public int IdLibro { get; set; }
}
