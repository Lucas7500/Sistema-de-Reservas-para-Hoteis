using System.Text.RegularExpressions;
using Dominio.Constantes;
using Dominio.Enums;

namespace Dominio
{
    public class ValidacaoCampos
    {
        private static readonly List<string> _ListaExcessoes = new();

        public static void ValidarCampos(Dictionary<string, dynamic> reservaDict)
        {
            string nome = reservaDict[CamposTabela.COLUNA_NOME];
            string cpf = reservaDict[CamposTabela.COLUNA_CPF];
            string telefone = reservaDict[CamposTabela.COLUNA_TELEFONE];
            int idade = reservaDict[CamposTabela.COLUNA_IDADE];
            decimal precoEstadia = reservaDict[CamposTabela.COLUNA_PRECO_ESTADIA];
            DateTime checkIn = reservaDict[CamposTabela.COLUNA_CHECK_IN];
            DateTime checkOut = reservaDict[CamposTabela.COLUNA_CHECK_OUT];

            ValidarNome(nome);
            ValidarCpf(cpf);
            ValidarTelefone(telefone);
            ValidarIdade(idade);
            ValidarCheckIn(checkIn, checkOut);
            ValidarPrecoEstadia(precoEstadia);

            if (_ListaExcessoes.Any())
            {
                const char quebraDeLinha = '\n';
                string erros = String.Join(quebraDeLinha, _ListaExcessoes);

                _ListaExcessoes.Clear();
                throw new Exception(erros);
            }
        }

        private static void ValidarNome(string nome)
        {
            string regexNome = "^[a-zA-ZáàâãäéèêëíìïóòôõöüúùçñÁÀÂÃÄÉÈÊËÍÌÏÓÒÔÕÖÜÚÙÇÑ ]*$";

            if (String.IsNullOrWhiteSpace(nome))
            {
                _ListaExcessoes.Add(Mensagem.NOME_NAO_PREENCHIDO);
            }
            else if (nome.Length < ValoresPadrao.TAMANHO_MINIMO_NOME)
            {
                _ListaExcessoes.Add(Mensagem.NOME_CURTO);
            }
            else if (nome.Length > ValoresPadrao.TAMANHO_MAXIMO_NOME)
            {
                _ListaExcessoes.Add(Mensagem.NOME_LONGO);
            }
            else if (!Regex.IsMatch(nome, regexNome))
            {
                _ListaExcessoes.Add(Mensagem.NOME_FORMATO_INCORRETO);
            }
        }

        private static void ValidarCpf(string cpf)
        {
            string numerosCPF = new(cpf.Where(char.IsDigit).ToArray());

            if (numerosCPF.Length == ValoresPadrao.EH_VAZIO)
            {
                _ListaExcessoes.Add(Mensagem.CPF_NAO_PREENCHIDO);
            }
            else if (numerosCPF.Length != ValoresPadrao.TAMANHO_NUMEROS_CPF)
            {
                _ListaExcessoes.Add(Mensagem.CPF_INVALIDO);
            }
        }

        private static void ValidarTelefone(string telefone)
        {
            string numerosTelefone = new(telefone.Where(char.IsDigit).ToArray());

            if (numerosTelefone.Length == ValoresPadrao.EH_VAZIO)
            {
                _ListaExcessoes.Add(Mensagem.TELEFONE_NAO_PREENCHIDO);
            }
            else if (numerosTelefone.Length != ValoresPadrao.TAMANHO_NUMEROS_TELEFONE)
            {
                _ListaExcessoes.Add(Mensagem.TELEFONE_INVALIDO);
            }
        }

        private static void ValidarIdade(int idade)
        {
            bool menordeIdade = idade < ValoresPadrao.MAIOR_DE_IDADE;

            if (idade == ValoresPadrao.CODIGO_DE_ERRO)
            {
                _ListaExcessoes.Add(Mensagem.IDADE_NAO_PREENCHIDA);
            }
            else if (menordeIdade)
            {
                _ListaExcessoes.Add(Mensagem.MENOR_DE_IDADE);
            }
        }

        private static void ValidarCheckIn(DateTime checkIn, DateTime checkOut)
        {
            TimeSpan diferencaCheckoutCheckIn = checkOut - checkIn;
            string stringDiferencaCheckoutCheckIn = diferencaCheckoutCheckIn.ToString();

            char ehNegativo = '-';
            byte indiceSinalDiferenca = 0;

            if (stringDiferencaCheckoutCheckIn[indiceSinalDiferenca].Equals(ehNegativo))
            {
                _ListaExcessoes.Add(Mensagem.CHECKOUT_ANTES_CHECK_IN);
            }
        }

        private static void ValidarPrecoEstadia(decimal precoEstadia)
        {
            if (precoEstadia == ValoresPadrao.CODIGO_DE_ERRO)
            {
                _ListaExcessoes.Add(Mensagem.PRECO_DA_ESTADIA_NAO_PREENCHIDO);
            }
        }
    }
}