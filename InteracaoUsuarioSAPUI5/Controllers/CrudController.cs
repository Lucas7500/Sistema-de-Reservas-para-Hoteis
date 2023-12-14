using Microsoft.AspNetCore.Mvc;
using Dominio;
using FluentValidation;
using Dominio.Extensoes;
using Infraestrutura;

namespace InteracaoUsuarioSAPUI5.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class CrudController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        [HttpPost]
        public IActionResult AdicionarReserva([FromBody] Reserva novaReserva)
        {
            if (novaReserva == null)
            {
                return BadRequest();
            }

            var validacao = new ReservaFluentValidation();
            var repositorio = new RepositorioLinq2DB();

            validacao.ValidateAndThrowArgumentException(novaReserva);
            repositorio.Criar(novaReserva);

            return Created($"reserva/{novaReserva.Id}", novaReserva);
        }   

        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
