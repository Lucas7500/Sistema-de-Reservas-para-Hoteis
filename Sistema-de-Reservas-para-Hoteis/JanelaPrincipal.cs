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

        static List<Reserva> reservas = new();

        static int id = 0;

        public static void AdicionarReservaNaLista(Reserva reserva)
        {
            TelaDaLista.DataSource = null;
            id++;
            reserva.Id = id;
            JanelaPrincipal.reservas.Add(reserva);
            TelaDaLista.DataSource = reservas;
        }

        private void AoClicarAbrirTelaDeCadastro(object sender, EventArgs e)
        {
            CadastroCliente TelaCadastro = new CadastroCliente();
            TelaCadastro.ShowDialog();
        }
    }
}