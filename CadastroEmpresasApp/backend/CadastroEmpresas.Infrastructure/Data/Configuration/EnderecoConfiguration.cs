using CadastroEmpresas.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace CadastroEmpresas.Infrastructure.Data.Configuration
{
    public class EnderecoConfiguration : IEntityTypeConfiguration<EnderecoEmpresa>
    {
        public void Configure(EntityTypeBuilder<EnderecoEmpresa> builder)
        {
            builder.ToTable("EnderecosEmpresa");

            builder.HasKey(e => e.EnderecoId);

            builder.Property(e => e.Logradouro)
                   .IsRequired()
                   .HasMaxLength(150);

            builder.Property(e => e.Numero)
                   .IsRequired()
                   .HasMaxLength(20);

            builder.Property(e => e.Complemento)
                   .HasMaxLength(100);

            builder.Property(e => e.Bairro)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(e => e.Municipio)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(e => e.UF)
                   .IsRequired()
                   .HasMaxLength(2);

            builder.Property(e => e.CEP)
                   .IsRequired()
                   .HasMaxLength(10);

            builder.HasOne(e => e.Empresa)
                   .WithOne(emp => emp.Endereco)
                   .HasForeignKey<EnderecoEmpresa>(e => e.EmpresaId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
