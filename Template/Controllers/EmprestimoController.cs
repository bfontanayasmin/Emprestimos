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
                var resposta = await _emprestimoDomain.InserirEmprestimo(dto);
                return Ok(new
                {
                    mensagem = resposta.Mensagem,
                    idEmprestimo = resposta.IdEmprestimo
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensagem = ex.Message });
            }
        }

        [HttpPatch("{id}/devolucao")]
        public async Task<IActionResult> DevolverEmprestimo(int id, [FromBody] AtualizarStatusDTO dto)
        {
            try
            {
                var mensagem = await _emprestimoDomain.DevolverEmprestimo(id, dto);
                return Ok(new { mensagem });
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensagem = ex.Message });
            }
        }

        [HttpGet("leitor/{id}")]
        public IActionResult ListarPorLeitor(int id)
        {
            var emprestimos = _emprestimoDomain.ListarPorLeitor(id);
            return Ok(emprestimos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> BuscarEmprestimo(int id)
        {
            try
            {
                var resultado = await _emprestimoDomain.BuscarEmprestimoDetalhado(id);

                if (resultado.Emprestimo == null)
                    return NotFound(new { mensagem = resultado.Mensagem });

                return Ok(resultado); // já retorna DetalheEmprestimoRespostaDTO corretamente
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensagem = ex.Message });
            }
        }
    }
}
