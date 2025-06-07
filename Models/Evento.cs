using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FuracaoAlerta.API.Models
{
    public class Evento
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdFuracao { get; set; }

        [Required]
        [MaxLength(2000)]
        public string Nome { get; set; } = string.Empty;

        [Required]
        public int Intensidade { get; set; }

        [Required]
        public DateTime DataInicio { get; set; }

        [Required]
        [MaxLength(2000)]
        public string Localizacao { get; set; } = string.Empty;

        [Required]
        [MaxLength(2000)]
        public string Status { get; set; } = string.Empty;

        public ICollection<Alerta>? Alertas { get; set; }
    }
}
