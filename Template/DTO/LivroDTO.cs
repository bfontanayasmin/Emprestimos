using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Emprestimos.DTO
{
    public class LivroDTO
    {
        [JsonPropertyName("id")]
        public int IdLeitor { get; set; }

        [JsonPropertyName("nome")]
        public string Nome { get; set; }

        [JsonPropertyName("contato")]
        public string Contato { get; set; }
    }
}
