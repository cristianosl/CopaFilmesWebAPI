using CopaFilmes.WebAPI.DAL;
using CopaFilmes.WebAPI.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CopaFilmes.WebAPI.Tests.Models
{
    [TestClass]
    public class FilmesResultadosTest
    {
        private List<Filme> _filmes;
        string[] _ids = new string[] { "tt3606756", "tt4881806", "tt5164214", "tt7784604", "tt4154756", "tt5463162", "tt3778644", "tt3501632" };


        private async Task<List<Filme>> selecionarOs8Primeiros()
        {
            if (_filmes == null) _filmes = await FilmesDal.getListaFilmes();
            return _filmes.getByIds(_ids);
        }

        private async Task<IOrderedEnumerable<Filme>> selecionarOs8PrimeirosEmOrdemAlfabetica()
        {
            var filmes = await selecionarOs8Primeiros();
            return filmes.getOrdenacaoTituloAsc();
        }

        private async Task<IOrderedEnumerable<Filme>> selecionarFilmesQuartasDeFinais()
        {
            var filmes = await selecionarOs8PrimeirosEmOrdemAlfabetica();
            return filmes;
        }

        [TestMethod]
        public async Task DeveRetornarAMesmaQuantidadeConsultada()
        {
            var filmesSelecionados = await selecionarOs8Primeiros();
            Assert.AreEqual(8, filmesSelecionados.Count);
        }

        [TestMethod]
        [DataRow(new string[] { "tt5463162", "tt3778644", "tt7784604", "tt4881806", "tt5164214", "tt3606756", "tt3501632", "tt4154756" })]
        public async Task DeveDeixarEmOrdemAlfabetica(string[] ids)
        {
            var filmesOrdenados = await selecionarOs8PrimeirosEmOrdemAlfabetica();

            // "Deadpool 2" // tt5463162
            // "Han Solo: Uma História Star Wars" // tt3778644
            // "Hereditário" // tt7784604
            // "Jurassic World: Reino Ameaçado" // tt4881806
            // "Oito Mulheres e um Segredo" // tt5164214
            // "Os Incríveis 2" // tt3606756
            // "Thor: Ragnarok" // tt3501632
            // "Vingadores: Guerra Infinita" // tt4154756

            // Testes para ordenação
            for (int i = 0; i < ids.Length; i++)
            {
                Assert.AreEqual(ids[i], filmesOrdenados.ElementAt(i).Id);
            }

        }

        [TestMethod]
        [DataRow(new string[] { "tt4154756", "tt3501632", "tt3606756", "tt4881806" })]
        public async Task DeveGerarQuartasDeFinais(string[] ids)
        {
            var filmes = await selecionarOs8PrimeirosEmOrdemAlfabetica(); 
            List<Filme> filmesQuartas = filmes
                .ToList()
                .getListDisputa()
                .getListRodada(); // Quartas de finais


            // "Vingadores: Guerra Infinita" // tt4154756
            // "Thor: Ragnarok" // tt3501632
            // "Os Incríveis 2" // tt3606756
            // "Jurassic World: Reino Ameaçado" // tt4881806

            for (int i = 0; i < ids.Length; i++)
            {
                Assert.AreEqual(ids[i], filmesQuartas.ElementAt(i).Id);
            }
        }

        [TestMethod]
        [DataRow(new string[] { "tt4154756", "tt3606756" })]
        public async Task DeveGeraFinais(string[] ids)
        {
            var filmes = await selecionarOs8PrimeirosEmOrdemAlfabetica();            
            List<Filme> filmesFinais = filmes
                .ToList()
                .getListDisputa()
                .getListRodada() // Quartas de finais
                .getListRodada(); // Finais

            // "Vingadores: Guerra Infinita" // tt4154756
            // "Os Incríveis 2" // tt3606756

            for (int i = 0; i < ids.Length; i++)
            {
                Assert.AreEqual(ids[i], filmesFinais.ElementAt(i).Id);
            }
        }

        [TestMethod]
        [DataRow("tt4154756")]
        public async Task DeveRetornarOVencedor(string id)
        {
            var filmes = await selecionarOs8PrimeirosEmOrdemAlfabetica();
            List<Filme> filmesFinais = filmes
                .ToList()
                .getListDisputa()
                .getListRodada() // Quartas de finais
                .getListRodada(); // Finais

            Filme filmeVencedor = FilmeExtensions.getVencedor(filmesFinais.First(), filmesFinais.Last());

            // "Vingadores: Guerra Infinita" // tt4154756

            Assert.AreEqual(id, filmeVencedor.Id);
        }
    }
}
