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
            if (Convert.ToInt32(idade) < 18)
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

            TimeSpan differenceCheckIn = DataCheckIn - DateTime.Now;
            TimeSpan differenceCheckOut = DataCheckOut - DateTime.Now;
            
            if (differenceCheckIn.TotalDays < 0)
            {
                throw new Exception(message: MensagemExcessao.CheckInEmDatasPassadas);
            }
            else if (differenceCheckOut.TotalDays < 0)
            {
                throw new Exception(message: MensagemExcessao.CheckOutEmDatasPassadas);
            }


            return true;
        }

        public static bool ValidarPreco(string preco)
        {
            if (Decimal.Parse(preco) <= 0)
            {
                throw new Exception(message: MensagemExcessao.PrecoNuloOuNegativo);
            }
            else if (Decimal.Parse(preco) == Convert.ToInt32(preco))
            {
                throw new Exception(message: MensagemExcessao.PrecoInteiro);
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
