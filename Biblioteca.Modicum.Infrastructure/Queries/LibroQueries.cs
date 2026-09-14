using Biblioteca.Modicum.Application.Feature.Interfaces.Libro;
using Biblioteca.Modicum.Application.Models;
using Biblioteca.Modicum.Application.Models.Responses;
using Dapper;
using System.Data;

namespace Biblioteca.Modicum.Infrastructure.Queries;
public class LibroQueries : ILibroQueries
{
    private readonly IDbConnection _dbConnection;

    public LibroQueries(IDbConnection dbConnection)
    {
        _dbConnection = dbConnection;
    }

    public async Task<ResponseData<IEnumerable<LibroResponse>>> GetListaLibros()
    {
        try
        {
            var builder = new SqlBuilder();

            var template = builder.AddTemplate(@"
                SELECT 
                    id              AS IdLibro, 
                    titulo          AS Titulo,
                    calidad         AS Calidad, 
                    estado          AS Estado,
                    autor           AS Autor
                FROM libro
                WHERE activo = true 
                ORDER BY titulo ASC");
            // Filtros
            //
            //
            var resultado = (await _dbConnection.QueryAsync<LibroResponse>(template.RawSql, template.Parameters)).ToList();

            if (resultado.Any())
            {
                return new ResponseData<IEnumerable<LibroResponse>>
                {
                    Descripcion = "Lista de libros obtenida con éxito.",
                    Resultado = resultado,
                    Exitoso = true
                };
            }

            return new ResponseData<IEnumerable<LibroResponse>>
            {
                Descripcion = "No se encontraron libros.",
                Resultado = resultado,
                Exitoso = true
            };
        }

        catch (Exception ex)
        {
            return new ResponseData<IEnumerable<LibroResponse>>
            {
                Descripcion = ex.Message,
                Resultado = null,
                Exitoso = false
            };
        }
    }
}
