using FuracaoAlerta.API.Data;
using FuracaoAlerta.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace FuracaoAlerta.API.Pages.Abrigos
{
    public class IndexModel : PageModel
    {
        private readonly AppDbContext _context;

        public IndexModel(AppDbContext context)
        {
            _context = context;
        }

        public List<Abrigo> Abrigos { get; set; } = new();

        [BindProperty]
        public Abrigo NovoAbrigo { get; set; } = new();

        [BindProperty]
        public Abrigo AbrigoEditado { get; set; } = new();

        public async Task OnGetAsync()
        {
            Abrigos = await _context.Abrigos
                .Include(a => a.Endereco)
                .ToListAsync();
        }

        public async Task<IActionResult> OnPostCreateAsync()
        {
            if (!ModelState.IsValid)
            {
                await OnGetAsync();
                return Page();
            }

            _context.Abrigos.Add(NovoAbrigo);
            await _context.SaveChangesAsync();
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostEditAsync()
        {
            var abrigoExistente = await _context.Abrigos.FindAsync(AbrigoEditado.IdAbrigo);
            if (abrigoExistente == null) return NotFound();

            abrigoExistente.Nome = AbrigoEditado.Nome;
            abrigoExistente.CapacidadeTotal = AbrigoEditado.CapacidadeTotal;
            abrigoExistente.LotacaoAtual = AbrigoEditado.LotacaoAtual;
            abrigoExistente.Coordenadas = AbrigoEditado.Coordenadas;

            await _context.SaveChangesAsync();
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            var abrigo = await _context.Abrigos.FindAsync(id);
            if (abrigo == null) return NotFound();

            _context.Abrigos.Remove(abrigo);
            await _context.SaveChangesAsync();
            return RedirectToPage();
        }
    }
}
