namespace Emprestimos.DTO
{
    public class InserirEmprestimoDTO
    {
        public required int IdLivro { get; set; }

        public required int IdLeitor { get; set; }

        public DateTime? DataInicio { get; set; }

        public required DateTime Prazo { get; set; }
    }
}
