using FuracaoAlerta.API.DTOs;
using FuracaoAlerta.API.Models;
using FuracaoAlerta.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace FuracaoAlerta.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventoController : ControllerBase
    {
        private readonly EventoService _service;

        public EventoController(EventoService service)
        {
            _service = service;
        }

        /// <summary>
        /// Retorna todos os eventos registrados.
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(List<EventoDTO>), 200)]
        public async Task<IActionResult> GetTodos()
        {
            var eventos = await _service.GetTodosAsync();
            return Ok(eventos);
        }

        /// <summary>
        /// Retorna um evento pelo ID.
        /// </summary>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(EventoDTO), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetPorId(int id)
        {
            var evento = await _service.GetPorIdAsync(id);
            if (evento == null) return NotFound();
            return Ok(evento);
        }

        /// <summary>
        /// Cria um novo evento.
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(Evento), 201)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Criar(EventoDTO dto)
        {
            var criado = await _service.CriarAsync(dto);
            return CreatedAtAction(nameof(GetPorId), new { id = criado.IdFuracao }, criado);
        }

        /// <summary>
        /// Atualiza os dados de um evento.
        /// </summary>
        [HttpPut("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Atualizar(int id, EventoDTO dto)
        {
            var atualizado = await _service.AtualizarAsync(id, dto);
            if (!atualizado) return NotFound();
            return NoContent();
        }

        /// <summary>
        /// Remove um evento.
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
