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


        [HttpPost]
        public async Task<IActionResult> CriarEmprestimo([FromBody] InserirEmprestimoDTO dto)
        {
            try
            {
                var mensagem = await _emprestimoDomain.InserirEmprestimo(dto);
                
                return Ok(new { mensagem });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPatch("{id}/devolucao")]
        public async Task<IActionResult> RegistrarDevolucao(int id, [FromBody] AtualizarStatusDTO dto)
        {
            try
            {
                await _emprestimoDomain.AtualizarStatus(id, dto);
                return Ok(new { mensagem = "Devolução registrada com sucesso!"});
            }
            catch (Exception ex)
            {
                return NotFound(new { ex.Message });
            }
        }

        [HttpGet("leitor/{id}")]
        public IActionResult ListarPorLeitor(int id)
        {
            var emprestimos = _emprestimoDomain.ListarPorLeitor(id);
            return Ok(emprestimos);
        }
    }
}
