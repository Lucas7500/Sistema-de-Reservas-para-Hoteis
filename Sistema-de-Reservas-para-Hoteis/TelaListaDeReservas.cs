using Dominio;
using Dominio.Constantes;
using FluentValidation;
using Infraestrutura;

namespace Interacao
{
    public partial class TelaListaDeReservas : Form
    {
        private static IRepositorio _repositorio;
        private static IValidator<Reserva> _validacaoReserva;

        public TelaListaDeReservas(IRepositorio repositorioUtilizado, IValidator<Reserva> validacaoReserva)
        {
            _repositorio = repositorioUtilizado;
            _validacaoReserva = validacaoReserva;
            InitializeComponent();
            AtualizarGrid();
        }

        const int primeiroElemento = 0;
        const int umaLinhaSelecionada = 1;
        const int idNulo = 0;
        const int listaNula = 0;

        public static bool AdicionarReservaNoFormulario(Reserva reserva)
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
                return true;
            }
            catch (Exception erro)
            {
                MessageBox.Show(erro.Message, MensagemExcessao.TITULO_ERRO_INESPERADO, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
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

        private static void AbrirNovaTelaDeCadastro(Reserva reserva)
        {
            TelaCadastroCliente TelaCadastro = new(reserva, _validacaoReserva);
            TelaCadastro.ShowDialog();
        }

        private void AoClicarAdicionarAbrirTelaDeCadastro(object sender, EventArgs e)
        {
            try
            {
                AbrirNovaTelaDeCadastro(new Reserva());
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
                    AbrirNovaTelaDeCadastro(reservaSelecionada);
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