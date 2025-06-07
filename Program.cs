using FuracaoAlerta.API.Data;
using FuracaoAlerta.API.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// 🔌 Conexão com Oracle
var connectionString = builder.Configuration.GetConnectionString("OracleConnection");
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseOracle(connectionString));

// 🧠 Injeção de dependência dos serviços
builder.Services.AddScoped<AlertaService>();
builder.Services.AddScoped<EventoService>();
builder.Services.AddScoped<UsuarioService>();
builder.Services.AddScoped<AbrigoService>();
builder.Services.AddScoped<EnderecoService>();

// 📄 Controllers e Razor Pages
builder.Services.AddControllers();
builder.Services.AddRazorPages(); // Adiciona suporte a Razor Pages

// 🧪 Swagger com metadados
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Furacão Alerta API",
        Version = "v1",
        Description = "API REST para gestão de alertas, eventos e abrigos durante furacões.",
        Contact = new OpenApiContact
        {
            Name = "Equipe FuracaoAlerta",
            Email = "contato@furacaoalerta.com",
            Url = new Uri("https://github.com")
        }
    });
});

var app = builder.Build();

// 🌱 Executar Seed de dados iniciais
SeedData.Inicializar(app);

// 🚀 Pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseStaticFiles(); // Habilita arquivos estáticos (CSS, JS, etc. se necessário)
app.UseRouting();

app.UseAuthorization();

// Mapeamento das rotas de Controllers e Razor Pages
app.MapControllers();
app.MapRazorPages(); // 🔥 Ativa as páginas Razor

app.Run();
