using CadastroEmpresas.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace CadastroEmpresas.Infrastructure.Data.Configuration
{
    public class UsuarioEmpresaConfiguration : IEntityTypeConfiguration<UsuarioEmpresa>
    {
        public void Configure(EntityTypeBuilder<UsuarioEmpresa> builder)
        {
            builder.ToTable("UsuarioEmpresas");

            builder.HasKey(ue => new { ue.UsuarioId, ue.EmpresaId });

            builder.HasOne(ue => ue.Usuario)
                   .WithMany(u => u.UsuarioEmpresas)
                   .HasForeignKey(ue => ue.UsuarioId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(ue => ue.Empresa)
                   .WithMany(e => e.UsuarioEmpresas)
                   .HasForeignKey(ue => ue.EmpresaId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
