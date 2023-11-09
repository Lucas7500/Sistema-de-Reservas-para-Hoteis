using System.Globalization;
using System.Windows.Forms;
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

        private void AoClicarEditarElementoSelecionado(object sender, EventArgs e)
        {
            if (TelaDaLista.SelectedRows.Count == 1)
            {
                int indexId = 0;
                int indexLinha = TelaDaLista.SelectedRows[0].Index;
                int idElemento = (int) TelaDaLista.Rows[indexLinha].Cells[indexId].Value;
               


                CadastroCliente TelaCadastro = new();
                TelaCadastro.ShowDialog();

                TelaDaLista.DataSource = null;
                // Fazer a edição
                TelaDaLista.DataSource = reservas;
            }
            else if (TelaDaLista.SelectedRows.Count > 1)
            {
                MessageBox.Show("Você deve selecionar apenas uma linha para editar.");
            }
            else
            {
                MessageBox.Show("Você deve selecionar ao menos uma linha para editar.");

            }
        }
    }
}