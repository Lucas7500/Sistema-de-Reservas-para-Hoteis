using Dominio.Enums;
using LinqToDB;
using LinqToDB.Mapping;

namespace Dominio
{
    [Table("TabelaReservas")]
    public class Reserva
    {
        [PrimaryKey, Identity]
        public int Id { get; set; }
        [Column("Nome"), NotNull]
        public string Nome { get; set; } = string.Empty;
        [Column("Cpf"), NotNull]
        public string Cpf { get; set; } = string.Empty;
        [Column("Telefone"), NotNull]
        public string Telefone { get; set; } = string.Empty;
        [Column("Idade"), NotNull]
        public int Idade { get; set; }
        [Column("Sexo"), NotNull]
        public GeneroEnum Sexo { get; set; }
        [Column("CheckIn"), NotNull]
        public DateTime CheckIn { get; set; }
        [Column("CheckOut"), NotNull]
        public DateTime CheckOut { get; set; }
        [Column("PrecoEstadia"), NotNull]
        public decimal PrecoEstadia { get; set; }
        [Column("PagamentoEfetuado"), NotNull]
        public bool PagamentoEfetuado { get; set; }

        public object ShallowCopy()
        {
            return this.MemberwiseClone();
        }

    }
}
