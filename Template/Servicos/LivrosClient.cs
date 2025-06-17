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
                BaseAddress = new Uri("http://localhost:5001/api/") // ✅ ajuste para a URL real do micro de livros
            };
        }

        // Verifica se um livro está disponível
        public async Task<bool> VerificarDisponibilidade(int idLivro)
        {
            var response = await _http.GetAsync($"livros/{idLivro}");
            if (!response.IsSuccessStatusCode)
                throw new Exception("Erro ao consultar o serviço de livros");

            var livro = await response.Content.ReadFromJsonAsync<VerificarLivroDTO>();
            return livro?.Disponibilidade ?? false;
        }

        // Marca um livro como emprestado (Disponibilidade = false)
        public async Task MarcarComoEmprestado(int idLivro)
        {
            var content = JsonContent.Create(new { disponibilidade = false });
            var response = await _http.PatchAsync($"livros/{idLivro}/status", content);
            response.EnsureSuccessStatusCode();
        }

        // Marca um livro como devolvido (Disponibilidade = true)
        public async Task MarcarComoDevolvido(int idLivro)
        {
            var content = JsonContent.Create(new { disponibilidade = true });
            var response = await _http.PatchAsync($"livros/{idLivro}/status", content);
            response.EnsureSuccessStatusCode();
        }
    }
}
