using Dominio;
using Dominio.Extensoes;
using FluentValidation;
using Infraestrutura;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
namespace InteracaoUsuarioSAPUI5.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservaController : ControllerBase
    {
        private static readonly IRepositorio _repositorio = new RepositorioLinq2DB();
        private static readonly IValidator<Reserva> _validacao = new ReservaFluentValidation(_repositorio);

        [HttpGet]
        public OkObjectResult ObterTodos()
        {
            try
            {
                return Ok(_repositorio.ObterTodos());
            }
            catch (Exception erro)
            {
                throw new Exception(message: erro.Message);
            }
        }

        [HttpGet("{id}")]
        public OkObjectResult ObterPorId(int id)
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
                _validacao.ValidateAndThrowArgumentException(reserva);
                _repositorio.Criar(reserva);
                
                return Created($"reserva/{reserva.Id}", reserva);
            }
            catch (Exception erro)
            {
                throw new Exception(message: erro.Message);
            }
        }

        [HttpPut]
        public NoContentResult AtualizarReserva([FromBody] Reserva reserva)
        {
            try
            {
                if (reserva == null)
                {
                    throw new Exception();
                }
                _validacao.ValidateAndThrowArgumentException(reserva);
                _repositorio.Atualizar(reserva);

                return NoContent();
            }
            catch (Exception erro)
            {
                throw new Exception(message: erro.Message);
            }
        }

        [HttpDelete("{id}")]
        public NoContentResult RemoverReserva(int id)
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