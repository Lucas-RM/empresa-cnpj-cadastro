using System.ComponentModel.DataAnnotations;

namespace CadastroEmpresas.API.DTOs
{
    public class EmpresaCadastroDto
    {
        [Required(ErrorMessage = "O cnpj é obrigatório.")]
        [MaxLength(18, ErrorMessage = "O cnpj deve ter no máximo 18 caracteres.")]
        public string Cnpj { get; set; } = string.Empty;
    }
}
