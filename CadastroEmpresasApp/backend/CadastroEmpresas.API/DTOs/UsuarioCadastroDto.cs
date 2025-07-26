using System.ComponentModel.DataAnnotations;

namespace CadastroEmpresas.API.DTOs
{
    public class UsuarioCadastroDto
    {
        [Required(ErrorMessage = "O nome é obrigatório.")]
        [MaxLength(100, ErrorMessage = "O nome deve ter no máximo 100 caracteres.")]
        public string Nome { get; set; } = default!;

        [Required(ErrorMessage = "O e-mail é obrigatório.")]
        [EmailAddress(ErrorMessage = "Formato de e-mail inválido.")]
        [MaxLength(150, ErrorMessage = "O e-mail deve ter no máximo 150 caracteres.")]
        public string Email { get; set; } = default!;

        [Required(ErrorMessage = "A senha é obrigatória.")]
        [MinLength(8, ErrorMessage = "A senha deve ter pelo menos 8 caracteres.")]
        public string Senha { get; set; } = default!;
    }
}
