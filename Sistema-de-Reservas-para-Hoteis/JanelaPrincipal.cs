using System.Globalization;
using Sistema_de_Reservas_para_Hoteis.Enum;

namespace Sistema_de_Reservas_para_Hoteis
{
    public partial class JanelaPrincipal : Form
    {
        public JanelaPrincipal()
        {
            CultureInfo culture = CultureInfo.InvariantCulture;
            InitializeComponent();
            //PreencherGrid();
        }

        private static List<Reserva> Lista()
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

            return reservas;
        }

        private void PreencherGrid()
        {
            TelaDaLista.DataSource = Lista();
        }

        private void BotaoAdicionar_Click(object sender, EventArgs e)
        {
            CadastroCliente frm2 = new CadastroCliente();
            frm2.Show();
        }
    }
}