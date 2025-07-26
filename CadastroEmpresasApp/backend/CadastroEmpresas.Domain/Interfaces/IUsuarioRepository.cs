using CadastroEmpresas.Domain.Entities;

namespace CadastroEmpresas.Domain.Interfaces
{
    public interface IUsuarioRepository
    {
        Task<Usuario> GetByEmailAsync(string email);
        Task AddAsync(Usuario user);
    }
}
