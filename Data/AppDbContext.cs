using FuracaoAlerta.API.Models;
using Microsoft.EntityFrameworkCore;

namespace FuracaoAlerta.API.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }

        public DbSet<Alerta> Alertas { get; set; }
        public DbSet<Evento> Eventos { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Abrigo> Abrigos { get; set; }
        public DbSet<Endereco> Enderecos { get; set; }
        public DbSet<UsuarioAlerta> UsuariosAlertas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuração da chave composta para tabela de junção
            modelBuilder.Entity<UsuarioAlerta>()
                .HasKey(ua => new { ua.UsuarioId, ua.AlertaId });

            modelBuilder.Entity<UsuarioAlerta>()
                .HasOne(ua => ua.Usuario)
                .WithMany(u => u.UsuarioAlertas) // Corrigido aqui
                .HasForeignKey(ua => ua.UsuarioId);

            modelBuilder.Entity<UsuarioAlerta>()
                .HasOne(ua => ua.Alerta)
                .WithMany(a => a.UsuarioAlertas) // Corrigido aqui
                .HasForeignKey(ua => ua.AlertaId);
        }
    }
}
