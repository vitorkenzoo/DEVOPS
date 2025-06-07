using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FuracaoAlerta.API.Models
{
    public class Endereco
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdEndereco { get; set; }

        [Required]
        [MaxLength(2000)]
        public string Rua { get; set; } = string.Empty;

        [Required]
        [MaxLength(2000)]
        public string Numero { get; set; } = string.Empty;

        [Required]
        [MaxLength(2000)]
        public string Bairro { get; set; } = string.Empty;

        [Required]
        [MaxLength(2000)]
        public string Cidade { get; set; } = string.Empty;

        [Required]
        [MaxLength(2000)]
        public string Estado { get; set; } = string.Empty;

        public ICollection<Usuario>? Usuarios { get; set; }
        public ICollection<Abrigo>? Abrigos { get; set; }
    }
}
