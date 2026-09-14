using Biblioteca.Modicum.Application.Feature.Commands.Prestamo;
using Biblioteca.Modicum.Application.Feature.Interfaces.Prestamo;
using Biblioteca.Modicum.Application.Models;
using Biblioteca.Modicum.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Biblioteca.Modicum.Infrastructure.Repositories;

public class PrestamoRepositories : IPrestamoRepositories
{
    private readonly ModicumDbContext _dbContext;

    public PrestamoRepositories(ModicumDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<ResponseData<bool>> CreatePrestamo(CreatePrestamoCommand request, CancellationToken cancellationToken = default)
    {
        try
        {
            // 1. Validar que el libro exista y esté activo
            var libro = await _dbContext.Libros
                .FirstOrDefaultAsync(l => l.IdLibro == request.IdLibro && l.Activo, cancellationToken);

            if (libro == null)
            {
                return new ResponseData<bool>
                {
                    Exitoso = false,
                    Resultado = false,
                    Descripcion = $"El libro con ID {request.IdLibro} no existe o no se encuentra activo."
                };
            }

            // 2. Validar que el usuario exista
            var usuarioExiste = await _dbContext.Users
                .AnyAsync(u => u.Id == request.IdUsuario, cancellationToken);

            if (!usuarioExiste)
            {
                return new ResponseData<bool>
                {
                    Exitoso = false,
                    Resultado = false,
                    Descripcion = $"El usuario con ID {request.IdUsuario} no existe."
                };
            }

            // 3. Validar que el libro NO tenga un préstamo activo
            var libroOcupado = await _dbContext.Prestamos
                .AnyAsync(p => p.IdLibro == request.IdLibro && p.EstadoPrestamo == "Activo", cancellationToken);

            if (libroOcupado)
            {
                return new ResponseData<bool>
                {
                    Exitoso = false,
                    Resultado = false,
                    Descripcion = $"El libro '{libro.Titulo}' ya cuenta con un préstamo activo vigente."
                };
            }

            // 4. Registrar préstamo
            var nuevoPrestamo = new Prestamo
            {
                IdLibro = request.IdLibro,
                IdUsuario = request.IdUsuario,
                FechaPrestamo = DateTime.UtcNow,
                FechaDevolucionEsperada = DateTime.UtcNow.AddDays(request.DiasPrestamo > 0 ? request.DiasPrestamo : 7),
                EstadoPrestamo = "Activo",
                Observaciones = request.Observaciones
            };

            await _dbContext.Prestamos.AddAsync(nuevoPrestamo, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return new ResponseData<bool>
            {
                Exitoso = true,
                Resultado = true,
                Descripcion = "Préstamo registrado exitosamente."
            };
        }
        catch (Exception ex)
        {
            var msg = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
            return new ResponseData<bool>
            {
                Exitoso = false,
                Resultado = false,
                Descripcion = $"Error al registrar préstamo: {msg}"
            };
        }
    }
}