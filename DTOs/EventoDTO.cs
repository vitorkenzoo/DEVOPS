namespace FuracaoAlerta.API.DTOs
{
    public class EventoDTO
    {
        public string Nome { get; set; } = string.Empty;
        public int Intensidade { get; set; }
        public DateTime DataInicio { get; set; }
        public string Localizacao { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
    }
}
