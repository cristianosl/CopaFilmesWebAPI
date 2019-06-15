using System.Threading.Tasks;
using CopaFilmes.WebAPI.DAL;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace CopaFilmes.WebAPI.WebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilmesController : ControllerBase
    {

        [HttpGet]
        [DisableCors]
        public async Task<IActionResult> Get()
        {            
            // Consulta uma lista de filmes
            var filmes = await FilmesDal.getListaFilmes();
            return Ok(filmes);
        }
    }
}