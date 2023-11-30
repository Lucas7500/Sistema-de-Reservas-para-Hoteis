using System.Text.RegularExpressions;
using Dominio.Enums;
using FluentValidation;
using FluentValidation.AspNetCore;

namespace Dominio
{
    public class ValidacaoReserva : AbstractValidator<Reserva>
    {
        private static readonly List<string> ListaExcessoes = new();

        public ValidacaoReserva()
        {
            RuleFor(x => x.Nome)
                .NotEmpty().WithMessage("Informe o nome")
                .Length(3, 100).WithMessage("Tamanho errado");
        }
    }
}

