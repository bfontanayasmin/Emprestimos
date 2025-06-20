using Emprestimos.Enums;

namespace Emprestimos.DTO
{
    public class EmprestimoComNomeLivroDTO
    {
        public int IdEmprestimo { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime? DataDevolucao { get; set; }
        public string NomeLivro { get; set; }
        public StatusEmprestimo Status { get; set; }
    }

}
