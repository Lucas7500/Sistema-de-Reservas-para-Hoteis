using Dominio;
using Dominio.Enums;

namespace Interacao
{
    public partial class TelaCadastroCliente : Form
    {
        private readonly Reserva reservaCopia = new();
        const int idNulo = 0;

        public TelaCadastroCliente(Reserva reservaParametro)
        {
            InitializeComponent();
            CaixaSexo.DataSource = Enum.GetValues(typeof(GeneroEnum));
            if (reservaParametro.Id > idNulo)
            {
                DataCheckIn.MinDate = reservaParametro.CheckIn;
                DataCheckOut.MinDate = reservaParametro.CheckOut;
                PreencherTelaDeCadastro(reservaParametro);
                reservaCopia = (Reserva)reservaParametro.ShallowCopy();
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
            if (!char.IsControl(e.KeyChar) && !char.IsAsciiLetter(e.KeyChar))
            {
                if (e.KeyChar == ' ')
                {
                    return;
                }
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
                int IndexCasasDecimais = 1, MaxCasasDecimais = 2;
                string[] preco = TextoPreco.Text.Split(',');
                string CasasDecimais = preco[IndexCasasDecimais];
                bool Possui2CasasDecimais = CasasDecimais.Length == MaxCasasDecimais;
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
                { "Idade", String.IsNullOrWhiteSpace(TextoIdade.Text) ? (int)ValoresValidacaoEnum.codigoDeErro : int.Parse(TextoIdade.Text) },
                { "CheckIn", Convert.ToDateTime(DataCheckIn.Value.Date) },
                { "CheckOut", Convert.ToDateTime(DataCheckOut.Value.Date) },
                { "PrecoEstadia", String.IsNullOrWhiteSpace(TextoPreco.Text) ? (int)ValoresValidacaoEnum.codigoDeErro : ConverterEmDecimalComVirgula(TextoPreco.Text) }
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
                AtribuirValoresReserva(reservaCopia);
                TelaListaDeReservas.AdicionarReservaNaLista(reservaCopia);
                this.Close();
            }
            catch (Exception erro)
            {
                MessageBox.Show(erro.Message, "Erro no Cadastro", MessageBoxButtons.OK, MessageBoxIcon.Error);
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