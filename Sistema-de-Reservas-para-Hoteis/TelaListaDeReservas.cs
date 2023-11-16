using System.Globalization;

namespace Sistema_de_Reservas_para_Hoteis
{
    public partial class TelaListaDeReservas : Form
    {
        public TelaListaDeReservas()
        {
            _ = CultureInfo.InvariantCulture;
            InitializeComponent();
        }

        private static readonly List<Reserva> listaReservas = new();
        static int id = 0;
        const int primeiroElemento = 0;
        const int umaLinhaSelecionada = 1;
        const int nenhumaLinhaSelecionada = 0;
        const int idNulo = 0;

        public static void AdicionarReservaNaLista(Reserva reserva)
        {
            if (reserva.Id == idNulo)
            {
                id++;
                reserva.Id = id;
                listaReservas.Add(reserva);
                MessageBox.Show("Reserva foi feita com Sucesso!");
            }
            else
            {
                MessageBox.Show("A reserva foi editada com sucesso!");
            }

            TelaDaLista.DataSource = null;
            TelaDaLista.DataSource = listaReservas;
        }

        private void AoClicarAdicionarAbrirTelaDeCadastro(object sender, EventArgs e)
        {
            Reserva reserva = new();
            TelaCadastroCliente TelaCadastro = new(reserva);
            TelaCadastro.ShowDialog();
        }

        private void AoClicarEditarElementoSelecionado(object sender, EventArgs e)
        {
            int qtdLinhasSelecionadas = TelaDaLista.SelectedRows.Count;

            if (qtdLinhasSelecionadas == umaLinhaSelecionada)
            {
                if (listaReservas.Count == nenhumaLinhaSelecionada)
                {
                    MessageBox.Show("Seu programa não possui nenhuma reserva para ser editada.");
                    return;
                }

                int indexLinha = TelaDaLista.SelectedRows[primeiroElemento].Index;
                int idLinhaSelecionada = (int)TelaDaLista.Rows[indexLinha].Cells[primeiroElemento].Value;

                Reserva reservaSelecionada = listaReservas.Find(x => x.Id == idLinhaSelecionada);

                TelaCadastroCliente TelaCadastro = new(reservaSelecionada);
                TelaCadastro.ShowDialog();
            }
            else if (qtdLinhasSelecionadas > umaLinhaSelecionada)
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