using FuracaoAlerta.API.Data;
using FuracaoAlerta.API.DTOs;
using FuracaoAlerta.API.Models;
using Microsoft.EntityFrameworkCore;

namespace FuracaoAlerta.API.Services
{
    public class EventoService
    {
        private readonly AppDbContext _context;

        public EventoService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<EventoDTO>> GetTodosAsync()
        {
            return await _context.Eventos
                .Select(e => new EventoDTO
                {
                    Nome = e.Nome,
                    Intensidade = e.Intensidade,
                    DataInicio = e.DataInicio,
                    Localizacao = e.Localizacao,
                    Status = e.Status
                }).ToListAsync();
        }

        public async Task<EventoDTO?> GetPorIdAsync(int id)
        {
            var evento = await _context.Eventos.FindAsync(id);
            if (evento == null) return null;

            return new EventoDTO
            {
                Nome = evento.Nome,
                Intensidade = evento.Intensidade,
                DataInicio = evento.DataInicio,
                Localizacao = evento.Localizacao,
                Status = evento.Status
            };
        }

        public async Task<Evento> CriarAsync(EventoDTO dto)
        {
            var evento = new Evento
            {
                Nome = dto.Nome,
                Intensidade = dto.Intensidade,
                DataInicio = dto.DataInicio,
                Localizacao = dto.Localizacao,
                Status = dto.Status
            };

            _context.Eventos.Add(evento);
            await _context.SaveChangesAsync();

            return evento;
        }

        public async Task<bool> AtualizarAsync(int id, EventoDTO dto)
        {
            var evento = await _context.Eventos.FindAsync(id);
            if (evento == null) return false;

            evento.Nome = dto.Nome;
            evento.Intensidade = dto.Intensidade;
            evento.DataInicio = dto.DataInicio;
            evento.Localizacao = dto.Localizacao;
            evento.Status = dto.Status;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> RemoverAsync(int id)
        {
            var evento = await _context.Eventos.FindAsync(id);
            if (evento == null) return false;

            _context.Eventos.Remove(evento);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
