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
            AtualizarGridView();
        }

        public static bool AdicionarReservaNoFormulario(Reserva reserva)
        {
            try
            {
                if (reserva.Id == ValoresPadrao.ID_ZERO)
                {
                    _repositorio.Criar(reserva);
                    MessageBox.Show(Mensagem.SUCESSO_CRIACAO);
                }
                else
                {
                    _repositorio.Atualizar(reserva);
                    MessageBox.Show(Mensagem.SUCESSO_EDICAO);
                }

                AtualizarGridView();
                return true;
            }
            catch (Exception erro)
            {
                MessageBox.Show(erro.Message, Mensagem.TITULO_ERRO_INESPERADO, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        private static void AtualizarGridView()
        {
            try
            {
                TelaDaLista.DataSource = null;
                if (_repositorio.ObterTodos().Any()) TelaDaLista.DataSource = _repositorio.ObterTodos();
            }
            catch (Exception erro)
            {
                MessageBox.Show(erro.Message, Mensagem.TITULO_ERRO_INESPERADO, MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private static int ObterIdReservaSelecionada()
        {
            const string nomeColunaIdGridView = "Id";
            return (int)TelaDaLista.CurrentRow.Cells[nomeColunaIdGridView].Value;
        }

        private static void AbrirTelaCadastro(Reserva reserva)
        {
            TelaCadastroCliente TelaCadastro = new(reserva, _validacaoReserva);
            TelaCadastro.ShowDialog();
        }

        private void AoClicarAdicionarAbrirTelaCadastro(object sender, EventArgs e)
        {
            try
            {
                AbrirTelaCadastro(new Reserva());
            }
            catch (Exception erro)
            {
                MessageBox.Show(erro.Message, Mensagem.TITULO_ERRO_INESPERADO, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AoClicarEditarElementoSelecionado(object sender, EventArgs e)
        {
            try
            {
                const string acaoEditar = "editar";

                if (ListaEhVazia())
                {
                    MessageBox.Show(Mensagem.MensagemErroListaVazia(acaoEditar));
                }
                else if (SomenteUmaLinhaSelecionada())
                {
                    Reserva reservaSelecionada = _repositorio.ObterPorId(ObterIdReservaSelecionada());
                    AbrirTelaCadastro(reservaSelecionada);
                }
                else
                {
                    MessageBox.Show(Mensagem.MensagemErroNenhumaLinhaSelecionada(acaoEditar));
                }
            }
            catch (Exception erro)
            {
                MessageBox.Show(erro.Message, Mensagem.TITULO_ERRO_INESPERADO, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AoClicarDeletarElementoSelecionado(object sender, EventArgs e)
        {
            try
            {
                const string acaoDeletar = "deletar";

                if (ListaEhVazia())
                {
                    MessageBox.Show(Mensagem.MensagemErroListaVazia(acaoDeletar));
                }
                else if (SomenteUmaLinhaSelecionada())
                {
                    Reserva reservaSelecionada = _repositorio.ObterPorId(ObterIdReservaSelecionada());
                    string mensagem = $"Você tem certeza que quer deletar a reserva de {reservaSelecionada.Nome}?", titulo = "Confirmação de remoção";
                    var deletar = MessageBox.Show(mensagem, titulo, MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (deletar.Equals(DialogResult.Yes))
                    {
                        _repositorio.Remover(ObterIdReservaSelecionada());
                        AtualizarGridView();
                    }
                }
                else
                {
                    MessageBox.Show(Mensagem.MensagemErroNenhumaLinhaSelecionada(acaoDeletar));
                }
            }
            catch (Exception erro)
            {
                MessageBox.Show(erro.Message, Mensagem.TITULO_ERRO_INESPERADO, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}