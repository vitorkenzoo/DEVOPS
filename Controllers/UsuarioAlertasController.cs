using FuracaoAlerta.API.Data;
using FuracaoAlerta.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FuracaoAlerta.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioAlertaController : ControllerBase
    {
        private readonly AppDbContext _context;

        public UsuarioAlertaController(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Cria uma nova associação entre usuário e alerta.
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(UsuarioAlerta), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post([FromBody] UsuarioAlerta ua)
        {
            _context.UsuariosAlertas.Add(ua);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetByIds), new { usuarioId = ua.UsuarioId, alertaId = ua.AlertaId }, ua);
        }

        /// <summary>
        /// Busca uma associação específica.
        /// </summary>
        [HttpGet("{usuarioId}/{alertaId}")]
        [ProducesResponseType(typeof(UsuarioAlerta), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetByIds(int usuarioId, int alertaId)
        {
            var result = await _context.UsuariosAlertas
                .Include(ua => ua.Usuario)
                .Include(ua => ua.Alerta)
                .FirstOrDefaultAsync(x => x.UsuarioId == usuarioId && x.AlertaId == alertaId);

            if (result == null) return NotFound();
            return Ok(result);
        }

        /// <summary>
        /// Deleta uma associação entre usuário e alerta.
        /// </summary>
        [HttpDelete("{usuarioId}/{alertaId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int usuarioId, int alertaId)
        {
            var ua = await _context.UsuariosAlertas
                .FirstOrDefaultAsync(x => x.UsuarioId == usuarioId && x.AlertaId == alertaId);

            if (ua == null) return NotFound();

            _context.UsuariosAlertas.Remove(ua);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
