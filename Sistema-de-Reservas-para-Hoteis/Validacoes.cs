using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

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


        public static void ValidarCampos(Reserva reserva, bool edicao)
        {
            string numerosCPF = new(reserva.Cpf.Where(char.IsDigit).ToArray());
            string numerosTelefone = new(reserva.Telefone.Where(char.IsDigit).ToArray());
            bool menordeIdade = reserva.Idade < idadeAdulto;
            TimeSpan diferencaCheckoutCheckIn = reserva.CheckOut - reserva.CheckIn;
            string stringDiferencaCheckoutCheckIn = diferencaCheckoutCheckIn.ToString();
            bool dataCheckOutAntesDoCheckIn = stringDiferencaCheckoutCheckIn[0].Equals('-');

            if (String.IsNullOrWhiteSpace(reserva.Nome))
            {
                ListaExcessoes.Add(MensagemExcessao.NomeNulo);
            }
            else if (reserva.Nome.Length < tamanhoMinimoNome)
            {
                ListaExcessoes.Add(MensagemExcessao.NomePequeno);
            }
            else if (!Regex.IsMatch(reserva.Nome, regexNome))
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

            if (reserva.Idade == codigoDeErro)
            {
                ListaExcessoes.Add(MensagemExcessao.IdadeNaoPreenchida);
            }
            else if (menordeIdade)
            {
                ListaExcessoes.Add(MensagemExcessao.MenorDeIdade);
            }

            if (dataCheckOutAntesDoCheckIn)
            {
                ListaExcessoes.Add(MensagemExcessao.CheckOutEmDatasPassadas);
            }

            if (reserva.PrecoEstadia == codigoDeErro)
            {
                ListaExcessoes.Add(MensagemExcessao.PrecoNaoPreenchido);
            }

            if (reserva.PagamentoEfetuado == null)
            {
                ListaExcessoes.Add(MensagemExcessao.PagamentoNaoInformado);
            }

            bool NaoPossuemErros = (ListaExcessoes == null) || (!ListaExcessoes.Any());

            if (NaoPossuemErros && !edicao)
            {
                MessageBox.Show("Reserva foi feita com Sucesso!");
            }
            else if (NaoPossuemErros && edicao)
            {
                MessageBox.Show("A reserva foi editada com sucesso!");
            }
            else
            {
                string erros = String.Join("\n\n", ListaExcessoes);
                ListaExcessoes.Clear();
                throw new Exception(message: erros);
            }
        }
    }
}