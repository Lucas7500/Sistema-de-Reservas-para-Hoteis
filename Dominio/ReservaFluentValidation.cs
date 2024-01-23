﻿using Dominio.Constantes;
using Dominio.Enums;
using FluentValidation;
using Infraestrutura;

namespace Dominio
{
    public class ReservaFluentValidation : AbstractValidator<Reserva>
    {
        private static IRepositorio _repositorio;

        public ReservaFluentValidation(IRepositorio repositorio)
        {
            RuleLevelCascadeMode = CascadeMode.Stop;
            _repositorio = repositorio;
            string regexNome = "^[a-zA-ZA-ZáàâãéèêíìîóòõôúùûçÁÀÃÂÉÈÊÍÌÎÓÒÔÕÚÙÛÇ ]*$";

            RuleFor(reserva => reserva.Nome)
                .NotEmpty().WithMessage(MensagemExcessao.NOME_NAO_PREENCHIDO)
                .Matches(regexNome).WithMessage(MensagemExcessao.NOME_FORMATO_INCORRETO)
                .MinimumLength(ValoresPadrao.TAMANHO_MINIMO_NOME).WithMessage(MensagemExcessao.NOME_CURTO)
                .MaximumLength(ValoresPadrao.TAMANHO_MAXIMO_NOME).WithMessage(MensagemExcessao.NOME_LONGO);

            RuleFor(reserva => reserva.Cpf)
            .Must(CpfEstaPreenchido).WithMessage(MensagemExcessao.CPF_NAO_PREENCHIDO)
            .Must(CpfEhValido).WithMessage(MensagemExcessao.CPF_INVALIDO);

            RuleFor(reserva => reserva)
                .Must(CpfEhUnico).WithMessage(MensagemExcessao.CPF_JA_REGISTRADO);

            RuleFor(reserva => reserva.Telefone)
                .Must(TelefoneEstaPreeenchido).WithMessage(MensagemExcessao.TELEFONE_NAO_PREENCHIDO)
                .Must(TelefoneEhValido).WithMessage(MensagemExcessao.TELEFONE_INVALIDO);

            RuleFor(reserva => reserva.Idade)
                .NotEmpty().WithMessage(MensagemExcessao.IDADE_NAO_PREENCHIDA)
                .LessThan(ValoresPadrao.VALOR_MAXIMO_IDADE).WithMessage(MensagemExcessao.IDADE_INVALIDA)
                .GreaterThanOrEqualTo(ValoresPadrao.MAIOR_DE_IDADE).WithMessage(MensagemExcessao.MENOR_DE_IDADE);

            RuleFor(reserva => reserva.Sexo)
                .NotNull().WithMessage(MensagemExcessao.SEXO_NULO)
                .Must(SexoEhValido).WithMessage(MensagemExcessao.SEXO_INVALIDO);

            RuleFor(reserva => reserva.CheckIn)
                .NotNull().WithMessage(MensagemExcessao.CHECKIN_NULO);

            RuleFor(reserva => reserva.CheckOut)
                .NotNull().WithMessage(MensagemExcessao.CHECKOUT_NULO)
                .GreaterThanOrEqualTo(reserva => reserva.CheckIn).WithMessage(MensagemExcessao.CHECKOUT_ANTES_CHECK_IN);

            RuleFor(reserva => reserva.PrecoEstadia)
                .NotEmpty().WithMessage(MensagemExcessao.PRECO_DA_ESTADIA_NAO_PREENCHIDO)
                .LessThanOrEqualTo(ValoresPadrao.VALOR_MAXIMO_PRECO).WithMessage(MensagemExcessao.PRECO_DA_ESTADIA_ACIMA_DO_VALOR_MAXIMO)
                .GreaterThanOrEqualTo(ValoresPadrao.PRECO_NEGATIVO_OU_ZERO).WithMessage(MensagemExcessao.PRECO_DA_ESTADIA_MENOR_IGUAL_A_ZERO);

            RuleFor(reserva => reserva.PagamentoEfetuado)
                .NotNull().WithMessage(MensagemExcessao.PAGAMENTO_EFETUADO_NULO);
        }

        private bool CpfEhUnico(Reserva reserva)
        {
            var reservaMesmoCpf = _repositorio
                .ObterTodos()
                .FirstOrDefault(reservaMesmoCpf => reservaMesmoCpf.Cpf == reserva.Cpf);

            return reservaMesmoCpf == null || reservaMesmoCpf.Id == reserva.Id;
        }

        private bool TelefoneEstaPreeenchido(string telefone)
        {
            string numerosTelefone = new(telefone.Where(char.IsDigit).ToArray());

            return numerosTelefone.Length != ValoresPadrao.EH_VAZIO;
        }

        private bool TelefoneEhValido(string telefone)
        {
            string numerosTelefone = new(telefone.Where(char.IsDigit).ToArray());

            return numerosTelefone.Length == ValoresPadrao.TAMANHO_NUMEROS_TELEFONE;
        }


        private bool CpfEstaPreenchido(string cpf)
        {
            string numerosCpf = new(cpf.Where(char.IsDigit).ToArray());

            return numerosCpf.Length != ValoresPadrao.EH_VAZIO;
        }

        private static bool CpfEhValido(string cpf)
        {
            string numerosCpf = new(cpf.Where(char.IsDigit).ToArray());

            if (numerosCpf.Length != ValoresPadrao.TAMANHO_NUMEROS_CPF)
            {
                return false;
            }

            int[] multiplicadoresPrimeiroDigito = { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicadoresSegundoDigito = { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int primeiroDigitoVerificador = int.Parse(numerosCpf[9].ToString());
            int segundoDigitoVerificador = int.Parse(numerosCpf[10].ToString());
            int somaPrimeiroDigito = 0;
            int somaSegundoDigito = 0;

            for (int i = 0; i < numerosCpf.Length; i++)
            {
                if (i < multiplicadoresPrimeiroDigito.Length)
                {
                    somaPrimeiroDigito += int.Parse(numerosCpf[i].ToString()) * multiplicadoresPrimeiroDigito[i];
                }

                if (i < multiplicadoresSegundoDigito.Length)
                {
                    somaSegundoDigito += int.Parse(numerosCpf[i].ToString()) * multiplicadoresSegundoDigito[i];
                }
            }

            int restoPrimeiroDigito = somaPrimeiroDigito % 11;
            int restoSegundoDigito = somaSegundoDigito % 11;

            bool primeiroCasoInvalido = restoPrimeiroDigito < ValoresPadrao.VALOR_REFERENCIA_RESTO_CPF
                && primeiroDigitoVerificador != ValoresPadrao.DIGITO_ZERO;

            bool segundoCasoInvalido = restoPrimeiroDigito >= ValoresPadrao.VALOR_REFERENCIA_RESTO_CPF
                && primeiroDigitoVerificador != (11 - restoPrimeiroDigito);

            bool terceiroCasoInvalido = restoSegundoDigito < ValoresPadrao.VALOR_REFERENCIA_RESTO_CPF
                && segundoDigitoVerificador != ValoresPadrao.DIGITO_ZERO;

            bool quartoCasoInvalido = restoSegundoDigito >= ValoresPadrao.VALOR_REFERENCIA_RESTO_CPF
                && segundoDigitoVerificador != (11 - restoSegundoDigito);

            return !(primeiroCasoInvalido || segundoCasoInvalido || terceiroCasoInvalido || quartoCasoInvalido);
        }

        private static bool SexoEhValido(GeneroEnum @enum)
        {
            return @enum == GeneroEnum.Masculino || @enum == GeneroEnum.Feminino;
        }
    }
}

