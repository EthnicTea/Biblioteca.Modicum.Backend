namespace Biblioteca.Modicum.Application.Models.Responses;
public class ListaPrestamoResponse
{
    public int IdPrestamo { get; set; }
    public int IdLibro { get; set; }
    public string TituloLibro { get; set; } = string.Empty;
    public string IdUsuario { get; set; } = string.Empty;
    public string NombreUsuario { get; set; } = string.Empty;
    public string EmailUsuario { get; set; } = string.Empty;
    public DateTime FechaPrestamo { get; set; }
    public DateTime FechaDevolucionEsperada { get; set; }
    public DateTime? FechaDevolucionReal { get; set; }
    public string EstadoPrestamo { get; set; } = string.Empty;
    public string? Observaciones { get; set; }
}
