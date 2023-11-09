using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sistema_de_Reservas_para_Hoteis.Enums;

namespace Sistema_de_Reservas_para_Hoteis
{
    public class Reserva
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Cpf { get; set; } = string.Empty;
        public string Telefone { get; set; } = string.Empty;
        public int Idade { get; set; }
        public GeneroEnum Sexo { get; set; }
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }
        public decimal PrecoEstadia { get; set; }
        public bool? PagamentoEfetuado { get; set; }
    }
}
