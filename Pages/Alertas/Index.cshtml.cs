using FuracaoAlerta.API.Data;
using FuracaoAlerta.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace FuracaoAlerta.API.Pages.Alertas
{
    public class IndexModel : PageModel
    {
        private readonly AppDbContext _context;

        public IndexModel(AppDbContext context)
        {
            _context = context;
        }

        public List<Alerta> Alertas { get; set; } = new();

        [BindProperty]
        public Alerta NovoAlerta { get; set; } = new();

        [BindProperty]
        public Alerta AlertaEditado { get; set; } = new();

        public async Task OnGetAsync()
        {
            Alertas = await _context.Alertas
                .Include(a => a.Evento)
                .ToListAsync();
        }

        public async Task<IActionResult> OnPostCreateAsync()
        {
            if (!ModelState.IsValid)
            {
                await OnGetAsync();
                return Page();
            }

            _context.Alertas.Add(NovoAlerta);
            await _context.SaveChangesAsync();
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostEditAsync()
        {
            var alertaExistente = await _context.Alertas.FindAsync(AlertaEditado.IdAlerta);
            if (alertaExistente == null) return NotFound();

            alertaExistente.Tipo = AlertaEditado.Tipo;
            alertaExistente.Descricao = AlertaEditado.Descricao;
            alertaExistente.Data = AlertaEditado.Data;
            alertaExistente.EventoId = AlertaEditado.EventoId;

            await _context.SaveChangesAsync();
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            var alerta = await _context.Alertas.FindAsync(id);
            if (alerta == null) return NotFound();

            _context.Alertas.Remove(alerta);
            await _context.SaveChangesAsync();
            return RedirectToPage();
        }
    }
}
