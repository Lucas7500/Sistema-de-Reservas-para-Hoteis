using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema_de_Reservas_para_Hoteis
{
    public class Reserva
    {
        private string Id { get; set; } = string.Empty;
        private string Cpf { get; set; } = string.Empty;
        private string Nome { get; set; } = string.Empty;
        private int Idade { get; set; }
        private string Telefone { get; set; } = string.Empty;
        private enum Genero
        {
            Masculino,
            Feminino
        }
        private DateTime CheckIn { get; set; }
        private DateTime CheckOut { get; set; }
        private decimal PrecoDaEstadia { get; set; }
        private bool QuartoDisponivel { get; set; } = true;
    }
}
