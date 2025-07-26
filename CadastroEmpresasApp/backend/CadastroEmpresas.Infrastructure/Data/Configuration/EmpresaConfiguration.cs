using CadastroEmpresas.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CadastroEmpresas.Infrastructure.Data.Configuration
{
    public class EmpresaConfiguration : IEntityTypeConfiguration<Empresa>
    {
        public void Configure(EntityTypeBuilder<Empresa> builder)
        {
            builder.ToTable("Empresas");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Cnpj)
                   .IsRequired()
                   .HasMaxLength(18);

            builder.Property(e => e.Nome)
                   .IsRequired()
                   .HasMaxLength(200);

            builder.Property(e => e.NomeFantasia)
                   .HasMaxLength(200);

            builder.Property(e => e.Situacao)
                   .HasMaxLength(50);

            builder.Property(e => e.Abertura)
                   .HasMaxLength(10);

            builder.Property(e => e.NaturezaJuridica)
                   .HasMaxLength(100);

            builder.Property(e => e.AtividadePrincipal)
                   .HasMaxLength(200);

            builder.HasOne(e => e.Endereco)
                   .WithOne(end => end.Empresa)
                   .HasForeignKey<EnderecoEmpresa>(end => end.EmpresaId)
                   .OnDelete(DeleteBehavior.Cascade);

            // Relacionamento N:N via UsuarioEmpresas
            builder.HasMany(e => e.UsuarioEmpresas)
                   .WithOne(ue => ue.Empresa)
                   .HasForeignKey(ue => ue.EmpresaId);
        }
    }
}
