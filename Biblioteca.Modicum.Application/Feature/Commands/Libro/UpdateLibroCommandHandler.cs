using Biblioteca.Modicum.Application.Feature.Interfaces.Libro;
using Biblioteca.Modicum.Application.Models;
using MediatR;

namespace Biblioteca.Modicum.Application.Feature.Commands.Libro;

public class UpdateLibroCommandHandler : IRequestHandler<UpdateLibroCommand, ResponseData<bool>>
{
    private readonly ILibroRepositories _libroRepositories;

    public UpdateLibroCommandHandler(ILibroRepositories libroRepositories)
    {
        _libroRepositories = libroRepositories;
    }

    public async Task<ResponseData<bool>> Handle(UpdateLibroCommand request, CancellationToken cancellationToken)
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
        return await _libroRepositories.UpdateLibro(request, cancellationToken);
    }
}