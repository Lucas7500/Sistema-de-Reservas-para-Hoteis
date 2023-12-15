using Infraestrutura;
using Microsoft.AspNetCore.Mvc;
using FluentValidation;
using Dominio;
using Dominio.Extensoes;

namespace InteracaoUsuarioSAPUI5.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Crud : ControllerBase
    {
        private readonly IRepositorio _repositorio = new RepositorioLinq2DB();
        private readonly IValidator<Reserva> _validacao = new ReservaFluentValidation();

        [HttpGet]
        public List<Reserva> ObterTodos()
        {
            return _repositorio.ObterTodos();
        }

        [HttpGet("{id}")]
        public Reserva ObterPorId(int id)
        {
            return _repositorio.ObterPorId(id);
        }

        [HttpPost]
        public IActionResult AdicionarReserva([FromBody] Reserva reserva)
        {
            if (reserva == null)
            {
                return BadRequest();
            }

            //_validacao.ValidateAndThrowArgumentException(reserva);
            _repositorio.Criar(reserva);
            return Created($"reserva/{reserva.Id}", reserva);
        }

        [HttpPut]
        public IActionResult AtualizarReserva([FromBody] Reserva reserva)
        {
            if (reserva == null)
            {
                return BadRequest();
            }
            //_validacao.ValidateAndThrowArgumentException(reserva);
            _repositorio.Atualizar(reserva);
            return Created($"reserva/{reserva.Id}", reserva);
        }

        [HttpDelete("{id}")]
        public IActionResult RemoverReserva(int id)
        {
            _repositorio.Remover(id);
            return Ok();
        }
    }
}