using FuracaoAlerta.API.Data;
using FuracaoAlerta.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace FuracaoAlerta.API.Pages.Eventos
{
    public class IndexModel : PageModel
    {
        private readonly AppDbContext _context;

        public IndexModel(AppDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Evento NovoEvento { get; set; } = new();

        [BindProperty]
        public Evento EventoEditado { get; set; } = new();

        public List<Evento> Eventos { get; set; } = new();

        public async Task OnGetAsync()
        {
            Eventos = await _context.Eventos.ToListAsync();
        }

        public async Task<IActionResult> OnPostCreateAsync()
        {
            if (!ModelState.IsValid)
            {
                await OnGetAsync();
                return Page();
            }

            _context.Eventos.Add(NovoEvento);
            await _context.SaveChangesAsync();
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            var evento = await _context.Eventos.FindAsync(id);
            if (evento != null)
            {
                _context.Eventos.Remove(evento);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostEditAsync()
        {
            var evento = await _context.Eventos.FindAsync(EventoEditado.IdFuracao);
            if (evento != null)
            {
                evento.Nome = EventoEditado.Nome;
                evento.Intensidade = EventoEditado.Intensidade;
                evento.DataInicio = EventoEditado.DataInicio;
                evento.Localizacao = EventoEditado.Localizacao;
                evento.Status = EventoEditado.Status;

                await _context.SaveChangesAsync();
            }

            return RedirectToPage();
        }
    }
}
