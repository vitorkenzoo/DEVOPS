using FuracaoAlerta.API.Models;

namespace FuracaoAlerta.API.Data
{
    public static class SeedData
    {
        public static void Inicializar(IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

            // Garante que o banco foi criado (em dev/local)
            context.Database.EnsureCreated();

            // Verificação segura para Oracle
            if (context.Eventos.Count() > 0) return;

            var endereco = new Endereco
            {
                Rua = "Av. Central",
                Numero = "100",
                Bairro = "Centro",
                Cidade = "Fortaleza",
                Estado = "CE"
            };

            var abrigo = new Abrigo
            {
                Nome = "Abrigo Municipal A",
                CapacidadeTotal = 200,
                LotacaoAtual = 85,
                Coordenadas = "-3.71722,-38.5433",
                Endereco = endereco
            };

            var usuario = new Usuario
            {
                Nome = "João Silva",
                Email = "joao@email.com",
                Telefone = "85999999999",
                Cpf = "12345678900",
                Senha = "123456",
                Endereco = endereco,
                Abrigo = abrigo
            };

            var evento = new Evento
            {
                Nome = "Furacão Aurora",
                Intensidade = 4,
                DataInicio = DateTime.UtcNow.AddDays(-2),
                Localizacao = "Litoral Norte",
                Status = "Ativo"
            };

            var alerta = new Alerta
            {
                Tipo = "Evacuação",
                Descricao = "Evacuação urgente da zona costeira.",
                Data = DateTime.UtcNow,
                Evento = evento
            };

            var usuarioAlerta = new UsuarioAlerta
            {
                Usuario = usuario,
                Alerta = alerta
            };

            context.Enderecos.Add(endereco);
            context.Abrigos.Add(abrigo);
            context.Usuarios.Add(usuario);
            context.Eventos.Add(evento);
            context.Alertas.Add(alerta);
            context.UsuariosAlertas.Add(usuarioAlerta);

            context.SaveChanges();
        }
    }
}
