using CadastroEmpresas.Domain.Entities;
using CadastroEmpresas.Domain.Interfaces;
using CadastroEmpresas.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CadastroEmpresas.Infrastructure.Repositories
{
    public class EmpresaRepository : IEmpresaRepository
    {
        private readonly AppDbContext _contexto;

        public EmpresaRepository(AppDbContext contexto)
        {
            _contexto = contexto;
        }

        public async Task<Empresa?> BuscarPorCnpjAsync(string cnpj)
        {
            return await _contexto.Empresas
                .Include(e => e.Endereco)
                .FirstOrDefaultAsync(e => e.Cnpj == cnpj);
        }

        public async Task CadastrarEmpresaAsync(Empresa empresa)
        {
            await _contexto.Empresas.AddAsync(empresa);
        }

        public async Task VincularEmpresaAoUsuarioAsync(Guid usuarioId, Guid empresaId)
        {
            var existeVinculo = await _contexto.UsuarioEmpresas
                .AnyAsync(ue => ue.UsuarioId == usuarioId && ue.EmpresaId == empresaId);

            if (!existeVinculo)
            {
                var usuarioEmpresa = new UsuarioEmpresa
                {
                    UsuarioId = usuarioId,
                    EmpresaId = empresaId
                };

                await _contexto.UsuarioEmpresas.AddAsync(usuarioEmpresa);
            }
        }

        public async Task<bool> UsuarioJaCadastrouEmpresa(Guid usuarioId, Guid empresaId)
        {
            return await _contexto.UsuarioEmpresas
                .AnyAsync(ue => ue.UsuarioId == usuarioId && ue.EmpresaId == empresaId);
        }

        public async Task<List<Empresa>> ListarEmpresasDoUsuarioAsync(Guid usuarioId, int pagina, int tamanho)
        {
            return await _contexto.UsuarioEmpresas
                .Where(ue => ue.UsuarioId == usuarioId)
                .Include(ue => ue.Empresa)
                    .ThenInclude(e => e.Endereco)
                .Select(ue => ue.Empresa)
                .Skip((pagina - 1) * tamanho)
                .Take(tamanho)
                .ToListAsync();
        }

        public async Task<int> ContarEmpresasDoUsuarioAsync(Guid usuarioId)
        {
            return await _contexto.UsuarioEmpresas
                .CountAsync(ue => ue.UsuarioId == usuarioId);
        }

        public async Task SalvarAsync()
        {
            await _contexto.SaveChangesAsync();
        }
    }
}
