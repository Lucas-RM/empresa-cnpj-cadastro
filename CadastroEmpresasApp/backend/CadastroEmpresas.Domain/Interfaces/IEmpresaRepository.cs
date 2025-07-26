using CadastroEmpresas.Domain.Entities;

namespace CadastroEmpresas.Domain.Interfaces
{
    public interface IEmpresaRepository
    {
        Task<Empresa?> BuscarPorCnpjAsync(string cnpj);
        Task CadastrarEmpresaAsync(Empresa empresa);
        Task VincularEmpresaAoUsuarioAsync(Guid usuarioId, Guid empresaId);
        Task<bool> UsuarioJaCadastrouEmpresa(Guid usuarioId, Guid empresaId);
        Task<List<Empresa>> ListarEmpresasDoUsuarioAsync(Guid usuarioId, int pagina, int tamanho);
        Task<int> ContarEmpresasDoUsuarioAsync(Guid usuarioId);
        Task SalvarAsync();
    }
}
