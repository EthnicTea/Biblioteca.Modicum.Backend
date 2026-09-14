using Biblioteca.Modicum.Application.Models;
using Biblioteca.Modicum.Application.Models.Responses;

namespace Biblioteca.Modicum.Application.Feature.Interfaces.Prestamo;
public interface IPrestamoQueries
{
    Task<ResponseData<IEnumerable<ListaPrestamoResponse>>> GetListaPrestamos(CancellationToken cancellationToken = default);
}
