using System.Threading.Tasks;
using CopaFilmes.WebAPI.DAL;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CopaFilmes.WebAPI.Tests.Dal
{
    [TestClass]
    public class FilmeDalTest
    {
        [TestMethod]
        public async Task deveRetornarUmListDe16Filmes()
        {
            var filmes = await FilmesDal.getListaFilmes();
            Assert.AreEqual(16, filmes.Count);
        }
    }
}
