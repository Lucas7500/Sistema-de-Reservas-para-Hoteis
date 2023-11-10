using System.Collections.Generic;
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

        public static readonly List<Reserva> reservas = new();
        static int id = 0;
        public static int tipoDeModificacao;
        public static int idReservaSelecionada;
        public static Reserva reservaSelecionada = new();
        public enum CRUD
        {
            Adicionar,
            Editar
        }

        public static void AdicionarReservaNaLista(Reserva reserva)
        {
            if (tipoDeModificacao == (int)CRUD.Adicionar)
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
            tipoDeModificacao = (int) CRUD.Adicionar;
            CadastroCliente TelaCadastro = new();
            TelaCadastro.ShowDialog();
        }

        private void AoClicarEditarElementoSelecionado(object sender, EventArgs e)
        {
            tipoDeModificacao = (int)CRUD.Editar;
            int umaLinhaSelecionada = 1;
            int qtdLinhasSelecionadas = TelaDaLista.SelectedRows.Count;
            int primeiroElemento = 0;

            if (qtdLinhasSelecionadas == umaLinhaSelecionada)
            {
                int indexLinha = TelaDaLista.SelectedRows[primeiroElemento].Index;
                int? idLinhaSelecionada = (int)TelaDaLista.Rows[indexLinha].Cells[primeiroElemento].Value;

                if (idLinhaSelecionada == null)
                {
                    MessageBox.Show("Seu programa não possui nenhuma reserva para ser editada.");
                    return;
                }

                idReservaSelecionada = (int)idLinhaSelecionada;

                CadastroCliente TelaCadastro = new();

                foreach (Reserva reservaEdicao in reservas)
                {
                    if (reservaEdicao.Id == idReservaSelecionada)
                    {
                        reservaSelecionada = reservaEdicao;
                        TelaCadastro.PreencherDadosDaReserva(reservaSelecionada);
                        break;
                    }
                }

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