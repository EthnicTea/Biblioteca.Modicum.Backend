using Biblioteca.Modicum.Domain.Entities;
using Biblioteca.Modicum.Infrastructure.EntityConfiguration;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Biblioteca.Modicum.Infrastructure;

public partial class ModicumDbContext : IdentityDbContext<ApplicationUser>
{
    public ModicumDbContext(DbContextOptions<ModicumDbContext> options) : base(options)
    {
        this.ChangeTracker.LazyLoadingEnabled = false;
    }

    public DbSet<Libro> Libros { get; set; }
    public DbSet<Prestamo> Prestamos { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new LibroEntityConfiguration());
        modelBuilder.ApplyConfiguration(new PrestamoEntityConfiguration());
    }
}