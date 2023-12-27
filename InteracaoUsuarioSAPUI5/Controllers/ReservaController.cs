using Dominio;
using Dominio.Constantes;
using Dominio.Extensoes;
using FluentValidation;
using Infraestrutura;
using Microsoft.AspNetCore.Mvc;
namespace InteracaoUsuarioSAPUI5.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservaController : ControllerBase
    {
        private readonly IRepositorio _repositorio;
        private readonly IValidator<Reserva> _validador;

        public ReservaController(IRepositorio repositorio, IValidator<Reserva> validador)
        {
            _repositorio = repositorio;
            _validador = validador;
        }
        
        [HttpGet]
        public OkObjectResult ObterTodos([FromQuery] string? filtro)
        {
            try
            {
                List<Reserva> reservas = new();

                if (!string.IsNullOrEmpty(filtro))
                {
                    reservas.AddRange(_repositorio.ObterTodos().Where(reserva => 
                    reserva.Nome.Contains(filtro, StringComparison.OrdinalIgnoreCase) ||
                    reserva.Cpf.Contains(filtro, StringComparison.OrdinalIgnoreCase)));
                }
                else
                {
                    reservas = _repositorio.ObterTodos();
                }

                return Ok(reservas);
            }
            catch (Exception erro)
            {
                throw new Exception(message: erro.Message);
            }
        }

        [HttpGet("{id}")]
        public OkObjectResult ObterPorId([FromRoute] int id)
        {
            try
            {
                return Ok(_repositorio.ObterPorId(id));
            }
            catch (Exception erro)
            {
                throw new Exception(message: erro.Message);
            }
        }

        [HttpPost]
        public CreatedResult CriarReserva([FromBody] Reserva reserva)
        {
            try
            {
                if (reserva == null)
                {
                    throw new Exception();
                }
                reserva.Id = ValoresPadrao.ID_NULO;
                _validador.ValidateAndThrowArgumentException(reserva);
                _repositorio.Criar(reserva);

                return Created($"reserva/{reserva.Id}", reserva);
            }
            catch (Exception erro)
            {
                throw new Exception(message: erro.Message);
            }
        }

        [HttpPut("{id}")]
        public NoContentResult AtualizarReserva([FromRoute] int id, [FromBody] Reserva reserva)
        {
            try
            {
                if (reserva == null || reserva.Id != id)
                {
                    throw new Exception(message: MensagemExcessao.RESERVA_INVALIDA_OU_INEXISTENTE);
                }
                _validador.ValidateAndThrowArgumentException(reserva);
                _repositorio.Atualizar(reserva);

                return NoContent();
            }
            catch (Exception erro)
            {
                throw new Exception(message: erro.Message);
            }
        }

        [HttpDelete("{id}")]
        public NoContentResult RemoverReserva([FromRoute] int id)
        {
            try
            {
                _repositorio.Remover(id);

                return NoContent();
            }
            catch (Exception erro)
            {
                throw new Exception(message: erro.Message);
            }
        }
    }
}