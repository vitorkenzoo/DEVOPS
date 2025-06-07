using FuracaoAlerta.API.Data;
using FuracaoAlerta.API.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// 🔌 Conexão com Oracle via ENV
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseOracle(connectionString));

// 🧠 DI dos serviços
builder.Services.AddScoped<AlertaService>();
builder.Services.AddScoped<EventoService>();
builder.Services.AddScoped<UsuarioService>();
builder.Services.AddScoped<AbrigoService>();
builder.Services.AddScoped<EnderecoService>();

// 📄 Controllers e Razor Pages
builder.Services.AddControllers();
builder.Services.AddRazorPages();

// 🧪 Swagger sempre ativo
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

// 🌱 Seed de dados
SeedData.Inicializar(app);

// 🚀 Pipeline
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
    c.RoutePrefix = string.Empty; // Swagger na raiz
});

// Sem redirecionamento para HTTPS
// app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapControllers();
app.MapRazorPages();

app.Run();
