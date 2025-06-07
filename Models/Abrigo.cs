using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FuracaoAlerta.API.Models
{
    public class Abrigo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdAbrigo { get; set; }

        [Required]
        [MaxLength(2000)]
        public string Nome { get; set; } = string.Empty;

        public int CapacidadeTotal { get; set; }
        public int LotacaoAtual { get; set; }

        [MaxLength(2000)]
        public string Coordenadas { get; set; } = string.Empty;

        public int? EnderecoId { get; set; }
        public Endereco? Endereco { get; set; }

        public ICollection<Usuario>? Usuarios { get; set; }
    }
}
