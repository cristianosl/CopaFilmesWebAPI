using CopaFilmes.WebAPI.Model;
using System.Collections.Generic;
using System.Linq;

namespace CopaFilmes.WebAPI.Model
{
    public static class FilmeExtensions
    {
        public static List<Filme> getByIds(this List<Filme> filmes, string[] ids)
        {
            return filmes.Where(filme => ids.Contains<string>(filme.Id)).ToList();
        }

        public static IOrderedEnumerable<Filme> getOrdenado(this List<Filme> filmes)
        {
            return filmes.OrderBy(filme => filme.Titulo);
        }

        public static List<Filme> getListDisputa(this List<Filme> filmes)
        {
            List<Filme> filmesDisputa = new List<Filme>();

            for (int i = 0; i < filmes.Count() / 2; i++)
            {
                var primeiroItem = filmes.ElementAt(i);
                var ultimoItem = filmes.ElementAt(filmes.Count() - 1 - i);
                filmesDisputa.Add(primeiroItem);
                filmesDisputa.Add(ultimoItem);
            }
            return filmesDisputa;
        }

        public static List<Filme> getListRodada(this List<Filme> filmes)
        {
            List<Filme> filmesRodada = new List<Filme>();
            for (int i = 0; i < filmes.Count; i = i + 2)
            {
                var primeiro = filmes.ElementAt(i);
                var segundo = filmes.ElementAt(i + 1);

                filmesRodada.Add(getVencedor(primeiro, segundo));
            }
            return filmesRodada;
        }

        public static Filme getVencedor(Filme primeiro, Filme segundo)
        {
            Filme filmeGanhador;
            if (primeiro.Nota > segundo.Nota)
            {
                filmeGanhador = primeiro;
            }
            else if (segundo.Nota > primeiro.Nota)
            {
                filmeGanhador = segundo;
            }
            else
            {
                filmeGanhador = primeiro;
            }
            return filmeGanhador;
        }
    }
}
