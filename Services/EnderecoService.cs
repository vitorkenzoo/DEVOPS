using FuracaoAlerta.API.Data;
using FuracaoAlerta.API.DTOs;
using FuracaoAlerta.API.Models;
using Microsoft.EntityFrameworkCore;

namespace FuracaoAlerta.API.Services
{
    public class EnderecoService
    {
        private readonly AppDbContext _context;

        public EnderecoService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<EnderecoDTO>> GetTodosAsync()
        {
            return await _context.Enderecos
                .Select(e => new EnderecoDTO
                {
                    Rua = e.Rua,
                    Numero = e.Numero,
                    Bairro = e.Bairro,
                    Cidade = e.Cidade,
                    Estado = e.Estado
                }).ToListAsync();
        }

        public async Task<EnderecoDTO?> GetPorIdAsync(int id)
        {
            var endereco = await _context.Enderecos.FindAsync(id);
            if (endereco == null) return null;

            return new EnderecoDTO
            {
                Rua = endereco.Rua,
                Numero = endereco.Numero,
                Bairro = endereco.Bairro,
                Cidade = endereco.Cidade,
                Estado = endereco.Estado
            };
        }

        public async Task<Endereco> CriarAsync(EnderecoDTO dto)
        {
            var endereco = new Endereco
            {
                Rua = dto.Rua,
                Numero = dto.Numero,
                Bairro = dto.Bairro,
                Cidade = dto.Cidade,
                Estado = dto.Estado
            };

            _context.Enderecos.Add(endereco);
            await _context.SaveChangesAsync();

            return endereco;
        }

        public async Task<bool> AtualizarAsync(int id, EnderecoDTO dto)
        {
            var endereco = await _context.Enderecos.FindAsync(id);
            if (endereco == null) return false;

            endereco.Rua = dto.Rua;
            endereco.Numero = dto.Numero;
            endereco.Bairro = dto.Bairro;
            endereco.Cidade = dto.Cidade;
            endereco.Estado = dto.Estado;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> RemoverAsync(int id)
        {
            var endereco = await _context.Enderecos.FindAsync(id);
            if (endereco == null) return false;

            _context.Enderecos.Remove(endereco);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
