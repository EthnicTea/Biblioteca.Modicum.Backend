using System.ComponentModel.DataAnnotations;

namespace Biblioteca.Modicum.Domain.Entities;
public class Libro
{
    public int IdLibro { get; set; }
    public string Titulo { get; set; } = string.Empty;
    public string Calidad { get; set; } = string.Empty;
    public string Estado { get; set; } = string.Empty;
    public string Autor { get; set; } = string.Empty;
    public ICollection<Prestamo> Prestamos { get; set; } = new List<Prestamo>();
    public bool Activo { get; set; } = true;

}
