using System.Text.RegularExpressions;
using Dominio.Enums;

namespace Dominio
{
    public class ValidacaoCampos
    {
        private static readonly List<string> _ListaExcessoes = new();

        public static void ValidarCampos(Dictionary<string, dynamic> reservaDict)
        {
            string nome = reservaDict["Nome"];
            string cpf = reservaDict["Cpf"];
            string telefone = reservaDict["Telefone"];
            int idade = reservaDict["Idade"];
            decimal precoEstadia = reservaDict["PrecoEstadia"];
            DateTime checkIn = reservaDict["CheckIn"], checkOut = reservaDict["CheckOut"];

            ValidarNome(nome);
            ValidarCpf(cpf);
            ValidarTelefone(telefone);
            ValidarIdade(idade);
            ValidarCheckIn(checkIn, checkOut);
            ValidarPrecoEstadia(precoEstadia);

            if (_ListaExcessoes.Any())
            {
                string erros = String.Join("\n\n", _ListaExcessoes);
                _ListaExcessoes.Clear();
                throw new Exception(message: erros);
            }
        }

        private static void ValidarNome(string nome)
        {
            string regexNome = @"^[a-zA-Z ]";

            if (String.IsNullOrWhiteSpace(nome))
            {
                _ListaExcessoes.Add(MensagemExcessao.NOME_NULO);
            }
            else if (nome.Length < (int)ValoresValidacaoEnum.tamanhoMinimoNome)
            {
                _ListaExcessoes.Add(MensagemExcessao.NOME_PEQUENO);
            }
            else if (!Regex.IsMatch(nome, regexNome))
            {
                _ListaExcessoes.Add(MensagemExcessao.NOME_CONTEM_NUMEROS_OU_CARACTERES_INVALIDOS);
            }
        }

        private static void ValidarCpf(string cpf)
        {
            string numerosCPF = new(cpf.Where(char.IsDigit).ToArray());

            if (numerosCPF.Length == (int)ValoresValidacaoEnum.ehVazio)
            {
                _ListaExcessoes.Add(MensagemExcessao.CPF_NAO_PREENCHIDO);
            }
            else if (numerosCPF.Length != (int)ValoresValidacaoEnum.tamanhoNumerosCpf)
            {
                _ListaExcessoes.Add(MensagemExcessao.CPF_INVALIDO);
            }
        }
        
        private static void ValidarTelefone(string telefone)
        {
            string numerosTelefone = new(telefone.Where(char.IsDigit).ToArray());

            if (numerosTelefone.Length == (int)ValoresValidacaoEnum.ehVazio)
            {
                _ListaExcessoes.Add(MensagemExcessao.TELEFONE_NAO_PREENCHIDO);
            }
            else if (numerosTelefone.Length != (int)ValoresValidacaoEnum.tamanhoNumerosTelefone)
            {
                _ListaExcessoes.Add(MensagemExcessao.TELEFONE_INVALIDO);
            }
        }
        
        private static void ValidarIdade(int idade)
        {
            bool menordeIdade = idade < (int)ValoresValidacaoEnum.maiorDeIdade;

            if (idade == (int)ValoresValidacaoEnum.codigoDeErro)
            {
                _ListaExcessoes.Add(MensagemExcessao.IDADE_NAO_PREENCHIDA);
            }
            else if (menordeIdade)
            {
                _ListaExcessoes.Add(MensagemExcessao.MENOR_DE_IDADE);
            }
        }
        
        private static void ValidarCheckIn(DateTime checkIn, DateTime checkOut)
        {
            TimeSpan diferencaCheckoutCheckIn = checkOut - checkIn;
            string stringDiferencaCheckoutCheckIn = diferencaCheckoutCheckIn.ToString();
            bool dataCheckOutAntesDoCheckIn = stringDiferencaCheckoutCheckIn[0].Equals('-');

            if (dataCheckOutAntesDoCheckIn)
            {
                _ListaExcessoes.Add(MensagemExcessao.CHECKOUT_EM_DATAS_PASSADAS);
            }
        }
        
        private static void ValidarPrecoEstadia(decimal precoEstadia)
        {
            if (precoEstadia == (int)ValoresValidacaoEnum.codigoDeErro)
            {
                _ListaExcessoes.Add(MensagemExcessao.PRECO_DA_ESTADIA_NAO_PREENCHIDO);
            }
        }   
    }
}