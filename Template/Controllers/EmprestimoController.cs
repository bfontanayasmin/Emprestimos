using Microsoft.AspNetCore.Mvc;
using Emprestimos.Servicos;
using Emprestimos.DTO;
using Emprestimos.Enums;

namespace Emprestimos.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmprestimosController : ControllerBase
    {
        private readonly EmprestimoDomain _emprestimoDomain;

        public EmprestimosController()
        {
            _emprestimoDomain = new EmprestimoDomain();
        }

        // POST /api/emprestimos
        [HttpPost]
        public async Task<IActionResult> CriarEmprestimo([FromBody] InserirEmprestimoDTO dto)
        {
            try
            {
                await _emprestimoDomain.InserirEmprestimo(dto);
                return StatusCode(201); // Created
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PATCH /api/emprestimos/{id}/devolucao
        [HttpPatch("{id}/devolucao")]
        public async Task<IActionResult> RegistrarDevolucao(int id, [FromBody] AtualizarStatusDTO dto)
        {
            try
            {
                await _emprestimoDomain.AtualizarStatus(id, dto);
                return NoContent();
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        // GET /api/emprestimos/leitor/{id}
        [HttpGet("leitor/{id}")]
        public IActionResult ListarPorLeitor(int id)
        {
            var emprestimos = _emprestimoDomain.ListarPorLeitor(id);
            return Ok(emprestimos);
        }
    }
}
