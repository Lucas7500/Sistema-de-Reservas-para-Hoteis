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
        public static List<string> ListaExcessoes = new();

        public static void ValidarCampos(Reserva reserva)
        {
            const int CodigoErro = -1;
            const int TamanhoMinimoNome = 3;
            const int TamanhoCpf = 14;
            string NumerosCPF = reserva.Cpf.Trim(new Char[] { '_', '-', '.', ' ' });
            const int TamanhoTelefone = 15;
            string NumerosTelefone = reserva.Telefone.Trim(new Char[] { '(', ')', '-', ' ' });
            const int IdadeAdulto = 18;
            bool MenordeIdade = reserva.Idade < IdadeAdulto;
            bool DataCheckOutAntesDoCheckIn = reserva.CheckOut.Day - reserva.CheckIn.Day < 0;

            if (String.IsNullOrWhiteSpace(reserva.Nome))
            {
                ListaExcessoes.Add(MensagemExcessao.NomeNulo);
            }
            else if (reserva.Nome.Length < TamanhoMinimoNome)
            {
                ListaExcessoes.Add(MensagemExcessao.NomePequeno);
            }

            if (NumerosCPF.Length == 0)
            {
                ListaExcessoes.Add(MensagemExcessao.CpfNaoPreenchido);
            }
            else if (reserva.Cpf.Length != TamanhoCpf || reserva.Cpf.Contains('_'))
            {
                ListaExcessoes.Add(MensagemExcessao.CpfInvalido);
            }

            if (NumerosTelefone.Length == 0)
            {
                ListaExcessoes.Add(MensagemExcessao.TelefoneNaoPreenchido);
            }
            else if (reserva.Telefone.Length != TamanhoTelefone || reserva.Telefone.Contains('_'))
            {
                ListaExcessoes.Add(MensagemExcessao.TelefoneInvalido);
            }

            if (reserva.Idade == CodigoErro)
            {
                Validacoes.ListaExcessoes.Add(MensagemExcessao.IdadeNaoPreenchida);
            }   
            else if (MenordeIdade)
            {
                ListaExcessoes.Add(MensagemExcessao.MenorDeIdade);
            }

            if (DataCheckOutAntesDoCheckIn)
            {
                ListaExcessoes.Add(MensagemExcessao.CheckOutEmDatasPassadas);
            }

            if (reserva.PrecoEstadia == CodigoErro)
            {
                Validacoes.ListaExcessoes.Add(MensagemExcessao.PrecoNaoPreenchido);
            }

            if (reserva.PagamentoEfetuado == null)
            {
                Validacoes.ListaExcessoes.Add(MensagemExcessao.PagamentoNaoInformado);
            }

            bool NaoPossuemErros = (ListaExcessoes == null) || (!ListaExcessoes.Any());

            if (NaoPossuemErros)
            {
                MessageBox.Show("Reserva foi feita com Sucesso!");
            }
            else
            {
                throw new Exception();
            }
        }
      
    }
}
