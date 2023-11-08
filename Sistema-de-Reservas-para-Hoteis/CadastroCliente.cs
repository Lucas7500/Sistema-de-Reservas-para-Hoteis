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
            DataCheckIn.MinDate = DateTime.Now;
            DataCheckOut.MinDate = DateTime.Now;
        }

        Reserva reserva = new Reserva();

        private bool LerDadosDaReserva()
        {
            try
            {
                reserva.Nome = TextoNome.Text;
                reserva.Cpf = TextoCPF.Text;
                reserva.Telefone = TextoTelefone.Text;

                // Pré-Validação da idade
                if (String.IsNullOrWhiteSpace(TextoIdade.Text))
                {
                    Validacoes.ListaExcessoes.Add(MensagemExcessao.IdadeNaoPreenchida);
                }
                else
                {
                    reserva.Idade = int.Parse(TextoIdade.Text);
                }

                reserva.Sexo = (GeneroEnum)CaixaSexo.SelectedItem;
                reserva.CheckIn = Convert.ToDateTime(DataCheckIn.Value.Date);
                reserva.CheckOut = Convert.ToDateTime(DataCheckOut.Value.Date);

                // Pré-Validação do Preço
                if (String.IsNullOrWhiteSpace(TextoPreco.Text))
                {
                    Validacoes.ListaExcessoes.Add(MensagemExcessao.PrecoNaoPreenchido);
                }
                else
                {
                    reserva.PrecoEstadia = Decimal.Parse(TextoPreco.Text);
                }

                // Validação dos Botões
                if (!BotaoTrue.Checked && !BotaoFalse.Checked)
                {
                    Validacoes.ListaExcessoes.Add(MensagemExcessao.PagamentoNaoInformado);
                }
                else
                {
                    reserva.PagamentoEfetuado = BotaoTrue.Checked;
                }

                Validacoes.ValidarCampos(reserva);
            }
            catch
            {
                MessageBox.Show(String.Join("\n\n", Validacoes.ListaExcessoes), "Erro no Cadastro" , MessageBoxButtons.OKCancel ,MessageBoxIcon.Error);
                Validacoes.ListaExcessoes.Clear();

                return false;
            }

            return true;
        }

        private void AoClicarEmAdicionar(object sender, EventArgs e)
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
            if (e.KeyChar == ',')
            {
                e.Handled = (TextoPreco.Text.Contains(','));
                return;
            }

            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }
}
