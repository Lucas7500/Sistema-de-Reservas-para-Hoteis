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
        public TelaCadastroCliente()
        {
            InitializeComponent();
            CaixaSexo.DataSource = Enum.GetValues(typeof(GeneroEnum));
            if (TelaListaDeReservas.tipoDeModificacao == (int)TelaListaDeReservas.CRUD.Adicionar)
            {
                DataCheckIn.MinDate = DateTime.Now;
                DataCheckOut.MinDate = DateTime.Now;
            }
        }

        public bool LerDadosDaReserva(Reserva reserva)
        {
            try
            {
                reserva.Nome = TextoNome.Text;
                reserva.Cpf = TextoCPF.Text;
                reserva.Telefone = TextoTelefone.Text;
                reserva.Idade = String.IsNullOrWhiteSpace(TextoIdade.Text) ? Validacoes.codigoDeErro : int.Parse(TextoIdade.Text);
                reserva.Sexo = (GeneroEnum)CaixaSexo.SelectedItem;
                reserva.CheckIn = Convert.ToDateTime(DataCheckIn.Value.Date);
                reserva.CheckOut = Convert.ToDateTime(DataCheckOut.Value.Date);
                reserva.PrecoEstadia = String.IsNullOrWhiteSpace(TextoPreco.Text) ? Validacoes.codigoDeErro : ConverterEmDecimalComVirgula(TextoPreco.Text);
                reserva.PagamentoEfetuado = !BotaoTrue.Checked && !BotaoFalse.Checked ? null : BotaoTrue.Checked;
                Validacoes.ValidarCampos(reserva);
            }
            catch
            {
                return false;
            }

            return true;
        }

        public void PreencherDadosDaReserva(Reserva reservaEdicao)
        {
            TextoNome.Text = reservaEdicao.Nome;
            TextoCPF.Text = reservaEdicao.Cpf;
            TextoTelefone.Text = reservaEdicao.Telefone;
            TextoIdade.Text = reservaEdicao.Idade.ToString();
            CaixaSexo.SelectedItem = reservaEdicao.Sexo;
            DataCheckIn.Value = reservaEdicao.CheckIn;
            DataCheckOut.Value = reservaEdicao.CheckOut;
            TextoPreco.Text = reservaEdicao.PrecoEstadia.ToString();
            if (reservaEdicao.PagamentoEfetuado != null)
            {
                BotaoTrue.Checked = (bool)reservaEdicao.PagamentoEfetuado;
                BotaoFalse.Checked = (bool)!reservaEdicao.PagamentoEfetuado;
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

        private void AoClicarAdicionarCadastro(object sender, EventArgs e)
        {
            if (TelaListaDeReservas.tipoDeModificacao == (int)TelaListaDeReservas.CRUD.Adicionar)
            {
                Reserva reserva = new();
                if (LerDadosDaReserva(reserva))
                {
                    TelaListaDeReservas.AdicionarReservaNaLista(reserva);
                    this.Close();
                }
            }
            else if (TelaListaDeReservas.tipoDeModificacao == (int)TelaListaDeReservas.CRUD.Editar)
            {
                this.Close();
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