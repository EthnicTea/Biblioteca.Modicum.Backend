using Biblioteca.Modicum.Application.Feature.Commands.Prestamo;
using Biblioteca.Modicum.Application.Feature.Interfaces.Prestamo;
using Biblioteca.Modicum.Application.Models;
using MediatR;

public class CreatePrestamoCommandHandler : IRequestHandler<CreatePrestamoCommand, ResponseData<bool>>
{
    private readonly IPrestamoRepositories _repositories;

    public CreatePrestamoCommandHandler(IPrestamoRepositories repositories)
    {
        _repositories = repositories;
    }

    public async Task<ResponseData<bool>> Handle(CreatePrestamoCommand request, CancellationToken cancellationToken)
    {
        return await _repositories.CreatePrestamo(request, cancellationToken);
    }
}