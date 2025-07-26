namespace CadastroEmpresas.API.Models
{
    public class ConfiguracoesToken
    {
        public string ChaveSecreta { get; set; } = string.Empty;
        public string Emissor { get; set; } = string.Empty;
        public string Audiencia { get; set; } = string.Empty;
        public int ExpiracaoEmMinutos { get; set; }
    }
}
