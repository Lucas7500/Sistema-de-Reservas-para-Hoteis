using Dominio;
using Dominio.Constantes;
using Dominio.Enums;
using Dominio.Extensoes;
using FluentValidation;
using System.Text.RegularExpressions;

namespace InteracaoUsuarioForms
{
    public partial class TelaCadastroCliente : Form
    {
        private readonly Reserva _reservaCopia = new();
        private static IValidator<Reserva> _validacaoReserva;

        public TelaCadastroCliente(Reserva reservaParametro, IValidator<Reserva> validacaoReserva)
        {
            InitializeComponent();
            _validacaoReserva = validacaoReserva;
            CaixaSexo.DataSource = Enum.GetValues(typeof(GeneroEnum));

            if (reservaParametro.Id > ValoresPadrao.ID_ZERO)
            {
                DataCheckIn.MinDate = reservaParametro.CheckIn;
                DataCheckOut.MinDate = reservaParametro.CheckOut;
                PreencherTelaCadastro(reservaParametro);
                _reservaCopia = (Reserva)reservaParametro.ShallowCopy();
            }
            else
            {
                DataCheckIn.MinDate = DateTime.Now;
                DataCheckOut.MinDate = DateTime.Now;
            }
        }

        private void PermitirApenasNumerosNaIdade(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar);
        }

        private void PermitirApenasLetrasNoNome(object sender, KeyPressEventArgs e)
        {
            string regexNome = "^[a-zA-ZA-ZáàâãéèêíìîóòõôúùûçÁÀÃÂÉÈÊÍÌÎÓÒÔÕÚÙÛÇ ]*$";

            e.Handled = !char.IsControl(e.KeyChar) && !Regex.IsMatch(e.KeyChar.ToString(), regexNome);
        }

        private void PermitirApenasDecimaisNoPrecoDaEstadia(object sender, KeyPressEventArgs e)
        {
            const char virgula = ',';
            string preco = TextoPreco.Text;
            bool precoPossuiVirgula = preco.Contains(virgula);
            string decimaisAposVirgula = precoPossuiVirgula
                ? preco.Split(virgula)[ValoresPadrao.INDICE_CASAS_DECIMAIS]
                : string.Empty;

            bool possuiDuasCasasDecimais = decimaisAposVirgula.Length == ValoresPadrao.MAX_CASAS_DECIMAIS;
            bool primeiraCondicaoInvalida = !char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != virgula;
            bool segundaCondicaoInvalida = precoPossuiVirgula && e.KeyChar == virgula;
            bool terceiraCondicaoInvalida = precoPossuiVirgula && possuiDuasCasasDecimais && !char.IsControl(e.KeyChar);

            e.Handled = primeiraCondicaoInvalida || segundaCondicaoInvalida || terceiraCondicaoInvalida;
        }

        private static decimal ConverteStringParaDecimalComVirgula(string numero)
        {
            const char virgula = ',';
            const int nenhumNumeroAposVirgula = 0;
            const int umNumeroAposVirgula = 1;
            const char umZeroAposVirgula = '0';
            const string doisZerosAposVirgula = "00";

            if (numero.Contains(virgula))
            {
                string[] preco = numero.Split(virgula);
                string CasasDecimais = preco[ValoresPadrao.INDICE_CASAS_DECIMAIS];

                switch (CasasDecimais.Length)
                {
                    case nenhumNumeroAposVirgula:
                        numero += doisZerosAposVirgula;
                        break;
                    case umNumeroAposVirgula:
                        numero += umZeroAposVirgula;
                        break;
                }
            }
            else
            {
                numero += virgula + doisZerosAposVirgula;
            }

            return Decimal.Parse(numero);
        }

        private void PreencherTelaCadastro(Reserva reserva)
        {
            try
            {
                TextoNome.Text = reserva.Nome;
                TextoCPF.Text = reserva.Cpf;
                TextoTelefone.Text = reserva.Telefone;
                TextoIdade.Text = reserva.Idade.ToString();
                CaixaSexo.SelectedItem = reserva.Sexo;
                DataCheckIn.Value = reserva.CheckIn;
                DataCheckOut.Value = reserva.CheckOut;
                TextoPreco.Text = reserva.PrecoEstadia.ToString();
                BotaoTrue.Checked = reserva.PagamentoEfetuado;
                BotaoFalse.Checked = !reserva.PagamentoEfetuado;
            }
            catch (Exception erro)
            {
                MessageBox.Show(erro.Message, Mensagem.TITULO_ERRO_INESPERADO, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private Dictionary<string, dynamic> ObterReservaPreenchida()
        {
            return new Dictionary<string, dynamic>
            {
                { CamposTabela.COLUNA_NOME, TextoNome.Text },
                { CamposTabela.COLUNA_CPF, TextoCPF.Text },
                { CamposTabela.COLUNA_TELEFONE, TextoTelefone.Text },
                { CamposTabela.COLUNA_IDADE, String.IsNullOrWhiteSpace(TextoIdade.Text) ? ValoresPadrao.CODIGO_DE_ERRO : int.Parse(TextoIdade.Text) },
                { CamposTabela.COLUNA_CHECK_IN, Convert.ToDateTime(DataCheckIn.Value.Date) },
                { CamposTabela.COLUNA_CHECK_OUT, Convert.ToDateTime(DataCheckOut.Value.Date) },
                { CamposTabela.COLUNA_PRECO_ESTADIA, String.IsNullOrWhiteSpace(TextoPreco.Text) ? ValoresPadrao.CODIGO_DE_ERRO : ConverteStringParaDecimalComVirgula(TextoPreco.Text) }
            };
        }

        private void PreencherReserva(Reserva reserva)
        {
            try
            {
                reserva.Nome = TextoNome.Text;
                reserva.Cpf = TextoCPF.Text;
                reserva.Telefone = TextoTelefone.Text;
                reserva.Idade = int.Parse(TextoIdade.Text);
                reserva.Sexo = (GeneroEnum)CaixaSexo.SelectedItem;
                reserva.CheckIn = Convert.ToDateTime(DataCheckIn.Value.Date);
                reserva.CheckOut = Convert.ToDateTime(DataCheckOut.Value.Date);
                reserva.PrecoEstadia = ConverteStringParaDecimalComVirgula(TextoPreco.Text);
                reserva.PagamentoEfetuado = BotaoTrue.Checked;
            }
            catch (Exception erro)
            {
                MessageBox.Show(erro.Message, Mensagem.TITULO_ERRO_INESPERADO, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AoClicarAdicionarCadastro(object sender, EventArgs e)
        {
            try
            {
                ValidacaoCampos.ValidarCampos(ObterReservaPreenchida());
                PreencherReserva(_reservaCopia);
                _validacaoReserva.ValidateAndThrowArgumentException(_reservaCopia);

                if (TelaListaDeReservas.AdicionarReservaNoFormulario(_reservaCopia))
                {
                    this.Close();
                }

            }
            catch (Exception erro)
            {
                string titulo = "Erro no Cadastro";
                MessageBox.Show(erro.Message, titulo, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AoClicarCancelarCadastro(object sender, EventArgs e)
        {
            string mensagem = "Você realmente deseja cancelar?", titulo = "Confirmação de cancelamento";
            var remover = MessageBox.Show(mensagem, titulo, MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (remover.Equals(DialogResult.Yes))
            {
                this.Close();
            }
        }
    }
}