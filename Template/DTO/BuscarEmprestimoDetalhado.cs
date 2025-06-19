using Emprestimos.Enums;

namespace Emprestimos.DTO
{
    public class BuscarEmprestimoDetalhadoDTO
    {
        public int Id { get; set; }
        public int IdLeitor { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime? DataDevolucao { get; set; }
        public StatusEmprestimo Status { get; set; }

        public LivroDTO Livro { get; set; }
    }
}
