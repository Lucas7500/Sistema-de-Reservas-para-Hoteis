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
    public partial class CadastroCliente : Form
    {
        public CadastroCliente()
        {
            InitializeComponent();
            CaixaSexo.DataSource = Enum.GetValues(typeof(GeneroEnum));
            
            if (JanelaPrincipal.tipoDeModificacao == (int)JanelaPrincipal.CRUD.Adicionar)
            {
                DataCheckIn.MinDate = DateTime.Now;
                DataCheckOut.MinDate = DateTime.Now;
            }
        }
       
        readonly Reserva reserva = new();
        public const int codigoDeErro = -1;

        private bool LerDadosDaReserva()
        {
            try
            {
                if (JanelaPrincipal.tipoDeModificacao == (int)JanelaPrincipal.CRUD.Editar)
                {
                    JanelaPrincipal.reservaSelecionada.Nome = TextoNome.Text;
                    JanelaPrincipal.reservaSelecionada.Cpf = TextoCPF.Text;
                    JanelaPrincipal.reservaSelecionada.Telefone = TextoTelefone.Text;
                    JanelaPrincipal.reservaSelecionada.Idade = String.IsNullOrWhiteSpace(TextoIdade.Text) ? codigoDeErro : int.Parse(TextoIdade.Text);
                    JanelaPrincipal.reservaSelecionada.Sexo = (GeneroEnum)CaixaSexo.SelectedItem;
                    JanelaPrincipal.reservaSelecionada.CheckIn = Convert.ToDateTime(DataCheckIn.Value.Date);
                    JanelaPrincipal.reservaSelecionada.CheckOut = Convert.ToDateTime(DataCheckOut.Value.Date);
                    JanelaPrincipal.reservaSelecionada.PrecoEstadia = String.IsNullOrWhiteSpace(TextoPreco.Text) ? codigoDeErro : ConverterEmDecimalComVirgula(TextoPreco.Text);
                    JanelaPrincipal.reservaSelecionada.PagamentoEfetuado = !BotaoTrue.Checked && !BotaoFalse.Checked ? null : BotaoTrue.Checked;
                    Validacoes.ValidarCampos(JanelaPrincipal.reservaSelecionada);
                }
                else
                {
                    reserva.Nome = TextoNome.Text;
                    reserva.Cpf = TextoCPF.Text;
                    reserva.Telefone = TextoTelefone.Text;
                    reserva.Idade = String.IsNullOrWhiteSpace(TextoIdade.Text) ? codigoDeErro : int.Parse(TextoIdade.Text);
                    reserva.Sexo = (GeneroEnum)CaixaSexo.SelectedItem;
                    reserva.CheckIn = Convert.ToDateTime(DataCheckIn.Value.Date);
                    reserva.CheckOut = Convert.ToDateTime(DataCheckOut.Value.Date);
                    reserva.PrecoEstadia = String.IsNullOrWhiteSpace(TextoPreco.Text) ? codigoDeErro : ConverterEmDecimalComVirgula(TextoPreco.Text);
                    reserva.PagamentoEfetuado = !BotaoTrue.Checked && !BotaoFalse.Checked ? null : BotaoTrue.Checked;
                    Validacoes.ValidarCampos(reserva);
                }
            }
            catch
            {
                MessageBox.Show(String.Join("\n\n", Validacoes.ListaExcessoes), "Erro no Cadastro" , MessageBoxButtons.OK ,MessageBoxIcon.Error);
                Validacoes.ListaExcessoes.Clear();

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
            if (LerDadosDaReserva())
            {
                JanelaPrincipal.AdicionarReservaNaLista(reserva);
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
