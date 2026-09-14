using Biblioteca.Modicum.Application.Feature.Commands.Libro;
using Biblioteca.Modicum.Application.Models;

namespace Biblioteca.Modicum.Application.Feature.Interfaces.Libro;
public interface ILibroRepositories
{
    Task<ResponseData<bool>> CreateLibro(CreateLibroCommand request, CancellationToken cancellationToken = default);
    Task<ResponseData<bool>> UpdateLibro(UpdateLibroCommand request, CancellationToken cancellationToken = default);
    Task<ResponseData<bool>> DeleteLibro(DeleteLibroCommand request, CancellationToken cancellationToken = default);
    Task<ResponseData<bool>> ReactiveLibro(ReactiveLibroCommand request, CancellationToken cancellationToken = default);
}
