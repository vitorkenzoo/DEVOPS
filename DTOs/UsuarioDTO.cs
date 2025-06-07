namespace FuracaoAlerta.API.DTOs
{
    public class UsuarioDTO
    {
        public string Nome { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Telefone { get; set; } = string.Empty;
        public string Cpf { get; set; } = string.Empty;
        public string Senha { get; set; } = string.Empty;

        public int? AbrigoId { get; set; }
        public int? EnderecoId { get; set; }
    }
}
