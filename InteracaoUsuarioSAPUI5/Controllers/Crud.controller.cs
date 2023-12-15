using Dominio;
using Dominio.Extensoes;
using FluentValidation;
using Infraestrutura;
using Microsoft.AspNetCore.Mvc;
namespace InteracaoUsuarioSAPUI5.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Crud : ControllerBase
    {
        private static readonly IRepositorio _repositorio = new RepositorioLinq2DB();
        private static readonly IValidator<Reserva> _validacao = new ReservaFluentValidation(_repositorio);

        [HttpGet]
        public List<Reserva> ObterTodos()
        {
            try
            {
                return _repositorio.ObterTodos();
            }
            catch (Exception erro)
            {
                throw new Exception(message: erro.Message);
            }
        }

        [HttpGet("{id}")]
        public Reserva ObterPorId(int id)
        {
            try
            {
                return _repositorio.ObterPorId(id);
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
        public OkResult AtualizarReserva([FromBody] Reserva reserva)
        {
            try
            {
                if (reserva == null)
                {
                    throw new Exception();
                }
                _validacao.ValidateAndThrowArgumentException(reserva);
                _repositorio.Atualizar(reserva);
                return Ok();
            }
            catch (Exception erro)
            {
                throw new Exception(message: erro.Message);
            }
        }

        [HttpDelete("{id}")]
        public OkResult RemoverReserva(int id)
        {
            try
            {
                _repositorio.Remover(id);
                return Ok();
            }
            catch (Exception erro)
            {
                throw new Exception(message: erro.Message);
            }
        }
    }
}