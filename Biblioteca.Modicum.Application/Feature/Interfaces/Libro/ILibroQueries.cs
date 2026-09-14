using Biblioteca.Modicum.Application.Models;
using Biblioteca.Modicum.Application.Models.Responses;

namespace Biblioteca.Modicum.Application.Feature.Interfaces.Libro;
public interface ILibroQueries
{
    Task<ResponseData<IEnumerable<LibroResponse>>> GetListaLibros();
}
