using System.Text.RegularExpressions;

namespace Sistema_de_Reservas_para_Hoteis
{
    public class Validacoes
    {
        private static readonly List<string> ListaExcessoes = new();
        public const int codigoDeErro = -1;
        const int tamanhoMinimoNome = 3;
        const int tamanhoNumerosCpf = 11;
        const int tamanhoNumerosTelefone = 11;
        const int idadeAdulto = 18;
        const int ehVazio = 0;
        readonly static string regexNome = @"^[a-zA-Z ]";


        public static void ValidarCampos(Dictionary<string, dynamic> reservaDict)
        {
            string nome = reservaDict["Nome"], cpf = reservaDict["Cpf"], telefone = reservaDict["Telefone"], sexo = reservaDict["Sexo"];
            int idade = reservaDict["Idade"];
            decimal precoEstadia = reservaDict["PrecoEstadia"];
            DateTime checkIn = reservaDict["CheckIn"], checkOut = reservaDict["CheckOut"];
            string pagamentoEfetuado = reservaDict["PagamentoEfetuado"];

            string numerosCPF = new(cpf.Where(char.IsDigit).ToArray());
            string numerosTelefone = new(telefone.Where(char.IsDigit).ToArray());
            bool menordeIdade = idade < idadeAdulto;
            TimeSpan diferencaCheckoutCheckIn = checkOut - checkIn;
            string stringDiferencaCheckoutCheckIn = diferencaCheckoutCheckIn.ToString();
            bool dataCheckOutAntesDoCheckIn = stringDiferencaCheckoutCheckIn[0].Equals('-');
            bool sexoInvalido = sexo != "Masculino" && sexo != "Feminino";

            if (String.IsNullOrWhiteSpace(nome))
            {
                ListaExcessoes.Add(MensagemExcessao.NomeNulo);
            }
            else if (nome.Length < tamanhoMinimoNome)
            {
                ListaExcessoes.Add(MensagemExcessao.NomePequeno);
            }
            else if (!Regex.IsMatch(nome, regexNome))
            {
                ListaExcessoes.Add(MensagemExcessao.NomeContemNumero);
            }

            if (numerosCPF.Length == ehVazio)
            {
                ListaExcessoes.Add(MensagemExcessao.CpfNaoPreenchido);
            }
            else if (numerosCPF.Length != tamanhoNumerosCpf)
            {
                ListaExcessoes.Add(MensagemExcessao.CpfInvalido);
            }

            if (numerosTelefone.Length == ehVazio)
            {
                ListaExcessoes.Add(MensagemExcessao.TelefoneNaoPreenchido);
            }
            else if (numerosTelefone.Length != tamanhoNumerosTelefone)
            {
                ListaExcessoes.Add(MensagemExcessao.TelefoneInvalido);
            }

            if (idade == codigoDeErro)
            {
                ListaExcessoes.Add(MensagemExcessao.IdadeNaoPreenchida);
            }
            else if (menordeIdade)
            {
                ListaExcessoes.Add(MensagemExcessao.MenorDeIdade);
            }

            if (sexoInvalido)
            {
                ListaExcessoes.Add(MensagemExcessao.SexoInvalido);
            }

            if (dataCheckOutAntesDoCheckIn)
            {
                ListaExcessoes.Add(MensagemExcessao.CheckOutEmDatasPassadas);
            }

            if (precoEstadia == codigoDeErro)
            {
                ListaExcessoes.Add(MensagemExcessao.PrecoNaoPreenchido);
            }

            if (String.IsNullOrEmpty(pagamentoEfetuado))
            {
                ListaExcessoes.Add(MensagemExcessao.PagamentoNaoInformado);
            }

            if (ListaExcessoes.Any())
            {
                string erros = String.Join("\n\n", ListaExcessoes);
                ListaExcessoes.Clear();
                throw new Exception(message: erros);
            }
        }
    }
}