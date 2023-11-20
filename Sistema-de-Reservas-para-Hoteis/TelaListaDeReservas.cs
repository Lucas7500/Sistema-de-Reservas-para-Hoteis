using System.Globalization;

namespace Sistema_de_Reservas_para_Hoteis
{
    public partial class TelaListaDeReservas : Form
    {
        public TelaListaDeReservas()
        {
            InitializeComponent();
        }

        const int primeiroElemento = 0;
        const int umaLinhaSelecionada = 1;
        const int idNulo = 0;
        const int listaNula = 0;

        public static void AdicionarReservaNaLista(Reserva reserva)
        {
            try
            {
                if (reserva.Id == idNulo)
                {
                    reserva.Id = Singleton.IncrementarId();
                    Singleton.RetornaLista().Add(reserva);
                    MessageBox.Show("Reserva foi feita com Sucesso!");
                }
                else
                {
                    MessageBox.Show("A reserva foi editada com sucesso!");
                }
                
                AtualizarLista();
            }
            catch
            {
                MensagemErroInesperado();
            }
        }

        public static void MensagemErroInesperado()
        {
            string mensagem = "Ocorreu um erro inesperado.";
            string titulo = "Aviso";
            MessageBox.Show(mensagem, titulo, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private static void AtualizarLista()
        {
            TelaDaLista.DataSource = null;
            TelaDaLista.DataSource = Singleton.RetornaLista();
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

        private static bool ListaEhVazia()
        {
            if (Singleton.RetornaLista().Count == listaNula)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private static void MensagemErroListaVazia(string acao)
        {
            MessageBox.Show($"Seu programa não possui nenhuma reserva para {acao}.");
        }

        private static void MensagemErroNenhumaLinhaSelecionada(string acao)
        {
            MessageBox.Show($"Selecione uma linha para {acao}!");
        }

        private static Reserva RetornaReservaSelecionada()
        {
            int indexLinha = TelaDaLista.SelectedRows[primeiroElemento].Index;
            int idLinhaSelecionada = (int)TelaDaLista.Rows[indexLinha].Cells[primeiroElemento].Value;
            Reserva reservaSelecionada = Singleton.RetornaLista().Find(x => x.Id == idLinhaSelecionada);
           
            return reservaSelecionada;
        }

        private void AoClicarAbrirTelaDeCadastro(object sender, EventArgs e)
        {
            try
            {
                Reserva reserva = new();
                TelaCadastroCliente TelaCadastro = new(reserva);
                TelaCadastro.ShowDialog();
            }
            catch 
            {
                MensagemErroInesperado();
            }
        }

        private void AoClicarEditarElementoSelecionado(object sender, EventArgs e)
        {
            try
            {
                if (ListaEhVazia())
                {
                    MensagemErroListaVazia("editar");
                }
                else if (SomenteUmaLinhaSelecionada())
                {
                    TelaCadastroCliente TelaCadastro = new(RetornaReservaSelecionada());
                    TelaCadastro.ShowDialog();
                }
                else
                {
                    MensagemErroNenhumaLinhaSelecionada("editar");
                }
            }
            catch
            {
                MensagemErroInesperado();
            }
        }

        private void AoClicarDeletarElementoSelecionado(object sender, EventArgs e)
        {
            try
            {
                if (ListaEhVazia())
                {
                    MensagemErroListaVazia("deletar");
                }
                else if (SomenteUmaLinhaSelecionada())
                {
                    string mensagem = $"Você tem certeza que quer deletar a reserva de {RetornaReservaSelecionada().Nome} ?";
                    var deletar = MessageBox.Show(mensagem, "Confirmação de remoção", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (deletar.Equals(DialogResult.Yes))
                    {
                        Singleton.RetornaLista().Remove(RetornaReservaSelecionada());
                        AtualizarLista();
                    }
                }
                else
                {
                    MensagemErroNenhumaLinhaSelecionada("deletar");
                }
            }
            catch
            {
                MensagemErroInesperado();
            }
        }
    }
}