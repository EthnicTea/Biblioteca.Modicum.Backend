namespace Biblioteca.Modicum.Domain.Entities;

public class Prestamo
{
    public int IdPrestamo { get; set; }
    public int IdLibro { get; set; }
    public Libro Libro { get; set; } = null!;
    public string IdUsuario { get; set; } = string.Empty;
    public ApplicationUser Usuario { get; set; } = null!;
    public DateTime FechaPrestamo { get; set; }
    public DateTime FechaDevolucionEsperada { get; set; }
    public DateTime? FechaDevolucionReal { get; set; }
    public string EstadoPrestamo { get; set; } = "Activo";
    public string? Observaciones { get; set; }

    // public bool Activo { get; set; }
}