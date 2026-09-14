using Biblioteca.Modicum.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Biblioteca.Modicum.Infrastructure.EntityConfiguration;
public class LibroEntityConfiguration : IEntityTypeConfiguration<Libro>
{
    public void Configure(EntityTypeBuilder<Libro> builder)
    {
        builder.ToTable("libro");

        builder.HasKey(m => m.IdLibro);

        builder.Property(b => b.IdLibro)
            .HasColumnName("id")
            .HasColumnType("integer")
            .IsRequired();

        builder.Property(b => b.Titulo)
            .HasColumnName("titulo")
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(b => b.Calidad)
            .HasColumnName("calidad")
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(b => b.Estado)
            .HasColumnName("estado")
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(b => b.Autor)
            .HasColumnName("autor")
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(b => b.Activo)
            .HasColumnName("activo")
            .HasDefaultValue(true)
            .IsRequired();
    }
}
