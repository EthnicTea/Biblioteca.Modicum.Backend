using System.ComponentModel.DataAnnotations;

namespace Biblioteca.Modicum.Application.Models.Responses;
public class LibroResponse
{
    public int IdLibro { get; set; }
    public string Titulo { get; set; } = string.Empty;
    public string Calidad { get; set; } = string.Empty;
    public string Estado { get; set; } = string.Empty;
    public string Autor { get; set; } = string.Empty;
}
