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

        private void LerDadosDaReserva()
        {
            reserva.Nome = TextoNome.Text;
            reserva.Cpf = TextoCPF.Text;
            reserva.Idade = Convert.ToInt32(TextoIdade.Text);
            reserva.Telefone = TextoTelefone.Text;
            reserva.Sexo = (GeneroEnum) CaixaSexo.SelectedItem;
            reserva.CheckIn = Convert.ToDateTime(DataCheckIn.Value.Date);
            reserva.CheckOut = Convert.ToDateTime(DataCheckOut.Value.Date);
            reserva.PrecoEstadia = Decimal.Parse(TextoPreco.Text);
            reserva.FoiPago = BotaoTrue.Checked;
            JanelaPrincipal.AdicionarReservaNaLista(reserva);
        }

        private void AoClicarAdicionarCadastroNaTelaPrincipal(object sender, EventArgs e)
        {
            LerDadosDaReserva();
            this.Close();
        }

        private void BotaoCancelarCadastro_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
