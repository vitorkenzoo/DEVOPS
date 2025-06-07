using FuracaoAlerta.API.Data;
using FuracaoAlerta.API.DTOs;
using FuracaoAlerta.API.Models;
using Microsoft.EntityFrameworkCore;

namespace FuracaoAlerta.API.Services
{
    public class UsuarioService
    {
        private readonly AppDbContext _context;

        public UsuarioService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<UsuarioDTO>> GetTodosAsync()
        {
            return await _context.Usuarios
                .Select(u => new UsuarioDTO
                {
                    Nome = u.Nome,
                    Email = u.Email,
                    Telefone = u.Telefone,
                    Cpf = u.Cpf,
                    Senha = u.Senha,
                    AbrigoId = u.AbrigoId,
                    EnderecoId = u.EnderecoId
                }).ToListAsync();
        }

        public async Task<UsuarioDTO?> GetPorIdAsync(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null) return null;

            return new UsuarioDTO
            {
                Nome = usuario.Nome,
                Email = usuario.Email,
                Telefone = usuario.Telefone,
                Cpf = usuario.Cpf,
                Senha = usuario.Senha,
                AbrigoId = usuario.AbrigoId,
                EnderecoId = usuario.EnderecoId
            };
        }

        public async Task<UsuarioDTO> CriarAsync(UsuarioDTO dto)
        {
            var usuario = new Usuario
            {
                Nome = dto.Nome,
                Email = dto.Email,
                Senha = dto.Senha 
            };

            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();

            return new UsuarioDTO
            {
                Nome = usuario.Nome,
                Email = usuario.Email,
                Senha = usuario.Senha
            };
        }

        public async Task<bool> AtualizarAsync(int id, UsuarioDTO dto)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null) return false;

            usuario.Nome = dto.Nome;
            usuario.Email = dto.Email;
            usuario.Telefone = dto.Telefone;
            usuario.Cpf = dto.Cpf;
            usuario.Senha = dto.Senha;
            usuario.AbrigoId = dto.AbrigoId;
            usuario.EnderecoId = dto.EnderecoId;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> RemoverAsync(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null) return false;

            _context.Usuarios.Remove(usuario);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
