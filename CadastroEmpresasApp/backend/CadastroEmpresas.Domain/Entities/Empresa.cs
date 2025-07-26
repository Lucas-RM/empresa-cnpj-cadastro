using System.ComponentModel.DataAnnotations.Schema;

namespace CadastroEmpresas.Domain.Entities
{
    [Table("Empresas")]
    public class Empresa
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Cnpj { get; set; } = string.Empty;
        public string Nome { get; set; } = string.Empty;
        public string NomeFantasia { get; set; } = string.Empty;
        public string Situacao { get; set; } = string.Empty;
        public string Abertura { get; set; } = string.Empty;
        public string NaturezaJuridica { get; set; } = string.Empty;
        public string AtividadePrincipal { get; set; } = string.Empty;
        public EnderecoEmpresa Endereco { get; set; } = new();

        public ICollection<UsuarioEmpresa> UsuarioEmpresas { get; set; } = new List<UsuarioEmpresa>();
    }
}
