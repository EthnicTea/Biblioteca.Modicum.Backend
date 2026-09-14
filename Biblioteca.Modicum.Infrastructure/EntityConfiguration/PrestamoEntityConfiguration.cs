using Biblioteca.Modicum.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Biblioteca.Modicum.Infrastructure.EntityConfiguration;
public class PrestamoEntityConfiguration : IEntityTypeConfiguration<Prestamo>
{
    public void Configure(EntityTypeBuilder<Prestamo> builder)
    {
        builder.ToTable("prestamos");

        builder.HasKey(p => p.IdPrestamo);

        builder.Property(b => b.IdPrestamo)
            .HasColumnName("id")
            .HasColumnType("integer")
            .IsRequired();

        builder.Property(p => p.IdLibro)
            .HasColumnName("id_libro")
            .IsRequired();

        builder.Property(p => p.IdUsuario)
            .HasColumnName("id_usuario")
            .HasMaxLength(450)
            .IsRequired();

        builder.Property(p => p.FechaPrestamo)
            .HasColumnName("fecha_prestamo")
            .HasColumnType("timestamp with time zone")
            .IsRequired();

        builder.Property(p => p.FechaDevolucionEsperada)
            .HasColumnName("fecha_devolucion_esperada")
            .HasColumnType("timestamp with time zone")
            .IsRequired();

        builder.Property(p => p.FechaDevolucionReal)
            .HasColumnName("fecha_devolucion_real")
            .HasColumnType("timestamp with time zone");

        builder.Property(p => p.EstadoPrestamo)
            .HasColumnName("estado_prestamo")
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(p => p.Observaciones)
            .HasColumnName("observaciones")
            .HasMaxLength(500);

        builder.HasOne(p => p.Libro)
            .WithMany(l => l.Prestamos)
            .HasForeignKey(p => p.IdLibro)
            .HasConstraintName("fk_prestamos_libros_id_libro")
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(p => p.Usuario)
            .WithMany(u => u.Prestamos)
            .HasForeignKey(p => p.IdUsuario)
            .HasConstraintName("fk_prestamos_aspnetusers_id_usuario")
            .OnDelete(DeleteBehavior.Restrict);
    }
}