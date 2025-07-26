using System.ComponentModel.DataAnnotations;

namespace CadastroEmpresas.API.DTOs
{
    public class UsuarioLoginDto
    {
        [Required(ErrorMessage = "O e-mail é obrigatório.")]
        [EmailAddress(ErrorMessage = "Formato de e-mail inválido.")]
        public required string Email { get; set; }

        [Required(ErrorMessage = "A senha é obrigatória.")]
        public required string Senha { get; set; }
    }
}
