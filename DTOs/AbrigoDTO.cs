namespace FuracaoAlerta.API.DTOs
{
    public class AbrigoDTO
    {
        public string Nome { get; set; } = string.Empty;
        public int CapacidadeTotal { get; set; }
        public int LotacaoAtual { get; set; }
        public string Coordenadas { get; set; } = string.Empty;
        public int? EnderecoId { get; set; }
    }
}
