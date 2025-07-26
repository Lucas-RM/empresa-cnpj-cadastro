using System.ComponentModel.DataAnnotations.Schema;

namespace CadastroEmpresas.Domain.Entities
{
    [Table("EnderecosEmpresa")]
    public class EnderecoEmpresa
    {
        public Guid EnderecoId { get; set; }
        public string Logradouro { get; set; } = string.Empty;
        public string Numero { get; set; } = string.Empty;
        public string Complemento { get; set; } = string.Empty;
        public string Bairro { get; set; } = string.Empty;
        public string Municipio { get; set; } = string.Empty;
        public string UF { get; set; } = string.Empty;
        public string CEP { get; set; } = string.Empty;

        public Guid EmpresaId { get; set; }
        public Empresa Empresa { get; set; } = null!;
    }
}
