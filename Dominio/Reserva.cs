using Dominio.Constantes;
using Dominio.Enums;
using LinqToDB;
using LinqToDB.Mapping;

namespace Dominio
{
    [Table(CamposTabelaReservas.NOME_TABELA)]
    public class Reserva
    {
        [PrimaryKey, Identity]
        public int Id { get; set; }
        [Column(CamposTabelaReservas.COLUNA_NOME), NotNull]
        public string Nome { get; set; } = string.Empty;
        [Column(CamposTabelaReservas.COLUNA_CPF), NotNull]
        public string Cpf { get; set; } = string.Empty;
        [Column(CamposTabelaReservas.COLUNA_TELEFONE), NotNull]
        public string Telefone { get; set; } = string.Empty;
        [Column(CamposTabelaReservas.COLUNA_IDADE), NotNull]
        public int Idade { get; set; }
        [Column(CamposTabelaReservas.COLUNA_SEXO), NotNull]
        public GeneroEnum Sexo { get; set; }
        [Column(CamposTabelaReservas.COLUNA_CHECK_IN), NotNull]
        public DateTime CheckIn { get; set; }
        [Column(CamposTabelaReservas.COLUNA_CHECK_OUT), NotNull]
        public DateTime CheckOut { get; set; }
        [Column(CamposTabelaReservas.COLUNA_PRECO_ESTADIA), NotNull]
        public decimal PrecoEstadia { get; set; }
        [Column(CamposTabelaReservas.COLUNA_PAGAMENTO_EFETUADO), NotNull]
        public bool PagamentoEfetuado { get; set; }

        public object ShallowCopy()
        {
            return this.MemberwiseClone();
        }

    }
}
