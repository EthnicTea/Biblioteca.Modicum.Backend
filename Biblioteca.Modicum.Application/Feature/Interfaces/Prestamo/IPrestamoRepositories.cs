using Biblioteca.Modicum.Application.Feature.Commands.Prestamo;
using Biblioteca.Modicum.Application.Models;

namespace Biblioteca.Modicum.Application.Feature.Interfaces.Prestamo;
public interface IPrestamoRepositories
{
    Task<ResponseData<bool>> CreatePrestamo(CreatePrestamoCommand request, CancellationToken cancellationToken = default);
}
