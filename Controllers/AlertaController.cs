using FuracaoAlerta.API.DTOs;
using FuracaoAlerta.API.Models;
using FuracaoAlerta.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace FuracaoAlerta.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AlertaController : ControllerBase
    {
        private readonly AlertaService _service;

        public AlertaController(AlertaService service)
        {
            _service = service;
        }

        /// <summary>
        /// Retorna todos os alertas registrados.
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(List<AlertaDTO>), 200)]
        public async Task<IActionResult> GetTodos()
        {
            var alertas = await _service.GetTodosAsync();
            return Ok(alertas);
        }

        /// <summary>
        /// Retorna um alerta pelo ID.
        /// </summary>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(AlertaDTO), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetPorId(int id)
        {
            var alerta = await _service.GetPorIdAsync(id);
            if (alerta == null) return NotFound();
            return Ok(alerta);
        }

        /// <summary>
        /// Cria um novo alerta.
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(Alerta), 201)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Criar(AlertaDTO dto)
        {
            var criado = await _service.CriarAsync(dto);
            return CreatedAtAction(nameof(GetPorId), new { id = criado.IdAlerta }, criado);
        }

        /// <summary>
        /// Atualiza um alerta existente.
        /// </summary>
        [HttpPut("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Atualizar(int id, AlertaDTO dto)
        {
            var atualizado = await _service.AtualizarAsync(id, dto);
            if (!atualizado) return NotFound();
            return NoContent();
        }

        /// <summary>
        /// Remove um alerta pelo ID.
        /// </summary>
        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Remover(int id)
        {
            var removido = await _service.RemoverAsync(id);
            if (!removido) return NotFound();
            return NoContent();
        }
    }
}
