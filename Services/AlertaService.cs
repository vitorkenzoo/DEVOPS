using FuracaoAlerta.API.Data;
using FuracaoAlerta.API.DTOs;
using FuracaoAlerta.API.Models;
using Microsoft.EntityFrameworkCore;

namespace FuracaoAlerta.API.Services
{
    public class AlertaService
    {
        private readonly AppDbContext _context;

        public AlertaService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<AlertaDTO>> GetTodosAsync()
        {
            return await _context.Alertas
                .Select(a => new AlertaDTO
                {
                    Tipo = a.Tipo,
                    Descricao = a.Descricao,
                    Data = a.Data,
                    EventoId = a.EventoId
                }).ToListAsync();
        }

        public async Task<AlertaDTO?> GetPorIdAsync(int id)
        {
            var alerta = await _context.Alertas.FindAsync(id);
            if (alerta == null) return null;

            return new AlertaDTO
            {
                Tipo = alerta.Tipo,
                Descricao = alerta.Descricao,
                Data = alerta.Data,
                EventoId = alerta.EventoId
            };
        }

        public async Task<Alerta> CriarAsync(AlertaDTO dto)
        {
            var alerta = new Alerta
            {
                Tipo = dto.Tipo,
                Descricao = dto.Descricao,
                Data = dto.Data,
                EventoId = dto.EventoId
            };

            _context.Alertas.Add(alerta);
            await _context.SaveChangesAsync();

            return alerta;
        }

        public async Task<bool> AtualizarAsync(int id, AlertaDTO dto)
        {
            var alerta = await _context.Alertas.FindAsync(id);
            if (alerta == null) return false;

            alerta.Tipo = dto.Tipo;
            alerta.Descricao = dto.Descricao;
            alerta.Data = dto.Data;
            alerta.EventoId = dto.EventoId;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> RemoverAsync(int id)
        {
            var alerta = await _context.Alertas.FindAsync(id);
            if (alerta == null) return false;

            _context.Alertas.Remove(alerta);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
