using CadastroEmpresas.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace CadastroEmpresas.Infrastructure.Data.Configuration
{
    public class UsuarioConfiguration : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.ToTable("Usuarios");

            builder.HasKey(u => u.Id);

            builder.Property(u => u.Nome)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(u => u.Email)
                   .IsRequired()
                   .HasMaxLength(150);

            builder.HasIndex(u => u.Email).IsUnique();

            builder.Property(u => u.SenhaHash)
                   .IsRequired();

            builder.Property(u => u.CreatedAt)
                   .IsRequired();

            // Relacionamento N:N via UsuarioEmpresas
            builder.HasMany(u => u.UsuarioEmpresas)
                   .WithOne(ue => ue.Usuario)
                   .HasForeignKey(ue => ue.UsuarioId);
        }
    }
}
