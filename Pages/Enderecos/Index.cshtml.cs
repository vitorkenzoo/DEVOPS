using FuracaoAlerta.API.Data;
using FuracaoAlerta.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace FuracaoAlerta.API.Pages.Enderecos
{
    public class IndexModel : PageModel
    {
        private readonly AppDbContext _context;

        public IndexModel(AppDbContext context)
        {
            _context = context;
        }

        public List<Endereco> Enderecos { get; set; } = new();

        [BindProperty]
        public Endereco NovoEndereco { get; set; } = new();

        [BindProperty]
        public Endereco EnderecoEditado { get; set; } = new();

        public async Task OnGetAsync()
        {
            Enderecos = await _context.Enderecos.ToListAsync();
        }

        public async Task<IActionResult> OnPostCreateAsync()
        {
            if (!ModelState.IsValid)
            {
                await OnGetAsync();
                return Page();
            }

            _context.Enderecos.Add(NovoEndereco);
            await _context.SaveChangesAsync();
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostEditAsync()
        {
            var enderecoExistente = await _context.Enderecos.FindAsync(EnderecoEditado.IdEndereco);
            if (enderecoExistente == null) return NotFound();

            enderecoExistente.Rua = EnderecoEditado.Rua;
            enderecoExistente.Numero = EnderecoEditado.Numero;
            enderecoExistente.Bairro = EnderecoEditado.Bairro;
            enderecoExistente.Cidade = EnderecoEditado.Cidade;
            enderecoExistente.Estado = EnderecoEditado.Estado;

            await _context.SaveChangesAsync();
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            var endereco = await _context.Enderecos.FindAsync(id);
            if (endereco == null) return NotFound();

            _context.Enderecos.Remove(endereco);
            await _context.SaveChangesAsync();
            return RedirectToPage();
        }
    }
}
