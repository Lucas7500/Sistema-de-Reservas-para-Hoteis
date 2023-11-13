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

        private static readonly List<Reserva> reservas = new();
        static int id = 0;

        public static void AdicionarReservaNaLista(Reserva reserva, bool edicao)
        {
            if (!edicao)
            {
                id++;
                reserva.Id = id;
                reservas.Add(reserva);
            }
            TelaDaLista.DataSource = null;
            TelaDaLista.DataSource = reservas;
        }

        private void AoClicarAdicionarAbrirTelaDeCadastro(object sender, EventArgs e)
        {
            TelaCadastroCliente TelaCadastro = new();
            TelaCadastro.ShowDialog();
        }

        private void AoClicarEditarElementoSelecionado(object sender, EventArgs e)
        {
            int umaLinhaSelecionada = 1;
            int qtdLinhasSelecionadas = TelaDaLista.SelectedRows.Count;
            int primeiroElemento = 0;

            if (qtdLinhasSelecionadas == umaLinhaSelecionada)
            {
                if (reservas.Count == 0)
                {
                    MessageBox.Show("Seu programa não possui nenhuma reserva para ser editada.");
                    return;
                }

                int indexLinha = TelaDaLista.SelectedRows[primeiroElemento].Index;
                int idLinhaSelecionada = (int)TelaDaLista.Rows[indexLinha].Cells[primeiroElemento].Value;

                Reserva reservaSelecionada = new();

                foreach (Reserva reservaEdicao in reservas)
                {
                    if (reservaEdicao.Id == idLinhaSelecionada)
                    {
                        reservaSelecionada = reservaEdicao;
                        break;
                    }
                }

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