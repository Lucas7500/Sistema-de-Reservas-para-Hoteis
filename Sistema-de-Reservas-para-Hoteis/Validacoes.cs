using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema_de_Reservas_para_Hoteis
{
    public class Validacoes
    {
        private static readonly List<string> ListaExcessoes = new();
        public const int codigoDeErro = -1;

        public static void ValidarCampos(Reserva reserva)
        {
            const int tamanhoMinimoNome = 3;
            const int tamanhoNumerosCpf = 11;
            string numerosCPF = new(reserva.Cpf.Where(char.IsDigit).ToArray());
            const int tamanhoNumerosTelefone = 11;
            string numerosTelefone = new(reserva.Telefone.Where(char.IsDigit).ToArray());
            const int idadeAdulto = 18;
            bool menordeIdade = reserva.Idade < idadeAdulto;
            TimeSpan diferencaCheckoutCheckIn = reserva.CheckOut - reserva.CheckIn;
            string stringDiferencaCheckoutCheckIn = diferencaCheckoutCheckIn.ToString();
            bool dataCheckOutAntesDoCheckIn = stringDiferencaCheckoutCheckIn[0].Equals('-');
            int ehVazio = 0;

            if (String.IsNullOrWhiteSpace(reserva.Nome))
            {
                ListaExcessoes.Add(MensagemExcessao.NomeNulo);
            }
            else if (reserva.Nome.Length < tamanhoMinimoNome)
            {
                ListaExcessoes.Add(MensagemExcessao.NomePequeno);
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

            if (NaoPossuemErros && TelaListaDeReservas.tipoDeModificacao == (int)TelaListaDeReservas.CRUD.Adicionar)
            {
                MessageBox.Show("Reserva foi feita com Sucesso!");
            }
            else if (NaoPossuemErros && TelaListaDeReservas.tipoDeModificacao == (int)TelaListaDeReservas.CRUD.Editar)
            {
                MessageBox.Show("A reserva foi editada com sucesso!");
            }
            else
            {
                MessageBox.Show(String.Join("\n\n", ListaExcessoes), "Erro no Cadastro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ListaExcessoes.Clear();
                throw new Exception();
            }
        }
    }
}