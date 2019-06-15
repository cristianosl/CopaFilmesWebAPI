using CopaFilmes.WebAPI.Model;
using System.Net.Http;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using System;
using System.Linq;

namespace CopaFilmes.WebAPI.DAL
{
    public class FilmesDal
    {
        static HttpClient client = new HttpClient();
     

        public static async Task<List<Filme>> getListaFilmes() {
            // Consulta uma lista de filmes
            HttpResponseMessage response = await client.GetAsync("https://copadosfilmes.azurewebsites.net/api/filmes");
            response.EnsureSuccessStatusCode();
            // Json
            string responseBody = await response.Content.ReadAsStringAsync();
            // List<Filme>
            var filmes = JsonConvert.DeserializeObject<List<Filme>>(responseBody);
            return filmes;
        }
    }
}
