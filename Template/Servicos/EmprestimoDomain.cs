using Emprestimos.Models;
using Emprestimos.Enums;
using Emprestimos.DTO;
using Emprestimos.Infra;
using Emprestimos.Servicos; 
using System.Linq;
using Emprestimos.Infra;

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


        public async Task<object> InserirEmprestimo(InserirEmprestimoDTO dto)
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

            return new
            {
                Mensagem = "Empréstimo realizado com sucesso!",
                IdEmprestimo = emprestimo.Id
            };
        }


        public async Task<string> AtualizarStatus(int id, AtualizarStatusDTO dto)
        {
            var emprestimo = _dataContext.Emprestimos.FirstOrDefault(e => e.Id == id);

            if (emprestimo == null)
                throw new Exception("Empréstimo não encontrado.");

            if (emprestimo.Status == StatusEmprestimo.Devolvido)
                throw new Exception("Este empréstimo já foi devolvido.");

            emprestimo.Status = dto.Status;
            emprestimo.DataDevolucao = dto.DataDevolucao ?? DateTime.Now;
            emprestimo.Multa = (emprestimo.DataDevolucao > emprestimo.DataEmprestimo.AddDays(14));

            await _dataContext.SaveChangesAsync();

            if (emprestimo.Status == StatusEmprestimo.Devolvido)
                await _livrosClient.MarcarComoDevolvido(emprestimo.CodigoLivro);

            return $"{emprestimo.Status}";
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
    }
}
