using Biblioteca.Modicum.Application.Feature.Interfaces.Libro;
using Biblioteca.Modicum.Application.Models;
using MediatR;

namespace Biblioteca.Modicum.Application.Feature.Commands.Libro;
public class ReactiveLibroCommandHandler : IRequestHandler<ReactiveLibroCommand, ResponseData<bool>>
{
    private readonly ILibroRepositories _libroRepositories;

    public ReactiveLibroCommandHandler(ILibroRepositories libroRepositories)
    {
        _libroRepositories = libroRepositories;
    }

    public async Task<ResponseData<bool>> Handle(ReactiveLibroCommand request, CancellationToken cancellationToken = default)
    {
        if (request.IdLibro <= 0)
        {
            return new ResponseData<bool>
            {
                Exitoso = false,
                Resultado = false,
                Descripcion = "El Id del libro debe ser mayor a 0."
            };
        }
        return await _libroRepositories.ReactiveLibro(request, cancellationToken);
    }
}
