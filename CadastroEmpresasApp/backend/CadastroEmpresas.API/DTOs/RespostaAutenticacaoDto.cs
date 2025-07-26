namespace CadastroEmpresas.API.DTOs
{
    public class RespostaAutenticacaoDto
    {
        public string Token { get; set; } = default!;
        public string Nome { get; set; } = default!;
        public string Email { get; set; } = default!;
    }
}
