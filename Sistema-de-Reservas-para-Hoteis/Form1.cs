namespace Sistema_de_Reservas_para_Hoteis
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private string Cpf { get; set; } = string.Empty;
        private string ClientName { get; set; } = string.Empty;
        private int Age { get; set; }
        private string PhoneNumber { get; set; } = string.Empty;
        private enum Gender
        {
            Male,
            Female
        }
        private DateTime CheckIn { get; set; }
        private DateTime CheckOut { get; set; }
       private decimal StayPrice {  get; set; }
       private bool IsRoomAvailable { get; set; }

    }
}