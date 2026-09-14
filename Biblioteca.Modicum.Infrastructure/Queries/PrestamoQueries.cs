using System.Data;
using Biblioteca.Modicum.Application.Feature.Interfaces.Prestamo;
using Biblioteca.Modicum.Application.Models;
using Biblioteca.Modicum.Application.Models.Responses;
using Dapper;

namespace Biblioteca.Modicum.Infrastructure.Queries;

public class PrestamoQueries : IPrestamoQueries
{
    private readonly IDbConnection _dbConnection;

    public PrestamoQueries(IDbConnection dbConnection)
    {
        _dbConnection = dbConnection;
    }

    public async Task<ResponseData<IEnumerable<ListaPrestamoResponse>>> GetListaPrestamos(CancellationToken cancellationToken = default)
    {
        try
        {
            const string sql = @"
                SELECT 
                    p.id AS IdPrestamo,
                    p.id_libro AS IdLibro,
                    l.titulo AS TituloLibro,
                    p.id_usuario AS IdUsuario,
                    COALESCE(u.""Nombre"", u.""UserName"") AS NombreUsuario,
                    u.""Email"" AS EmailUsuario,
                    p.fecha_prestamo AS FechaPrestamo,
                    p.fecha_devolucion_esperada AS FechaDevolucionEsperada,
                    p.fecha_devolucion_real AS FechaDevolucionReal,
                    p.estado_prestamo AS EstadoPrestamo,
                    p.observaciones AS Observaciones
                FROM prestamos p
                INNER JOIN libro l ON l.id = p.id_libro
                INNER JOIN ""AspNetUsers"" u ON u.""Id"" = p.id_usuario
                ORDER BY p.fecha_prestamo DESC";

            var lista = await _dbConnection.QueryAsync<ListaPrestamoResponse>(sql);

            return new ResponseData<IEnumerable<ListaPrestamoResponse>>
            {
                Exitoso = true,
                Resultado = lista,
                Descripcion = "Lista de préstamos obtenida con éxito."
            };
        }
        catch (Exception ex)
        {
            var msg = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
            return new ResponseData<IEnumerable<ListaPrestamoResponse>>
            {
                Exitoso = false,
                Resultado = Enumerable.Empty<ListaPrestamoResponse>(),
                Descripcion = $"Error al consultar préstamos: {msg}"
            };
        }
    }
}