using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FuracaoAlerta.API.Models
{
    public class UsuarioAlerta
    {
        [Key]
        public int UsuarioId { get; set; }

        [Key]
        public int AlertaId { get; set; }

        [ForeignKey("UsuarioId")]
        public Usuario? Usuario { get; set; }

        [ForeignKey("AlertaId")]
        public Alerta? Alerta { get; set; }
    }
}
