using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Emprestimos.DTO;

namespace Emprestimos.Servicos
{
    public class LivrosClient
    {
        private readonly HttpClient _http;

        public LivrosClient()
        {
            _http = new HttpClient
            {
                BaseAddress = new Uri("http://localhost:5089/api/") // porta do microserviço de livros
            };
        }

        // ✅ Verifica se o livro está disponível
        public async Task<bool> VerificarDisponibilidade(int idLivro)
        {
            var response = await _http.GetAsync($"livros/{idLivro}");

            if (!response.IsSuccessStatusCode)
                throw new Exception("Erro ao consultar o serviço de livros");

            var livro = await response.Content.ReadFromJsonAsync<VerificarLivroDTO>();

            if (livro == null)
                throw new Exception("Livro não encontrado");

            return livro.Disponibilidade;
        }

        // ✅ Marca como emprestado (Disponibilidade = false)
        public async Task MarcarComoEmprestado(int idLivro)
        {
            var content = JsonContent.Create(new { disponibilidade = false });
            var response = await _http.PatchAsync($"livros/{idLivro}/status", content);
            response.EnsureSuccessStatusCode();
        }

        // ✅ Marca como devolvido (Disponibilidade = true)
        public async Task MarcarComoDevolvido(int idLivro)
        {
            var content = JsonContent.Create(new { disponibilidade = true });
            var response = await _http.PatchAsync($"livros/{idLivro}/status", content);
            response.EnsureSuccessStatusCode();
        }

        public async Task<LivroDTO> BuscarLivroPorId(int idLivro)
        {
            var response = await _http.GetAsync($"livros/{idLivro}");

            if (!response.IsSuccessStatusCode)
                throw new Exception("Erro ao buscar o livro no serviço de livros.");

            var livro = await response.Content.ReadFromJsonAsync<LivroDTO>();

            if (livro == null)
                throw new Exception("Livro não encontrado.");

            return livro;
        }

    }
}
