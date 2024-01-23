using Dominio;
using Dominio.Constantes;
using FluentValidation;
using Infraestrutura;

namespace InteracaoUsuarioForms
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

        public static bool AdicionarReservaNoFormulario(Reserva reserva)
        {
            try
            {
                const string mensagemSucessoCriacao = "Reserva foi criada com Sucesso!";
                const string mensagemSucessoEdicao = "Reserva foi editada com Sucesso!";
                if (reserva.Id == ValoresPadrao.ID_ZERO)
                {
                    _repositorio.Criar(reserva);
                    MessageBox.Show(mensagemSucessoCriacao);
                }
                else
                {
                    _repositorio.Atualizar(reserva);
                    MessageBox.Show(mensagemSucessoEdicao);
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
            try
            {
                TelaDaLista.DataSource = null;
                if (_repositorio.ObterTodos().Any()) TelaDaLista.DataSource = _repositorio.ObterTodos();
            }
            catch (Exception erro)
            {
                MessageBox.Show(erro.Message, MensagemExcessao.TITULO_ERRO_INESPERADO, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }


        private static bool SomenteUmaLinhaSelecionada()
        {
            return TelaDaLista.SelectedRows.Count == ValoresPadrao.UMA_LINHA_SELECIONADA;
        }

        private static bool ListaEhVazia()
        {
            return _repositorio.ObterTodos().Count == ValoresPadrao.LISTA_NULA;
        }

        private static int RetornaIdReservaSelecionada()
        {
            const string nomeColunaId = "Id";
            return (int)TelaDaLista.CurrentRow.Cells[nomeColunaId].Value;
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
                const string acaoEditar = "editar";

                if (ListaEhVazia())
                {
                    MessageBox.Show(MensagemExcessao.MensagemErroListaVazia(acaoEditar));
                }
                else if (SomenteUmaLinhaSelecionada())
                {
                    Reserva reservaSelecionada = _repositorio.ObterPorId(RetornaIdReservaSelecionada());
                    AbrirNovaTelaDeCadastro(reservaSelecionada);
                }
                else
                {
                    MessageBox.Show(MensagemExcessao.MensagemErroNenhumaLinhaSelecionada(acaoEditar));
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
                const string acaoDeletar = "deletar";

                if (ListaEhVazia())
                {
                    MessageBox.Show(MensagemExcessao.MensagemErroListaVazia(acaoDeletar));
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
                    MessageBox.Show(MensagemExcessao.MensagemErroNenhumaLinhaSelecionada(acaoDeletar));
                }
            }
            catch (Exception erro)
            {
                MessageBox.Show(erro.Message, MensagemExcessao.TITULO_ERRO_INESPERADO, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}