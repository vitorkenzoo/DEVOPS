using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FuracaoAlerta.API.Models
{
    public class Alerta
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdAlerta { get; set; }

        [Required]
        [MaxLength(2000)]
        public string Tipo { get; set; } = string.Empty;

        [Required]
        [MaxLength(2000)]
        public string Descricao { get; set; } = string.Empty;

        [Required]
        public DateTime Data { get; set; }

        public int EventoId { get; set; }
        public Evento? Evento { get; set; }

        public ICollection<UsuarioAlerta> UsuarioAlertas { get; set; } = new List<UsuarioAlerta>();

    }
}
