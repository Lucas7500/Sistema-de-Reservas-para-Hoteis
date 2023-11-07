using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema_de_Reservas_para_Hoteis
{
    internal class Validacoes
    {
        public static bool ValidarNome(string nome)
        {
            if (String.IsNullOrWhiteSpace(nome)) 
            {
                throw new Exception(message: MensagemExcessao.NomeNulo);
            }
            else if (nome.Length < 3) 
            {
                throw new Exception(message: MensagemExcessao.NomePequeno);
            }

            return true;
        }

        public static bool ValidarCPF(MaskedTextBox CPF)
        {
            if (!CPF.MaskCompleted)
            {
                throw new Exception(message: MensagemExcessao.CpfNaoPreenchido);
            }

            return true;
        }

        public static bool ValidarIdade(string idade)
        {
            if (String.IsNullOrWhiteSpace(idade))
            {
                throw new Exception(message: MensagemExcessao.IdadeNaoPreenchida);
            }
            else if (int.Parse(idade) < 18)
            {
                throw new Exception(message: MensagemExcessao.MenorDeIdade);
            }

            return true;
        }

        public static bool ValidarTelefone(MaskedTextBox telefone)
        {
            if (!telefone.MaskCompleted)
            {
                throw new Exception(message: MensagemExcessao.TelefoneNaoPreenchido);
            }

            return true;
        }

        public static bool ValidarDatas(DateTimePicker CheckIn, DateTimePicker CheckOut)
        {
            DateTime DataCheckIn = Convert.ToDateTime(CheckIn.Value.Date);
            DateTime DataCheckOut = Convert.ToDateTime(CheckOut.Value.Date);

            int differenceCheckIn = DataCheckIn.Day - DateTime.Now.Day;
            int differenceCheckOut = DataCheckOut.Day - DataCheckIn.Day;

            if (differenceCheckIn < 0)
            {
                throw new Exception(message: MensagemExcessao.CheckInEmDatasPassadas);
            }
            else if (differenceCheckOut < 0)
            {
                throw new Exception(message: MensagemExcessao.CheckOutEmDatasPassadas);
            }


            return true;
        }

        public static bool ValidarPreco(string preco)
        {
            if (String.IsNullOrWhiteSpace(preco))
            {
                throw new Exception(message: MensagemExcessao.PrecoNaoPreenchido);
            }
            
            if(!preco.Contains(','))
            {
                throw new Exception(message: MensagemExcessao.PrecoNaoEDecimal);
            }
            else
            {
                string[] strings = preco.Split(',');

                if (strings.Length !=  2)
                {
                    throw new Exception(message: MensagemExcessao.PrecoNaoEDecimal);
                }
                else if (strings[1].Length != 2)
                {
                    throw new Exception(message: MensagemExcessao.PrecoNaoEDecimal);
                }
            }

            return true;
        }

        public static bool ValidarPagamento(RadioButton BotaoTrue, RadioButton BotaoFalse)
        {
            if (!BotaoTrue.Checked && !BotaoFalse.Checked)
            {
                throw new Exception(message: MensagemExcessao.PagamentoNaoInformado);
            }

            return true;
        }


    }
}
