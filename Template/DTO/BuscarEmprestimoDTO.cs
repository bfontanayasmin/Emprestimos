using Emprestimos.Enums;

namespace Emprestimos.DTO
{
    public class BuscarEmprestimoDTO
    {
        public int Id { get; set; }
        public int IdLeitor { get; set; }
        public int IdLivro { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime? Prazo { get; set; }
        public DateTime? DataDevolucao { get; set; }
        public StatusEmprestimo Status { get; set; }
    }
}
