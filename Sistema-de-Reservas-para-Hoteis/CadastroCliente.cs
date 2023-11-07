using Sistema_de_Reservas_para_Hoteis.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sistema_de_Reservas_para_Hoteis
{
    public partial class CadastroCliente : Form
    {
        public CadastroCliente()
        {
            InitializeComponent();
            CaixaSexo.DataSource = Enum.GetValues(typeof(GeneroEnum));
        }

        Reserva reserva = new Reserva();

        private bool LerDadosDaReserva()
        {
            try
            {
                if (Validacoes.ValidarNome(TextoNome.Text))
                {
                    reserva.Nome = TextoNome.Text;
                }
                if (Validacoes.ValidarCPF(TextoCPF))
                {
                    reserva.Cpf = TextoCPF.Text;
                }
                if (Validacoes.ValidarIdade(TextoIdade.Text))
                {
                    reserva.Idade = int.Parse(TextoIdade.Text);
                }
                if (Validacoes.ValidarTelefone(TextoTelefone))
                {
                    reserva.Telefone = TextoTelefone.Text;
                }
                if(Validacoes.ValidarDatas(DataCheckIn, DataCheckOut))
                {
                    reserva.CheckIn = Convert.ToDateTime(DataCheckIn.Value.Date);
                    reserva.CheckOut = Convert.ToDateTime(DataCheckOut.Value.Date);
                }
                if (Validacoes.ValidarPreco(TextoPreco.Text))
                {
                    reserva.PrecoEstadia = Decimal.Parse(TextoPreco.Text);
                }
                if (Validacoes.ValidarPagamento(BotaoTrue, BotaoFalse))
                {
                    reserva.FoiPago = BotaoTrue.Checked;
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                return false;
            }

            reserva.Sexo = (GeneroEnum)CaixaSexo.SelectedItem;
            return true;
        }

        private void AoClicarEmAdicionar(object sender, EventArgs e)
        {
            if (LerDadosDaReserva())
            {
                JanelaPrincipal.AdicionarReservaNaLista(reserva);
                this.Close();
            }
        }

        private void AoClicarCancelarCadastro(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
