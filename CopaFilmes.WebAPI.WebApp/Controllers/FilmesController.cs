using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CopaFilmes.WebAPI.DAL;
using CopaFilmes.WebAPI.Model;
using Microsoft.AspNetCore.Mvc;

namespace CopaFilmes.WebAPI.WebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilmesController : ControllerBase
    {

        [HttpGet]
        public async Task<IActionResult> Get()
        {            
            // Consulta uma lista de filmes
            var filmes = await FilmesDal.getListaFilmes();
            return Ok(filmes);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] string[] ids)
        {
            if(ids.Length != 8)
            {
                return BadRequest("Não foram recebidos um post com 8 filmes");
            }
            var filmes = await FilmesDal.getListaFilmes();
            var filmesSelecionados = filmes.filtroPorIds(ids);
            if (filmesSelecionados.Count() != 8)
            {
                return BadRequest("Alguns ids recebidos são inválidos ou não foram localizados");
            }
            List<Filme> filmesFinais = filmesSelecionados
               .ToList()
               .getListDisputa() // Monta um list com as disputas
               .getListRodada() // Quartas de finais
               .getListRodada() // Finais
               .getOrdenacaoNotaDesc();
            return Ok(filmesFinais);
        }

    }
}