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
            CaixaSexo.DataSource = Enum.GetValues(typeof(GeneroEnum));
            _validacaoReserva = validacaoReserva;
            if (reservaParametro.Id > ValoresPadrao.ID_NULO)
            {
                DataCheckIn.MinDate = reservaParametro.CheckIn;
                DataCheckOut.MinDate = reservaParametro.CheckOut;
                PreencherTelaDeCadastro(reservaParametro);
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
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void PermitirApenasLetrasNoNome(object sender, KeyPressEventArgs e)
        {
            string regexNome = "^[a-zA-ZA-ZáàâãéèêíìîóòõôúùûçÁÀÃÂÉÈÊÍÌÎÓÒÔÕÚÙÛÇ ]*$";

            if (!char.IsControl(e.KeyChar) && !Regex.IsMatch(e.KeyChar.ToString(), regexNome))
            {
                e.Handled = true;
            }
        }

        private void PermitirApenasDecimaisNoPrecoDaEstadia(object sender, KeyPressEventArgs e)
        {
            bool PossuiVirgula = TextoPreco.Text.Contains(',');

            if (e.KeyChar == ',')
            {
                e.Handled = PossuiVirgula;
                return;
            }
            if (PossuiVirgula)
            {
                string[] preco = TextoPreco.Text.Split(',');
                string CasasDecimais = preco[ValoresPadrao.INDEX_CASAS_DECIMAIS];
                bool Possui2CasasDecimais = CasasDecimais.Length == ValoresPadrao.MAX_CASAS_DECIMAIS;
                e.Handled = Possui2CasasDecimais && !char.IsControl(e.KeyChar);
            }
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private static decimal ConverterEmDecimalComVirgula(string numero)
        {
            if (numero.Contains(','))
            {
                string[] preco = numero.Split(',');
                string CasasDecimais = preco[1];

                switch (CasasDecimais.Length)
                {
                    case 0:
                        numero += "00";
                        return Decimal.Parse(numero);
                    case 1:
                        numero += '0';
                        return Decimal.Parse(numero);
                    case 2:
                        return Decimal.Parse(numero);
                }
            }
            numero += ",00";

            return Decimal.Parse(numero);
        }

        private void PreencherTelaDeCadastro(Reserva reserva)
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
                MessageBox.Show(erro.Message, MensagemExcessao.TITULO_ERRO_INESPERADO, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private Dictionary<string, dynamic> LerEntradasDoUsuario()
        {
            return new Dictionary<string, dynamic>
            {
                { "Nome", TextoNome.Text },
                { "Cpf", TextoCPF.Text },
                { "Telefone", TextoTelefone.Text },
                { "Idade", String.IsNullOrWhiteSpace(TextoIdade.Text) ? ValoresPadrao.CODIGO_DE_ERRO : int.Parse(TextoIdade.Text) },
                { "CheckIn", Convert.ToDateTime(DataCheckIn.Value.Date) },
                { "CheckOut", Convert.ToDateTime(DataCheckOut.Value.Date) },
                { "PrecoEstadia", String.IsNullOrWhiteSpace(TextoPreco.Text) ? ValoresPadrao.CODIGO_DE_ERRO : ConverterEmDecimalComVirgula(TextoPreco.Text) }
            };
        }

        private void AtribuirValoresReserva(Reserva reserva)
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
                reserva.PrecoEstadia = ConverterEmDecimalComVirgula(TextoPreco.Text);
                reserva.PagamentoEfetuado = BotaoTrue.Checked;
            }
            catch (Exception erro)
            {
                MessageBox.Show(erro.Message, MensagemExcessao.TITULO_ERRO_INESPERADO, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AoClicarAdicionarCadastro(object sender, EventArgs e)
        {
            try
            {
                ValidacaoCampos.ValidarCampos(LerEntradasDoUsuario());
                AtribuirValoresReserva(_reservaCopia);
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