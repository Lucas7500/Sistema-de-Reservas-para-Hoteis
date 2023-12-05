using Dominio.Constantes;
using Dominio.Enums;
using FluentValidation;
using System.Text.RegularExpressions;

namespace Dominio
{
    public class ValidacaoReserva : AbstractValidator<Reserva>
    {
        public ValidacaoReserva()
        {
            RuleLevelCascadeMode = CascadeMode.Stop;

            RuleFor(reserva => reserva.Nome)
                .NotEmpty().WithMessage(MensagemExcessao.NOME_NAO_PREENCHIDO)
                .Matches(PadroesRegex.REGEX_NOME).WithMessage(MensagemExcessao.NOME_FORMATO_INCORRETO)
                .MinimumLength(ConstantesValidacao.TAMANHO_MINIMO_NOME).WithMessage(MensagemExcessao.NOME_CURTO)
                .MaximumLength(ConstantesValidacao.TAMANHO_MAXIMO_NOME).WithMessage(MensagemExcessao.NOME_LONGO);

            RuleFor(reserva => reserva.Cpf)
            .Must(CpfEstaPreenchido).WithMessage(MensagemExcessao.CPF_NAO_PREENCHIDO)
            .Must(CpfEhValido).WithMessage(MensagemExcessao.CPF_INVALIDO);

            RuleFor(reserva => reserva.Telefone)
                .Must(TelefoneEstaPreeenchido).WithMessage(MensagemExcessao.TELEFONE_NAO_PREENCHIDO)
                .Must(TelefoneEhValido).WithMessage(MensagemExcessao.TELEFONE_INVALIDO);

            RuleFor(reserva => reserva.Idade)
                .NotEmpty().WithMessage(MensagemExcessao.IDADE_NAO_PREENCHIDA)
                .LessThan(ConstantesValidacao.VALOR_MAXIMO_IDADE).WithMessage(MensagemExcessao.IDADE_INVALIDA)
                .GreaterThanOrEqualTo(ConstantesValidacao.MAIOR_DE_IDADE).WithMessage(MensagemExcessao.MENOR_DE_IDADE);

            RuleFor(reserva => reserva.Sexo)
                .NotNull().WithMessage(MensagemExcessao.SEXO_NULO)
                .Must(SexoEhValido).WithMessage(MensagemExcessao.SEXO_INVALIDO);

            RuleFor(reserva => reserva.CheckIn)
                .NotNull().WithMessage(MensagemExcessao.CHECKIN_NULO);

            RuleFor(reserva => reserva.CheckOut)
                .NotNull().WithMessage(MensagemExcessao.CHECKOUT_NULO)
                .GreaterThanOrEqualTo(reserva => reserva.CheckIn).WithMessage(MensagemExcessao.CHECKOUT_EM_DATAS_PASSADAS);

            RuleFor(reserva => reserva.PrecoEstadia)
                .NotEmpty().WithMessage(MensagemExcessao.PRECO_DA_ESTADIA_NAO_PREENCHIDO)
                .LessThanOrEqualTo(ConstantesValidacao.VALOR_MAXIMO_PRECO).WithMessage(MensagemExcessao.PRECO_DA_ESTADIA_ACIMA_DO_VALOR_MAXIMO)
                .GreaterThanOrEqualTo(ConstantesValidacao.PRECO_NEGATIVO_OU_ZERO).WithMessage(MensagemExcessao.PRECO_DA_ESTADIA_MENOR_IGUAL_A_ZERO);

            RuleFor(reserva => reserva.PagamentoEfetuado)
                .NotNull().WithMessage(MensagemExcessao.PAGAMENTO_EFETUADO_NULO);
        }

        private bool TelefoneEstaPreeenchido(string telefone)
        {
            string numerosTelefone = new(telefone.Where(char.IsDigit).ToArray());

            if (numerosTelefone.Length == ConstantesValidacao.EH_VAZIO)
            {
                return false;
            }

            return true;
        }

        private bool TelefoneEhValido(string telefone)
        {
            string numerosTelefone = new(telefone.Where(char.IsDigit).ToArray());

            if (numerosTelefone.Length != ConstantesValidacao.TAMANHO_NUMEROS_TELEFONE)
            {
                return false;
            }

            return true;
        }


        private bool CpfEstaPreenchido(string cpf)
        {
            string numerosCpf = new(cpf.Where(char.IsDigit).ToArray());

            if (numerosCpf.Length == ConstantesValidacao.EH_VAZIO)
            {
                return false;
            }

            return true;
        }

        private static bool CpfEhValido(string cpf)
        {
            string numerosCpf = new(cpf.Where(char.IsDigit).ToArray());
            int[] multiplicacoesPrimeiroDigito = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int somaPrimeiroDigito = 0;
            int resto;

            for (int i = 0; i < multiplicacoesPrimeiroDigito.Length; i++)
            {
                somaPrimeiroDigito += int.Parse(numerosCpf[i].ToString()) * multiplicacoesPrimeiroDigito[i];
            }

            int primeiroDigitoVerificador = int.Parse(numerosCpf[9].ToString());
            resto = somaPrimeiroDigito % 11;

            if (resto < 2)
            {
                if (primeiroDigitoVerificador != 0) return false;
            }
            else
            {
                if (primeiroDigitoVerificador != (11 - resto)) return false;
            }

            int[] multiplicacoesSegundoDigito = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int somaSegundoDigito = 0;

            for (int i = 0; i < multiplicacoesSegundoDigito.Length; i++)
            {
                somaSegundoDigito += int.Parse(numerosCpf[i].ToString()) * multiplicacoesSegundoDigito[i];
            }

            int segundoDigitoVerificador = int.Parse(numerosCpf[10].ToString()); ;
            resto = somaSegundoDigito % 11;

            if (resto < 2)
            {
                if (segundoDigitoVerificador != '0') return false;
            }
            else
            {
                if (segundoDigitoVerificador != (11 - resto)) return false;
            }

            return true;
        }

        private static bool SexoEhValido(GeneroEnum @enum)
        {
            return @enum == GeneroEnum.Masculino || @enum == GeneroEnum.Feminino;
        }
    }
}

