using Emprestimos.Models;
using Emprestimos.Enums;
using Emprestimos.DTO;
using Emprestimos.Infra;
using Emprestimos.Servicos;
using System.Linq;

namespace Emprestimos.Servicos
{
    public class EmprestimoDomain
    {
        private readonly DataContext _dataContext;
        private readonly LivrosClient _livrosClient;

        public EmprestimoDomain()
        {
            _dataContext = GeradorDeServicos.CarregarContexto();
            _livrosClient = new LivrosClient();
        }

        public async Task<RespostaEmprestimoDTO> InserirEmprestimo(InserirEmprestimoDTO dto)
        {
            var livroDisponivel = await _livrosClient.VerificarDisponibilidade(dto.IdLivro);

            if (!livroDisponivel)
                throw new Exception("Livro indisponível para empréstimo.");

            var emprestimo = new Emprestimo
            {
                CodigoLivro = dto.IdLivro,
                CodigoLeitor = dto.IdLeitor,
                DataEmprestimo = dto.DataInicio ?? DateTime.Now,
                Status = StatusEmprestimo.Emprestado,
                Multa = false
            };

            _dataContext.Emprestimos.Add(emprestimo);
            await _dataContext.SaveChangesAsync();

            await _livrosClient.MarcarComoEmprestado(dto.IdLivro);

            return new RespostaEmprestimoDTO
            {
                Mensagem = "Empréstimo realizado com sucesso!",
                IdEmprestimo = emprestimo.Id
            };
        }

        public async Task<string> DevolverEmprestimo(int id, AtualizarStatusDTO dto)
        {
            var emprestimo = _dataContext.Emprestimos.FirstOrDefault(e => e.Id == id);

            if (emprestimo == null)
                throw new Exception("Empréstimo não encontrado.");

            if (emprestimo.Status == StatusEmprestimo.Devolvido)
                throw new Exception("Este empréstimo já foi devolvido.");

            emprestimo.Status = StatusEmprestimo.Devolvido;
            emprestimo.DataDevolucao = dto.DataDevolucao ?? DateTime.Now;

            var prazoLimite = emprestimo.DataEmprestimo.AddDays(14);
            emprestimo.Multa = emprestimo.DataDevolucao > prazoLimite;

            await _dataContext.SaveChangesAsync();

            await _livrosClient.MarcarComoDevolvido(emprestimo.CodigoLivro);

            return "Devolução realizada com sucesso.";
        }

        public List<BuscarEmprestimoDTO> ListarPorLeitor(int idLeitor)
        {
            var emprestimos = _dataContext.Emprestimos
                .Where(e => e.CodigoLeitor == idLeitor)
                .ToList();

            return emprestimos.Select(e => new BuscarEmprestimoDTO
            {
                Id = e.Id,
                IdLeitor = e.CodigoLeitor,
                IdLivro = e.CodigoLivro,
                DataInicio = e.DataEmprestimo,
                DataDevolucao = e.DataDevolucao,
                Status = e.Status
            }).ToList();
        }

        public async Task<DetalheEmprestimoRespostaDTO> BuscarEmprestimoDetalhado(int id)
        {
            var emprestimo = await _dataContext.Emprestimos.FindAsync(id);

            if (emprestimo == null)
            {
                return new DetalheEmprestimoRespostaDTO
                {
                    Mensagem = "Empréstimo não encontrado.",
                    Emprestimo = null
                };
            }

            var livro = await _livrosClient.BuscarLivroPorId(emprestimo.CodigoLivro);

            var leitoresClient = new LeitoresClient();
            var leitor = await leitoresClient.BuscarLeitorPorId(emprestimo.CodigoLeitor);

            var dto = new BuscarEmprestimoDetalhadoDTO
            {
                Id = emprestimo.Id,
                IdLeitor = emprestimo.CodigoLeitor,
                DataInicio = emprestimo.DataEmprestimo,
                DataDevolucao = emprestimo.DataDevolucao,
                Status = emprestimo.Status,
                Livro = livro,
                Leitor = leitor
            };

            return new DetalheEmprestimoRespostaDTO
            {
                Mensagem = "Empréstimo localizado com sucesso.",
                Emprestimo = dto
            };
        }


    }
}
