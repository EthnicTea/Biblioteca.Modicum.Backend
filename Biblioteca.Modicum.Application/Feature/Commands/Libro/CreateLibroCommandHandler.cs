using Biblioteca.Modicum.Application.Feature.Interfaces.Libro;
using Biblioteca.Modicum.Application.Models;
using MediatR;

namespace Biblioteca.Modicum.Application.Feature.Commands.Libro;
public class CreateLibroCommandHandler : IRequestHandler<CreateLibroCommand, ResponseData<bool>>
{
    private readonly ILibroRepositories _libroRepositories;

    public CreateLibroCommandHandler(ILibroRepositories libroRepositories)
    {
        _libroRepositories = libroRepositories;
    }

    public async Task<ResponseData<bool>> Handle(CreateLibroCommand request, CancellationToken cancellation)
    {
        return await _libroRepositories.CreateLibro(request);
    }
}
