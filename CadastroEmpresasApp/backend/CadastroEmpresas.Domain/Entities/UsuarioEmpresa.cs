using System.ComponentModel.DataAnnotations.Schema;

namespace CadastroEmpresas.Domain.Entities
{
    [Table("UsuarioEmpresas")]
    public class UsuarioEmpresa
    {
        public Guid UsuarioId { get; set; }
        public Usuario Usuario { get; set; } = null!;

        public Guid EmpresaId { get; set; }
        public Empresa Empresa { get; set; } = null!;

        public DateTime CadastradaEm { get; set; } = DateTime.UtcNow;
    }
}
