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
        const int idNulo = 0;
        const int listaNula = 0;

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

            AtualizarLista();
        }

        private static void AtualizarLista()
        {
            TelaDaLista.DataSource = null;
            TelaDaLista.DataSource = listaReservas;
        }

        private static bool SomenteUmaLinhaSelecionada()
        {
            int qtdLinhasSelecionadas = TelaDaLista.SelectedRows.Count;

            if (qtdLinhasSelecionadas == umaLinhaSelecionada)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private static bool ListaEhNula()
        {
            if (listaReservas.Count == listaNula)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private static Reserva RetornaReservaSelecionada()
        {
            int indexLinha = TelaDaLista.SelectedRows[primeiroElemento].Index;
            int idLinhaSelecionada = (int)TelaDaLista.Rows[indexLinha].Cells[primeiroElemento].Value;
            Reserva reservaSelecionada = listaReservas.Find(x => x.Id == idLinhaSelecionada);

            return reservaSelecionada;
        }

        private void AoClicarAbrirTelaDeCadastro(object sender, EventArgs e)
        {
            Reserva reserva = new();
            TelaCadastroCliente TelaCadastro = new(reserva);
            TelaCadastro.ShowDialog();
        }

        private void AoClicarEditarElementoSelecionado(object sender, EventArgs e)
        {
            if (ListaEhNula())
            {
                MessageBox.Show("Seu programa não possui nenhuma reserva para ser editada.");
            }
            else if (SomenteUmaLinhaSelecionada())
            {
                TelaCadastroCliente TelaCadastro = new(RetornaReservaSelecionada());
                TelaCadastro.ShowDialog();
            }
            else
            {
                MessageBox.Show("Selecione uma linha para editar!");
            }
        }

        private void AoClicarDeletarElementoSelecionado(object sender, EventArgs e)
        {
            if (ListaEhNula())
            {
                MessageBox.Show("Seu programa não possui nenhuma reserva para ser deletada.");
            }
            else if (SomenteUmaLinhaSelecionada())
            {
                string mensagem = $"Você tem certeza que quer deletar a reserva do cliente {RetornaReservaSelecionada().Nome} ?";
                var deletar = MessageBox.Show(mensagem, "Confirmação de remoção", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (deletar.Equals(DialogResult.Yes))
                {
                    listaReservas.Remove(RetornaReservaSelecionada());
                    AtualizarLista();
                }
            }
            else
            {
                MessageBox.Show("Selecione uma linha para deletar!");
            }
        }
    }
}