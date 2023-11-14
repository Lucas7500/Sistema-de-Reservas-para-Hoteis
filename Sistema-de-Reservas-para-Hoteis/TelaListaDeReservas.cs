using System.Collections.Generic;
using System.Globalization;
using System.Windows.Forms;
using Sistema_de_Reservas_para_Hoteis.Enums;

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

        public static void AdicionarReservaNaLista(Reserva reserva)
        {
            if (reserva.Id == 0)
            {
                id++;
                reserva.Id = id;
                listaReservas.Add(reserva);
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
            int umaLinhaSelecionada = 1;
            int qtdLinhasSelecionadas = TelaDaLista.SelectedRows.Count;
            int primeiroElemento = 0;

            if (qtdLinhasSelecionadas == umaLinhaSelecionada)
            {
                if (listaReservas.Count == 0)
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