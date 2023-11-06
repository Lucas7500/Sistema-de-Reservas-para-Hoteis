using System.Globalization;
using Sistema_de_Reservas_para_Hoteis.Enums;

namespace Sistema_de_Reservas_para_Hoteis
{
    public partial class JanelaPrincipal : Form
    {
        public JanelaPrincipal()
        {
            CultureInfo culture = CultureInfo.InvariantCulture;
            InitializeComponent();
        }

        static List<Reserva> reservas = new List<Reserva>();

        static int id = 0;

        public static void Lista(Reserva reserva)
        {
            TelaDaLista.DataSource = null;
            id++;
            reserva.Id = id;
            JanelaPrincipal.reservas.Add(reserva);
            TelaDaLista.DataSource = reservas;
        }

        private void BotaoAdicionar_Click(object sender, EventArgs e)
        {
            CadastroCliente frm2 = new CadastroCliente();
            frm2.Show();
        }
    }
}