using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema_de_Reservas_para_Hoteis
{
    internal class Validacoes
    {
        public static List<string> ListaExcessoes = new();

        public static void ValidarCampos(Reserva reserva)
        {
            // Validar Nome

            if (String.IsNullOrWhiteSpace(reserva.Nome))
            {
                ListaExcessoes.Add(MensagemExcessao.NomeNulo);
            }
            else if (reserva.Nome.Length < 3)
            {
                ListaExcessoes.Add(MensagemExcessao.NomePequeno);
            }

            // Validar CPF
            int TamanhoCpf = 14;

            if (reserva.Cpf.Length != TamanhoCpf)
            {
                ListaExcessoes.Add(MensagemExcessao.CpfNaoPreenchido);
            }

            // Validar Telefone
            int TamanhoTelefone = 15;

            if (reserva.Telefone.Length != TamanhoTelefone)
            {
                ListaExcessoes.Add(MensagemExcessao.TelefoneNaoPreenchido);
            }

            // Validar Idade
            bool MenordeIdade = reserva.Idade < 18 ? true : false;

            if (MenordeIdade)
            {
                ListaExcessoes.Add(MensagemExcessao.MenorDeIdade);
            }

            // Validar datas

            bool DataCheckOutAntesDoCheckIn = reserva.CheckOut.Day - reserva.CheckIn.Day < 0 ? true : false;

            if (DataCheckOutAntesDoCheckIn)
            {
                ListaExcessoes.Add(MensagemExcessao.CheckOutEmDatasPassadas);
            }

            // Validar Preço

            if (!reserva.PrecoEstadia.ToString().Contains(','))
            {
                ListaExcessoes.Add(MensagemExcessao.PrecoEmFormatoInvalido);
            }
            else
            {
                string[] Preco = reserva.PrecoEstadia.ToString().Split(',');
                int IndexCasasDecimais = 1, MaxCasasDecimais = 2;

                if (Preco[IndexCasasDecimais].Length != MaxCasasDecimais)
                {
                    ListaExcessoes.Add(MensagemExcessao.PrecoEmFormatoInvalido);
                }
            }

            if ((ListaExcessoes == null) || (!ListaExcessoes.Any()))
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
