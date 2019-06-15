using CopaFilmes.WebAPI.Models;
using System.Net.Http;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace CopaFilmes.WebAPI.DAL
{
    public class FilmesDal
    {
        static HttpClient client = new HttpClient();
     

        public static async Task<Filme[]> getListaFilmes() {
            // Consulta uma lista de filmes
            HttpResponseMessage response = await client.GetAsync("https://copadosfilmes.azurewebsites.net/api/filmes");
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
            var filmes = JsonConvert.DeserializeObject<Filme[]>(responseBody);
            return filmes;
        }
    }
}
