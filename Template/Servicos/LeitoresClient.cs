using Emprestimos.DTO;

namespace Emprestimos.Servicos

{
    public class LeitoresClient
    {
        private readonly HttpClient _http;

        public LeitoresClient()
        {
            _http = new HttpClient
            {
                BaseAddress = new Uri("http://localhost:5091/api/")
            };
        }

        public async Task<LeitorDTO> BuscarLeitorPorId(int id)
        {
            var response = await _http.GetAsync($"leitores/{id}");

            if (!response.IsSuccessStatusCode)
                throw new Exception("Erro ao buscar leitor");

            var leitor = await response.Content.ReadFromJsonAsync<LeitorDTO>();

            if (leitor == null)
                throw new Exception("Leitor não encontrado");

            return leitor;
        }
    }

}
