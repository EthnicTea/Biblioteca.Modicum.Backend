using Biblioteca.Modicum.Application.Feature.Commands.Libro;
using Biblioteca.Modicum.Application.Feature.Interfaces.Libro;
using Biblioteca.Modicum.Application.Models;
using Biblioteca.Modicum.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Biblioteca.Modicum.Infrastructure.Repositories;
public class LibroRepositories : ILibroRepositories
{
    private readonly ModicumDbContext _dbContext;
    public LibroRepositories(ModicumDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<ResponseData<bool>> CreateLibro(CreateLibroCommand request, CancellationToken cancellationToken = default)
    {
        try
        {
            var nuevoLibro = new Libro
            {
                Titulo = request.Titulo,
                Calidad = request.Calidad,
                Estado = request.Estado,
                Autor = request.Autor,
                Activo = true
            };

            await _dbContext.Libros.AddAsync(nuevoLibro);
            await _dbContext.SaveChangesAsync();

            return new ResponseData<bool>
            {
                Descripcion = "Libro registrado.",
                Exitoso = true,
                Resultado = true
            };
        }
        catch (Exception ex)
        {
            var mensajeReal = ex.InnerException != null ? ex.InnerException.Message : ex.Message;

            return new ResponseData<bool>
            {
                Descripcion = $"Error al registrar el libro: {mensajeReal}",
                Exitoso = false,
                Resultado = false
            };
        }
    }

    public async Task<ResponseData<bool>> UpdateLibro(UpdateLibroCommand request, CancellationToken cancellationToken = default)
    {
        try
        {
            var libroExistente = await _dbContext.Libros
                .FirstOrDefaultAsync(l => l.IdLibro == request.IdLibro && l.Activo, cancellationToken);
            
            if (libroExistente == null)
            {
                return new ResponseData<bool>
                {
                    Resultado = false,
                    Descripcion = $"No se encontró el libro con el ID {request.IdLibro}.",
                    Exitoso = false
                };
            }

            if (!string.IsNullOrWhiteSpace(request.Titulo))
            {
                libroExistente.Titulo = request.Titulo;
            }

            if (!string.IsNullOrWhiteSpace(request.Calidad))
            {
                libroExistente.Calidad = request.Calidad;
            }

            if (!string.IsNullOrWhiteSpace(request.Estado))
            {
                libroExistente.Estado = request.Estado;
            }

            if (!string.IsNullOrWhiteSpace(request.Autor))
            {
                libroExistente.Autor = request.Autor;
            }

            await _dbContext.SaveChangesAsync(cancellationToken);

            return new ResponseData<bool>
            {
                Resultado = true,
                Descripcion = "Libro Actualizado con nuevos datos",
                Exitoso = true
            };
        }
        catch (Exception ex)
        {
            var ms = ex.InnerException != null ? ex.InnerException.Message : ex.Message;

            return new ResponseData<bool>
            {
                Descripcion = $"Error al actualizar el libro: {ms}",
                Exitoso = false,
                Resultado = false
            };
        }
    }

    public async Task<ResponseData<bool>> DeleteLibro(DeleteLibroCommand request, CancellationToken cancellationToken = default)
    {
        try
        {
            var libroExistente = await _dbContext.Libros
                .FirstOrDefaultAsync(l => l.IdLibro == request.IdLibro && l.Activo, cancellationToken);
            
            if (libroExistente == null)
            {
                return new ResponseData<bool>
                {
                    Resultado = false,
                    Descripcion = $"El libro con el ID {request.IdLibro} no existe o no se encuentra.",
                    Exitoso = false
                };
            }

            if (!libroExistente.Activo)
            {
                return new ResponseData<bool>
                {
                    Resultado = false,
                    Descripcion = $"El libro con el ID {request.IdLibro} ya se encontraba eliminado.",
                    Exitoso = false
                };
            }

            libroExistente.Activo = false;

            await _dbContext.SaveChangesAsync(cancellationToken);

            return new ResponseData<bool>
            {
                Resultado = true,
                Descripcion = "Libro eliminado (Soft delete)",
                Exitoso = true
            };
        }
        catch (Exception ex)
        {
            var ms = ex.InnerException != null ? ex.InnerException.Message : ex.Message;

            return new ResponseData<bool>
            {
                Descripcion = $"Error al eliminar el libro: {ms}",
                Exitoso = false,
                Resultado = false
            };
        }
    }

    public async Task<ResponseData<bool>> ReactiveLibro(ReactiveLibroCommand request, CancellationToken cancellationToken = default)
    {
        try
        {
            var libroExistente = await _dbContext.Libros
                .FirstOrDefaultAsync(l => l.IdLibro == request.IdLibro, cancellationToken);

            if (libroExistente == null)
            {
                return new ResponseData<bool>
                {
                    Resultado = false,
                    Descripcion = $"El libro con el ID {request.IdLibro} no existe o no se encuentra.",
                    Exitoso = false
                };
            }

            if (libroExistente.Activo)
            {
                return new ResponseData<bool>
                {
                    Resultado = false,
                    Descripcion = $"El libro con el ID {request.IdLibro} ya se encontraba activo.",
                    Exitoso = false
                };
            }

            libroExistente.Activo = true;

            await _dbContext.SaveChangesAsync(cancellationToken);

            return new ResponseData<bool>
            {
                Resultado = true,
                Descripcion = "Libro reactivado",
                Exitoso = true
            };
        }
        catch (Exception ex)
        {
            var ms = ex.InnerException != null ? ex.InnerException.Message : ex.Message;

            return new ResponseData<bool>
            {
                Descripcion = $"Error al reactivar el libro: {ms}",
                Exitoso = false,
                Resultado = false
            };
        }
    }
}
