using FuracaoAlerta.API.DTOs;
using FuracaoAlerta.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace FuracaoAlerta.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly UsuarioService _service;

        public UsuarioController(UsuarioService service)
        {
            _service = service;
        }

        /// <summary>
        /// Lista todos os usuários cadastrados
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(List<UsuarioDTO>), 200)]
        public async Task<IActionResult> Get()
        {
            var usuarios = await _service.GetTodosAsync();
            return Ok(usuarios);
        }

        /// <summary>
        /// Retorna um usuário por ID
        /// </summary>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(UsuarioDTO), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetById(int id)
        {
            var usuario = await _service.GetPorIdAsync(id);
            if (usuario == null) return NotFound();
            return Ok(usuario);
        }

        /// <summary>
        /// Cria um novo usuário (apenas nome, email e senha)
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(UsuarioDTO), 201)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Post([FromBody] UsuarioDTO dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var novoUsuario = await _service.CriarAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = novoUsuario.Email }, novoUsuario);
        }

        /// <summary>
        /// Atualiza um usuário existente
        /// </summary>
        [HttpPut("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Put(int id, [FromBody] UsuarioDTO dto)
        {
            var atualizado = await _service.AtualizarAsync(id, dto);
            return atualizado ? NoContent() : NotFound();
        }

        /// <summary>
        /// Remove um usuário por ID
        /// </summary>
        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Delete(int id)
        {
            var removido = await _service.RemoverAsync(id);
            return removido ? NoContent() : NotFound();
        }
    }
}
