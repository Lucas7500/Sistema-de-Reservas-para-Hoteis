using System.Text.RegularExpressions;
using Dominio.Enums;

namespace Dominio
{
    public class ValidacaoCampos
    {
        private static readonly List<string> ListaExcessoes = new();

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

            if (ListaExcessoes.Any())
            {
                string erros = String.Join("\n\n", ListaExcessoes);
                ListaExcessoes.Clear();
                throw new Exception(message: erros);
            }
        }

        private static void ValidarNome(string nome)
        {
            string regexNome = @"^[a-zA-Z ]";

            if (String.IsNullOrWhiteSpace(nome))
            {
                ListaExcessoes.Add(MensagemExcessao.NOME_NULO);
            }
            else if (nome.Length < (int)ValoresValidacaoEnum.tamanhoMinimoNome)
            {
                ListaExcessoes.Add(MensagemExcessao.NOME_PEQUENO);
            }
            else if (!Regex.IsMatch(nome, regexNome))
            {
                ListaExcessoes.Add(MensagemExcessao.NOME_CONTEM_NUMEROS);
            }
        }

        private static void ValidarCpf(string cpf)
        {
            string numerosCPF = new(cpf.Where(char.IsDigit).ToArray());

            if (numerosCPF.Length == (int)ValoresValidacaoEnum.ehVazio)
            {
                ListaExcessoes.Add(MensagemExcessao.CPF_NAO_PREENCHIDO);
            }
            else if (numerosCPF.Length != (int)ValoresValidacaoEnum.tamanhoNumerosCpf)
            {
                ListaExcessoes.Add(MensagemExcessao.CPF_INVALIDO);
            }
        }
        
        private static void ValidarTelefone(string telefone)
        {
            string numerosTelefone = new(telefone.Where(char.IsDigit).ToArray());

            if (numerosTelefone.Length == (int)ValoresValidacaoEnum.ehVazio)
            {
                ListaExcessoes.Add(MensagemExcessao.TELEFONE_NAO_PREENCHIDO);
            }
            else if (numerosTelefone.Length != (int)ValoresValidacaoEnum.tamanhoNumerosTelefone)
            {
                ListaExcessoes.Add(MensagemExcessao.TELEFONE_INVALIDO);
            }
        }
        
        private static void ValidarIdade(int idade)
        {
            bool menordeIdade = idade < (int)ValoresValidacaoEnum.idadeAdulto;

            if (idade == (int)ValoresValidacaoEnum.codigoDeErro)
            {
                ListaExcessoes.Add(MensagemExcessao.IDADE_NAO_PREENCHIDA);
            }
            else if (menordeIdade)
            {
                ListaExcessoes.Add(MensagemExcessao.MENOR_DE_IDADE);
            }
        }
        
        private static void ValidarCheckIn(DateTime checkIn, DateTime checkOut)
        {
            TimeSpan diferencaCheckoutCheckIn = checkOut - checkIn;
            string stringDiferencaCheckoutCheckIn = diferencaCheckoutCheckIn.ToString();
            bool dataCheckOutAntesDoCheckIn = stringDiferencaCheckoutCheckIn[0].Equals('-');

            if (dataCheckOutAntesDoCheckIn)
            {
                ListaExcessoes.Add(MensagemExcessao.CHECKOUT_EM_DATAS_PASSADAS);
            }
        }
        
        private static void ValidarPrecoEstadia(decimal precoEstadia)
        {
            if (precoEstadia == (int)ValoresValidacaoEnum.codigoDeErro)
            {
                ListaExcessoes.Add(MensagemExcessao.PRECO_DA_ESTADIA_NAO_PREENCHIDO);
            }
        }   
    }
}