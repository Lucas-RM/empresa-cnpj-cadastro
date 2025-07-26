using CadastroEmpresas.Domain.Entities;
using CadastroEmpresas.Infrastructure.Data.Configuration;
using Microsoft.EntityFrameworkCore;

namespace CadastroEmpresas.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> opcao) : base(opcao)
        {
        }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Empresa> Empresas { get; set; }
        public DbSet<EnderecoEmpresa> EnderecosEmpresa { get; set; }
        public DbSet<UsuarioEmpresa> UsuarioEmpresas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UsuarioConfiguration());
            modelBuilder.ApplyConfiguration(new EmpresaConfiguration());
            modelBuilder.ApplyConfiguration(new EnderecoConfiguration());
            modelBuilder.ApplyConfiguration(new UsuarioEmpresaConfiguration());
            base.OnModelCreating(modelBuilder);
        }
    }
}
