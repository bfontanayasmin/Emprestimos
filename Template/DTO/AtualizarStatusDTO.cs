using Emprestimos.Enums;

namespace Emprestimos.DTO
{
    public class AtualizarStatusDTO
    {
        public int Id { get; set; }
        public required int IdLivro { get; set; }
        public required int IdLeitor { get; set; }

        public DateTime? DataInicio { get; set; }
        public DateTime? Prazo { get; set; }
        public DateTime? DataDevolucao { get; set; }

        public required StatusEmprestimo Status { get; set; }
    }
}
