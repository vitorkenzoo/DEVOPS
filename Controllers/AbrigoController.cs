using FuracaoAlerta.API.DTOs;
using FuracaoAlerta.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace FuracaoAlerta.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AbrigoController : ControllerBase
    {
        private readonly AbrigoService _service;

        public AbrigoController(AbrigoService service)
        {
            _service = service;
        }

        /// <summary>
        /// Retorna todos os abrigos cadastrados.
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(List<AbrigoDTO>), 200)]
        public async Task<IActionResult> GetTodos()
        {
            var abrigos = await _service.GetTodosAsync();
            return Ok(abrigos);
        }

        /// <summary>
        /// Retorna um abrigo pelo ID.
        /// </summary>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(AbrigoDTO), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetPorId(int id)
        {
            var abrigo = await _service.GetPorIdAsync(id);
            if (abrigo == null) return NotFound();
            return Ok(abrigo);
        }

        /// <summary>
        /// Cria um novo abrigo.
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(AbrigoDTO), 201)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Criar(AbrigoDTO dto)
        {
            var criado = await _service.CriarAsync(dto);
            return CreatedAtAction(nameof(GetPorId), new { id = criado.IdAbrigo }, criado);
        }

        /// <summary>
        /// Atualiza um abrigo existente.
        /// </summary>
        [HttpPut("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Atualizar(int id, AbrigoDTO dto)
        {
            var atualizado = await _service.AtualizarAsync(id, dto);
            if (!atualizado) return NotFound();
            return NoContent();
        }

        /// <summary>
        /// Remove um abrigo pelo ID.
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
