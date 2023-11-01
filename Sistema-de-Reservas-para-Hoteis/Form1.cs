namespace Sistema_de_Reservas_para_Hoteis
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            AtualizarGrid();
        }

        private static List<Reserva> Lista()
        {
            List<Reserva> reservas = new List<Reserva>();
            reservas.Add(new Reserva()
            {
                Id = 1,
                Cpf = "123456789-00",
                Nome = "Joao Da Silva",
                Idade = 23,
                Telefone = "(00)91234-5678",
                Sexo = Reserva.Genero.Feminino,
                CheckIn = new DateTime(2023, 10, 12),
                CheckOut = new DateTime(2023, 10, 24),
                PrecoDaEstadia = 800.00M,
                PagamentoEfetuado = true
            });

            return reservas;
        }

        private void AtualizarGrid()
        {
            dataGridView.DataSource = Lista();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}