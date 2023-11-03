using System.Globalization;
using Sistema_de_Reservas_para_Hoteis.Enum;

namespace Sistema_de_Reservas_para_Hoteis
{
    public partial class JanelaPrincipal : Form
    {
        public JanelaPrincipal()
        {
            CultureInfo culture = CultureInfo.InvariantCulture;
            InitializeComponent();
            //PreencherGrid();
        }

        static List<Reserva> reservas = new List<Reserva>();

        public static void Lista(Reserva reserva)
        {
            JanelaPrincipal.reservas.Add(reserva);
            TelaDaLista.DataSource = reservas; // Relacionado a Pilha
        }

        //private void PreencherGrid()
        //{
        //    TelaDaLista.DataSource = Lista();
        //}

        private void BotaoAdicionar_Click(object sender, EventArgs e)
        {
            CadastroCliente frm2 = new CadastroCliente();
            frm2.Show();
        }
    }
}