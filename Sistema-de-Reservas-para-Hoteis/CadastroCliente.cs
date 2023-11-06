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

        private void BotaoAdicionarCadastro_Click(object sender, EventArgs e)
        {


            reserva.Id++;
            reserva.Nome = TextoNome.Text;
            reserva.Cpf = TextoCpf.Text;
            reserva.Idade = Convert.ToInt32(TextoIdade.Text);
            reserva.Telefone = TextoTelefone.Text;
            reserva.Sexo = (GeneroEnum) CaixaSexo.SelectedItem;
            reserva.CheckIn = Convert.ToDateTime(DataCheckIn.Value.Date);
            reserva.CheckOut = Convert.ToDateTime(DataCheckOut.Value.Date);
            reserva.PrecoDaEstadia = Decimal.Parse(TextoPreco.Text);
            reserva.PagamentoEfetuado = BotaoTrue.Checked ? true : false;
            JanelaPrincipal.Lista(reserva);
            this.Close();
        }

        private void BotaoCancelarCadastro_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
