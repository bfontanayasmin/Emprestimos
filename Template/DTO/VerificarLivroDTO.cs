namespace Emprestimos.DTO
{
    public class VerificarLivroDTO
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Autor { get; set; } = string.Empty;
        public string CodigoISBN { get; set; } = string.Empty;
        public bool Disponibilidade { get; set; }
    }
}
