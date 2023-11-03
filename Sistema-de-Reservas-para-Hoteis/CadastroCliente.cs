using Sistema_de_Reservas_para_Hoteis.Enum;
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
        }

        private void BotaoAdicionarCadastro_Click(object sender, EventArgs e)
        {
            
            var reservas = new List<Reserva>()
            {
                new Reserva
                {
                    Id = 1,
                    Cpf = "123456789-00",
                    Nome = "Joao Da Silva",
                    Idade = 23,
                    Telefone = "(00)91234-5678",
                    Sexo = GeneroEnum.Feminino,
                    CheckIn = new DateTime(2023, 10, 12),
                    CheckOut = new DateTime(2023, 10, 24),
                    PrecoDaEstadia = 800.00M,
                    PagamentoEfetuado = true
                }
            };
        }
    }
}
