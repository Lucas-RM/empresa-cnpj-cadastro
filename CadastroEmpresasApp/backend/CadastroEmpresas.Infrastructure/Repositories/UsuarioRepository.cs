using CadastroEmpresas.Domain.Entities;
using CadastroEmpresas.Domain.Interfaces;
using CadastroEmpresas.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CadastroEmpresas.Infrastructure.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly AppDbContext _contexto;

        public UsuarioRepository(AppDbContext contexto)
        {
            _contexto = contexto ?? throw new ArgumentNullException(nameof(contexto));
        }
        public async Task<Usuario> GetByEmailAsync(string email)
        {
            return await _contexto.Usuarios.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task AddAsync(Usuario usuario)
        {
            await _contexto.Usuarios.AddAsync(usuario);
            await _contexto.SaveChangesAsync();
        }
    }
}
