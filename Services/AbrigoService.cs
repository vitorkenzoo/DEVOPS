using FuracaoAlerta.API.Data;
using FuracaoAlerta.API.DTOs;
using FuracaoAlerta.API.Models;
using Microsoft.EntityFrameworkCore;

namespace FuracaoAlerta.API.Services
{
    public class AbrigoService
    {
        private readonly AppDbContext _context;

        public AbrigoService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<AbrigoDTO>> GetTodosAsync()
        {
            return await _context.Abrigos
                .Select(a => new AbrigoDTO
                {
                    Nome = a.Nome,
                    CapacidadeTotal = a.CapacidadeTotal,
                    LotacaoAtual = a.LotacaoAtual,
                    Coordenadas = a.Coordenadas,
                    EnderecoId = a.EnderecoId
                }).ToListAsync();
        }

        public async Task<AbrigoDTO?> GetPorIdAsync(int id)
        {
            var abrigo = await _context.Abrigos.FindAsync(id);
            if (abrigo == null) return null;

            return new AbrigoDTO
            {
                Nome = abrigo.Nome,
                CapacidadeTotal = abrigo.CapacidadeTotal,
                LotacaoAtual = abrigo.LotacaoAtual,
                Coordenadas = abrigo.Coordenadas,
                EnderecoId = abrigo.EnderecoId
            };
        }

        public async Task<Abrigo> CriarAsync(AbrigoDTO dto)
        {
            var abrigo = new Abrigo
            {
                Nome = dto.Nome,
                CapacidadeTotal = dto.CapacidadeTotal,
                LotacaoAtual = dto.LotacaoAtual,
                Coordenadas = dto.Coordenadas,
                EnderecoId = dto.EnderecoId
            };

            _context.Abrigos.Add(abrigo);
            await _context.SaveChangesAsync();

            return abrigo;
        }

        public async Task<bool> AtualizarAsync(int id, AbrigoDTO dto)
        {
            var abrigo = await _context.Abrigos.FindAsync(id);
            if (abrigo == null) return false;

            abrigo.Nome = dto.Nome;
            abrigo.CapacidadeTotal = dto.CapacidadeTotal;
            abrigo.LotacaoAtual = dto.LotacaoAtual;
            abrigo.Coordenadas = dto.Coordenadas;
            abrigo.EnderecoId = dto.EnderecoId;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> RemoverAsync(int id)
        {
            var abrigo = await _context.Abrigos.FindAsync(id);
            if (abrigo == null) return false;

            _context.Abrigos.Remove(abrigo);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
