using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema_de_Reservas_para_Hoteis
{
    public class Reserva
    {
        public int Id { get; set; }
        public string Cpf { get; set; } = string.Empty;
        public string Nome { get; set; } = string.Empty;
        public int Idade { get; set; }
        public string Telefone { get; set; } = string.Empty;
        public enum Genero
        {
            Masculino,
            Feminino
        }
        public Genero Sexo { get; set; }
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }
        public decimal PrecoDaEstadia { get; set; }
        public bool PagamentoEfetuado { get; set; } = true;
    }
}
