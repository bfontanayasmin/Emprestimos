using Emprestimos.Enums;

namespace Emprestimos.DTO
{
    public class AtualizarStatusDTO
    {
        public int Id { get; set; } // ID do empréstimo
        public required int IdLivro { get; set; }
        public required int IdLeitor { get; set; }

        public DateTime? DataInicio { get; set; } // opcional, se quiser atualizar
        public DateTime? Prazo { get; set; } // opcional
        public DateTime? DataDevolucao { get; set; } // preenchida quando devolver o livro

        public required StatusEmprestimo Status { get; set; } // enum: Emprestado ou Devolvido
    }
}
