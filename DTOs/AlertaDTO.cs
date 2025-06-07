namespace FuracaoAlerta.API.DTOs
{
    public class AlertaDTO
    {
        public string Tipo { get; set; } = string.Empty;
        public string Descricao { get; set; } = string.Empty;
        public DateTime Data { get; set; }
        public int EventoId { get; set; }
    }
}
