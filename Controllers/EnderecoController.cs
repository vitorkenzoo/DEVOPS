using FuracaoAlerta.API.DTOs;
using FuracaoAlerta.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace FuracaoAlerta.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EnderecoController : ControllerBase
    {
        private readonly EnderecoService _service;

        public EnderecoController(EnderecoService service)
        {
            _service = service;
        }

        /// <summary>
        /// Retorna todos os endereços cadastrados.
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(List<EnderecoDTO>), 200)]
        public async Task<IActionResult> GetTodos()
        {
            var enderecos = await _service.GetTodosAsync();
            return Ok(enderecos);
        }

        /// <summary>
        /// Retorna um endereço pelo ID.
        /// </summary>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(EnderecoDTO), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetPorId(int id)
        {
            var endereco = await _service.GetPorIdAsync(id);
            if (endereco == null) return NotFound();
            return Ok(endereco);
        }

        /// <summary>
        /// Cria um novo endereço.
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(EnderecoDTO), 201)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Criar(EnderecoDTO dto)
        {
            var criado = await _service.CriarAsync(dto);
            return CreatedAtAction(nameof(GetPorId), new { id = criado.IdEndereco }, dto);
        }

        /// <summary>
        /// Atualiza um endereço existente.
        /// </summary>
        [HttpPut("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Atualizar(int id, EnderecoDTO dto)
        {
            var atualizado = await _service.AtualizarAsync(id, dto);
            if (!atualizado) return NotFound();
            return NoContent();
        }

        /// <summary>
        /// Remove um endereço pelo ID.
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
