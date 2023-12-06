using Dominio.Enums;

namespace Dominio
{
    public class Reserva
    {
        public object ShallowCopy()
        {
            return this.MemberwiseClone();
        }

        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Cpf { get; set; } = string.Empty;
        public string Telefone { get; set; } = string.Empty;
        public int Idade { get; set; }
        public GeneroEnum Sexo { get; set; }
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }
        public decimal PrecoEstadia { get; set; }
        public bool PagamentoEfetuado { get; set; }
    }
}
