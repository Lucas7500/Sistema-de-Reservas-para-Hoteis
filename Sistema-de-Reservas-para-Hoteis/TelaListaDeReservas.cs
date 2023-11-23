namespace Sistema_de_Reservas_para_Hoteis
{
    public partial class TelaListaDeReservas : Form
    {
        public TelaListaDeReservas()
        {
            InitializeComponent();
            AtualizarGrid();
        }

        private static readonly IRepositorio repositorio = new Repositorio();
        private static readonly IRepositorio repositorioBD = new RepositorioBancoDeDados();
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
                    repositorioBD.Criar(reserva);
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
                MensagemErroInesperado(erro.Message);
            }  
        }
        public static void MensagemErroInesperado(string mensagem)
        {
            string titulo = "Ocorreu um erro inesperado";
            MessageBox.Show(mensagem, titulo, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private static void AtualizarGrid()
        {
            TelaDaLista.DataSource = null;
            TelaDaLista.DataSource = repositorioBD.ObterTodos();
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
            if (repositorioBD.ObterTodos().Count == listaNula)
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
                MensagemErroInesperado(erro.Message);
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
                    Reserva reservaSelecionada = repositorio.ObterPorId(RetornaIdReservaSelecionada());
                    TelaCadastroCliente TelaCadastro = new(reservaSelecionada);
                    TelaCadastro.ShowDialog();
                }
                else
                {
                    MensagemErroNenhumaLinhaSelecionada("editar");
                }
            }
            catch (Exception erro)
            {
                MensagemErroInesperado(erro.Message);
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
                    Reserva reservaSelecionada = repositorioBD.ObterPorId(RetornaIdReservaSelecionada());
                    string mensagem = $"Você tem certeza que quer deletar a reserva de {reservaSelecionada.Nome}?", titulo = "Confirmação de remoção";
                    var deletar = MessageBox.Show(mensagem, titulo, MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (deletar.Equals(DialogResult.Yes))
                    {
                        repositorioBD.Remover(RetornaIdReservaSelecionada());
                        AtualizarGrid();
                    }
                }
                else
                {
                    MensagemErroNenhumaLinhaSelecionada("deletar");
                }
            }
            catch (Exception erro)
            {
                MensagemErroInesperado(erro.Message);      
            }
        }
    }
}