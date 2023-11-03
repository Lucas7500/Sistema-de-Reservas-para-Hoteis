using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sistema_de_Reservas_para_Hoteis.Enum;

namespace Sistema_de_Reservas_para_Hoteis
{
    public class Reserva
    {
        public int Id { get; set; } = -1;
        public string Cpf { get; set; } = string.Empty;
        public string Nome { get; set; } = string.Empty;
        public int Idade { get; set; }
        public string Telefone { get; set; } = string.Empty;
        public GeneroEnum Sexo { get; set; }
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }
        public decimal PrecoDaEstadia { get; set; }
        public bool PagamentoEfetuado { get; set; } = true;
    }
}
