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
            const int tamanhoMinimoNome = 3;
            const int tamanhoNumerosCpf = 11;
            const int tamanhoCpfFormatado = 14;
            string numerosCPF = reserva.Cpf.Trim(new Char[] { '_', '-', '.', ' ' });
            const int tamanhoNumerosTelefone = 11;
            const int tamanhoTelefoneFormatado = 14;
            string numerosTelefone = reserva.Telefone.Trim(new Char[] { '(', ')', '-', ' ' });
            const int idadeAdulto = 18;
            bool menordeIdade = reserva.Idade < idadeAdulto;
            bool dataCheckOutAntesDoCheckIn = reserva.CheckOut.Day - reserva.CheckIn.Day < 0;
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
            else if (numerosCPF.Length != tamanhoNumerosCpf && numerosCPF.Length != tamanhoCpfFormatado)
            {
                ListaExcessoes.Add(MensagemExcessao.CpfInvalido);
            }

            if (numerosTelefone.Length == ehVazio)
            {
                ListaExcessoes.Add(MensagemExcessao.TelefoneNaoPreenchido);
            }
            else if (numerosTelefone.Length != tamanhoNumerosTelefone && numerosTelefone.Length != tamanhoTelefoneFormatado)
            {
                ListaExcessoes.Add(MensagemExcessao.TelefoneInvalido);
            }

            if (reserva.Idade == CadastroCliente.codigoDeErro)
            {
                Validacoes.ListaExcessoes.Add(MensagemExcessao.IdadeNaoPreenchida);
            }   
            else if (menordeIdade)
            {
                ListaExcessoes.Add(MensagemExcessao.MenorDeIdade);
            }

            if (dataCheckOutAntesDoCheckIn)
            {
                ListaExcessoes.Add(MensagemExcessao.CheckOutEmDatasPassadas);
            }

            if (reserva.PrecoEstadia == CadastroCliente.codigoDeErro)
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
