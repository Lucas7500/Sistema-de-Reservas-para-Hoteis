using FluentValidation;
using Dominio.Enums;

namespace Dominio
{
    public class ValidacaoReserva : AbstractValidator<Reserva>
    {
        public ValidacaoReserva(Reserva reserva)
        {
            RuleFor(reserva => reserva.Nome)
                .NotEmpty().WithMessage(MensagemExcessao.NOME_NULO)
                .MinimumLength((int)ValoresValidacaoEnum.tamanhoMinimoNome).WithMessage(MensagemExcessao.NOME_PEQUENO)
                .MaximumLength(50).WithMessage("* O Nome do Cliente não pode superar 50 caracteres!")
                .Matches("[a-zA-ZáàâãéèêíìîóòõôúùûçÁÀÃÂÉÈÊÍÌÎÓÒÔÕÚÙÛÇ ]").WithMessage(MensagemExcessao.NOME_CONTEM_NUMEROS_OU_CARACTERES_INVALIDOS);

            RuleFor(reserva => reserva.Cpf)
            .NotEmpty().WithMessage(MensagemExcessao.CPF_NAO_PREENCHIDO)
            .Matches(@$"[^\d{3}.?\d{3}.?\d{3}-?\d{2}$]").WithMessage(MensagemExcessao.CPF_INVALIDO)
            .Must(CpfEhValido);

            RuleFor(reserva => reserva.Telefone)
                .NotEmpty().WithMessage(MensagemExcessao.TELEFONE_NAO_PREENCHIDO)
                .Matches(@$"[^(?\d{2})?\d{5}-?\d{4}$]").WithMessage(MensagemExcessao.TELEFONE_INVALIDO);

            RuleFor(reserva => reserva.Idade)
                .NotEmpty().WithMessage(MensagemExcessao.IDADE_NAO_PREENCHIDA)
                .GreaterThanOrEqualTo((int)ValoresValidacaoEnum.maiorDeIdade).WithMessage(MensagemExcessao.MENOR_DE_IDADE)
                .LessThan(200).WithMessage("* A Idade digitada é inválida!");

            RuleFor(reserva => reserva.Sexo)
                .NotEmpty().WithMessage("* O Cliente deve possuir um sexo!")
                .Must(SexoEhValido).WithMessage("* O Sexo do cliente é inválido!");

            RuleFor(reserva => reserva.CheckIn)
                .NotNull().WithMessage("* A Data de Check-in não pode ser nula!");

            RuleFor(reserva => reserva.CheckOut)
                .NotNull().WithMessage("* A Data de Check-out não pode ser nula!")
                .GreaterThan(reserva => reserva.CheckIn).WithMessage(MensagemExcessao.CHECKOUT_EM_DATAS_PASSADAS);

            RuleFor(reserva => reserva.PrecoEstadia)
                .NotEmpty().WithMessage(MensagemExcessao.PRECO_DA_ESTADIA_NAO_PREENCHIDO)
                .GreaterThanOrEqualTo(0).WithMessage("* O Preço da Estadia não pode ser negativo!")
                .LessThanOrEqualTo(9999999999.99M).WithMessage("* O Preço da Estadia está acima do intervalo permitido!");

            RuleFor(reserva => reserva.PagamentoEfetuado)
                .NotNull().WithMessage("* Informe se o Preço da Estadia foi pago!");
        }

        private static bool CpfEhValido(string cpf)
        {
            
        }
        
        private static bool SexoEhValido(GeneroEnum @enum)
        {
            return @enum == GeneroEnum.Masculino || @enum == GeneroEnum.Feminino;
        }
    }
}

