using Emprestimos.Enums;

namespace Emprestimos.Models;
    public class Emprestimo
    {
        public int Id { get; set; }

        public int CodigoLivro { get; set; }

        public int CodigoLeitor { get; set; }

        public DateTime DataEmprestimo { get; set; }

        public DateTime? DataDevolucao { get; set; }

        public StatusEmprestimo Status { get; set; }

        public bool Multa { get; set; }
    }
