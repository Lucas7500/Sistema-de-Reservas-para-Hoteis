namespace Sistema_de_Reservas_para_Hoteis
{
    public partial class TelaListaDeReservas : Form
    {
        public TelaListaDeReservas()
        {
            InitializeComponent();
            AtualizarGrid();
        }

        private static readonly IRepositorio repositorio = new RepositorioBancoDeDados();
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
                    repositorio.Criar(reserva);
                    MessageBox.Show("Reserva foi criada com Sucesso!");
                }
                else
                {
                    repositorio.Atualizar(reserva);
                    MessageBox.Show("A reserva foi editada com sucesso!");
                }

                AtualizarGrid();
            }
            catch (Exception erro)
            {
                MensagemExcessao.MensagemErroInesperado(erro.Message);
            }
        }

        private static void AtualizarGrid()
        {
            TelaDaLista.DataSource = null;
            TelaDaLista.DataSource = repositorio.ObterTodos();
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
            if (repositorio.ObterTodos().Count == listaNula)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private static int RetornaIdReservaSelecionada()
        {
            int indexLinha = TelaDaLista.SelectedRows[primeiroElemento].Index;
            int idLinhaSelecionada = (int)TelaDaLista.Rows[indexLinha].Cells[primeiroElemento].Value;

            return idLinhaSelecionada;
        }

        private void AoClicarAbrirTelaDeCadastro(object sender, EventArgs e)
        {
            try
            {
                Reserva reserva = new();
                TelaCadastroCliente TelaCadastro = new(reserva);
                TelaCadastro.ShowDialog();
            }
            catch (Exception erro)
            {
                MensagemExcessao.MensagemErroInesperado(erro.Message);
            }
        }

        private void AoClicarEditarElementoSelecionado(object sender, EventArgs e)
        {
            try
            {
                if (ListaEhVazia())
                {
                    MensagemExcessao.MensagemErroListaVazia("editar");
                }
                else if (SomenteUmaLinhaSelecionada())
                {
                    Reserva reservaSelecionada = repositorio.ObterPorId(RetornaIdReservaSelecionada());
                    TelaCadastroCliente TelaCadastro = new(reservaSelecionada);
                    TelaCadastro.ShowDialog();
                }
                else
                {
                    MensagemExcessao.MensagemErroNenhumaLinhaSelecionada("editar");
                }
            }
            catch (Exception erro)
            {
                MensagemExcessao.MensagemErroInesperado(erro.Message);
            }
        }

        private void AoClicarDeletarElementoSelecionado(object sender, EventArgs e)
        {
            try
            {
                if (ListaEhVazia())
                {
                    MensagemExcessao.MensagemErroListaVazia("deletar");
                }
                else if (SomenteUmaLinhaSelecionada())
                {
                    Reserva reservaSelecionada = repositorio.ObterPorId(RetornaIdReservaSelecionada());
                    string mensagem = $"Voc� tem certeza que quer deletar a reserva de {reservaSelecionada.Nome}?", titulo = "Confirma��o de remo��o";
                    var deletar = MessageBox.Show(mensagem, titulo, MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (deletar.Equals(DialogResult.Yes))
                    {
                        repositorio.Remover(RetornaIdReservaSelecionada());
                        AtualizarGrid();
                    }
                }
                else
                {
                    MensagemExcessao.MensagemErroNenhumaLinhaSelecionada("deletar");
                }
            }
            catch (Exception erro)
            {
                MensagemExcessao.MensagemErroInesperado(erro.Message);
            }
        }
    }
}