using Sistema_de_Reservas_para_Hoteis.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sistema_de_Reservas_para_Hoteis
{
    public partial class TelaCadastroCliente : Form
    {
        private readonly Reserva reserva;
        private readonly bool edicao;
        public TelaCadastroCliente()
        {
            InitializeComponent();
            reserva = new();
            edicao = false;
            CaixaSexo.DataSource = Enum.GetValues(typeof(GeneroEnum));
            DataCheckIn.MinDate = DateTime.Now;
            DataCheckOut.MinDate = DateTime.Now;
        }

        public TelaCadastroCliente(Reserva reservaSelecionada)
        {
            InitializeComponent();
            edicao = true;
            reserva = reservaSelecionada;
            CaixaSexo.DataSource = Enum.GetValues(typeof(GeneroEnum));
            DataCheckIn.MinDate = reservaSelecionada.CheckIn;
            DataCheckOut.MinDate = reservaSelecionada.CheckIn;
            PreencherCadastroComDadosDaReserva(reservaSelecionada);
        }

        public void LerDadosDaReserva(Reserva reservaTemporaria)
        {
            reservaTemporaria.Nome = TextoNome.Text;
            reservaTemporaria.Cpf = TextoCPF.Text;
            reservaTemporaria.Telefone = TextoTelefone.Text;
            reservaTemporaria.Idade = String.IsNullOrWhiteSpace(TextoIdade.Text) ? Validacoes.codigoDeErro : int.Parse(TextoIdade.Text);
            reservaTemporaria.Sexo = (GeneroEnum)CaixaSexo.SelectedItem;
            reservaTemporaria.CheckIn = Convert.ToDateTime(DataCheckIn.Value.Date);
            reservaTemporaria.CheckOut = Convert.ToDateTime(DataCheckOut.Value.Date);
            reservaTemporaria.PrecoEstadia = String.IsNullOrWhiteSpace(TextoPreco.Text) ? Validacoes.codigoDeErro : ConverterEmDecimalComVirgula(TextoPreco.Text);
            reservaTemporaria.PagamentoEfetuado = !BotaoTrue.Checked && !BotaoFalse.Checked ? null : BotaoTrue.Checked;
        }

        private void PreencherCadastroComDadosDaReserva(Reserva reservaSelecionada)
        {
            TextoNome.Text = reservaSelecionada.Nome;
            TextoCPF.Text = reservaSelecionada.Cpf;
            TextoTelefone.Text = reservaSelecionada.Telefone;
            TextoIdade.Text = reservaSelecionada.Idade.ToString();
            CaixaSexo.SelectedItem = reservaSelecionada.Sexo;
            DataCheckIn.Value = reservaSelecionada.CheckIn;
            DataCheckOut.Value = reservaSelecionada.CheckOut;
            TextoPreco.Text = reservaSelecionada.PrecoEstadia.ToString();
            if (reservaSelecionada.PagamentoEfetuado != null)
            {
                BotaoTrue.Checked = (bool)reservaSelecionada.PagamentoEfetuado;
                BotaoFalse.Checked = (bool)!reservaSelecionada.PagamentoEfetuado;
            }
        }

        private static void CopiarDadosDeReservas(Reserva reserva1, Reserva reserva2)
        {
            reserva1.Nome = reserva2.Nome;
            reserva1.Cpf = reserva2.Cpf;
            reserva1.Telefone = reserva2.Telefone;
            reserva1.Idade = reserva2.Idade;
            reserva1.Sexo = reserva2.Sexo;
            reserva1.CheckIn = reserva2.CheckIn;
            reserva1.CheckOut = reserva2.CheckOut;
            reserva1.PrecoEstadia = reserva2.PrecoEstadia;
            reserva1.PagamentoEfetuado = reserva2.PagamentoEfetuado;
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

        private void AoClicarAdicionarCadastro(object sender, EventArgs e)
        {
            try
            {
                Reserva reservaTemporaria = new();

                if (edicao)
                {
                    CopiarDadosDeReservas(reservaTemporaria, reserva);
                }

                LerDadosDaReserva(reservaTemporaria);
                Validacoes.ValidarCampos(reservaTemporaria, edicao);
                CopiarDadosDeReservas(reserva, reservaTemporaria);
                TelaListaDeReservas.AdicionarReservaNaLista(reserva, edicao);
                this.Close();
            }
            catch (Exception erro)
            {
                MessageBox.Show(erro.Message, "Erro no Cadastro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AoClicarCancelarCadastro(object sender, EventArgs e)
        {
            this.Close();
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
    }
}