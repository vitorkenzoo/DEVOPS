using FuracaoAlerta.API.Data;
using FuracaoAlerta.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace FuracaoAlerta.API.Pages.Usuarios
{
    public class IndexModel : PageModel
    {
        private readonly AppDbContext _context;

        public IndexModel(AppDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Usuario UsuarioModel { get; set; } = new();

        public List<Usuario> Usuarios { get; set; } = new();

        public async Task OnGetAsync()
        {
            Usuarios = await _context.Usuarios
                .Include(u => u.Abrigo)
                .Include(u => u.Endereco)
                .ToListAsync();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                await OnGetAsync();
                return Page();
            }

            _context.Usuarios.Add(UsuarioModel);
            await _context.SaveChangesAsync();
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario != null)
            {
                _context.Usuarios.Remove(usuario);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage();
        }
    }
}
