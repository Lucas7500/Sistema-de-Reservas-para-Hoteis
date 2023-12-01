using Dominio;
using Infraestrutura;

namespace Interacao
{
    public partial class TelaListaDeReservas : Form
    {
        private static IRepositorio _repositorio;

        public TelaListaDeReservas(IRepositorio repositorioUtilizado)
        {
            _repositorio = repositorioUtilizado;
            InitializeComponent();
            AtualizarGrid();
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
                    _repositorio.Criar(reserva);
                    MessageBox.Show("Reserva foi criada com Sucesso!");
                }
                else
                {
                    _repositorio.Atualizar(reserva);
                    MessageBox.Show("A reserva foi editada com sucesso!");
                }

                AtualizarGrid();
            }
            catch (Exception erro)
            {
                MessageBox.Show(erro.Message, MensagemExcessao.TITULO_ERRO_INESPERADO, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private static void AtualizarGrid()
        {
            TelaDaLista.DataSource = null;
            if (_repositorio.ObterTodos().Any())
            {
                TelaDaLista.DataSource = _repositorio.ObterTodos();
            }
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
            if (_repositorio.ObterTodos().Count == listaNula)
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
                MessageBox.Show(erro.Message, MensagemExcessao.TITULO_ERRO_INESPERADO, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AoClicarEditarElementoSelecionado(object sender, EventArgs e)
        {
            try
            {
                if (ListaEhVazia())
                {
                    MessageBox.Show(MensagemExcessao.MensagemErroListaVazia("editar"));
                }
                else if (SomenteUmaLinhaSelecionada())
                {
                    Reserva reservaSelecionada = _repositorio.ObterPorId(RetornaIdReservaSelecionada());
                    TelaCadastroCliente TelaCadastro = new(reservaSelecionada);
                    TelaCadastro.ShowDialog();
                }
                else
                {
                    MessageBox.Show(MensagemExcessao.MensagemErroNenhumaLinhaSelecionada("editar"));
                }
            }
            catch (Exception erro)
            {
                MessageBox.Show(erro.Message, MensagemExcessao.TITULO_ERRO_INESPERADO, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AoClicarDeletarElementoSelecionado(object sender, EventArgs e)
        {
            try
            {
                if (ListaEhVazia())
                {
                    MessageBox.Show(MensagemExcessao.MensagemErroListaVazia("deletar"));
                }
                else if (SomenteUmaLinhaSelecionada())
                {
                    Reserva reservaSelecionada = _repositorio.ObterPorId(RetornaIdReservaSelecionada());
                    string mensagem = $"Você tem certeza que quer deletar a reserva de {reservaSelecionada.Nome}?", titulo = "Confirmação de remoção";
                    var deletar = MessageBox.Show(mensagem, titulo, MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (deletar.Equals(DialogResult.Yes))
                    {
                        _repositorio.Remover(RetornaIdReservaSelecionada());
                        AtualizarGrid();
                    }
                }
                else
                {
                    MessageBox.Show(MensagemExcessao.MensagemErroNenhumaLinhaSelecionada("deletar"));
                }
            }
            catch (Exception erro)
            {
                MessageBox.Show(erro.Message, MensagemExcessao.TITULO_ERRO_INESPERADO, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}