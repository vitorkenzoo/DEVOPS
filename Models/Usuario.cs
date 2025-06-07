using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FuracaoAlerta.API.Models
{
    public class Usuario
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdUsuario { get; set; }

        [Required]
        [MaxLength(2000)]
        public string Nome { get; set; } = string.Empty;

        [Required]
        [MaxLength(2000)]
        public string Email { get; set; } = string.Empty;

        [Required]
        [MaxLength(2000)]
        public string Telefone { get; set; } = string.Empty;

        [Required]
        [MaxLength(2000)]
        public string Cpf { get; set; } = string.Empty;

        [Required]
        [MaxLength(2000)]
        public string Senha { get; set; } = string.Empty;

        public int? AbrigoId { get; set; }
        public Abrigo? Abrigo { get; set; }

        public int? EnderecoId { get; set; }
        public Endereco? Endereco { get; set; }

        public ICollection<UsuarioAlerta> UsuarioAlertas { get; set; } = new List<UsuarioAlerta>();

    }
}
