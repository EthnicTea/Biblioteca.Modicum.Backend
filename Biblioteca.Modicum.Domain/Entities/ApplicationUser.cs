using Microsoft.AspNetCore.Identity;

namespace Biblioteca.Modicum.Domain.Entities;
public class ApplicationUser : IdentityUser
{
    public string Nombre { get; set; } = string.Empty;
    public ICollection<Prestamo> Prestamos { get; set; } = new List<Prestamo>();
}
