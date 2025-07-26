namespace CadastroEmpresas.API.DTOs
{
    public class EmpresaListagemDto
    {
        public string Cnpj { get; set; } = string.Empty;
        public string Nome { get; set; } = string.Empty;
        public string NomeFantasia { get; set; } = string.Empty;
        public string Situacao { get; set; } = string.Empty;
        public string Abertura { get; set; } = string.Empty;
        public string AtividadePrincipal { get; set; } = string.Empty;
        public string Logradouro { get; set; } = string.Empty;
        public string Numero { get; set; } = string.Empty;
        public string Municipio { get; set; } = string.Empty;
        public string UF { get; set; } = string.Empty;
        public string CEP { get; set; } = string.Empty;
    }
}
