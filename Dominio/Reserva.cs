using Dominio.Constantes;
using Dominio.Enums;
using LinqToDB;
using LinqToDB.Mapping;

namespace Dominio
{
    [Table(CamposTabela.NOME_TABELA)]
    public class Reserva
    {
        [PrimaryKey, Identity]
        public int Id { get; set; }
        [Column(CamposTabela.COLUNA_NOME), NotNull]
        public string Nome { get; set; } = string.Empty;
        [Column(CamposTabela.COLUNA_CPF), NotNull]
        public string Cpf { get; set; } = string.Empty;
        [Column(CamposTabela.COLUNA_TELEFONE), NotNull]
        public string Telefone { get; set; } = string.Empty;
        [Column(CamposTabela.COLUNA_IDADE), NotNull]
        public int Idade { get; set; }
        [Column(CamposTabela.COLUNA_SEXO), NotNull]
        public GeneroEnum Sexo { get; set; }
        [Column(CamposTabela.COLUNA_CHECK_IN), NotNull]
        public DateTime CheckIn { get; set; }
        [Column(CamposTabela.COLUNA_CHECK_OUT), NotNull]
        public DateTime CheckOut { get; set; }
        [Column(CamposTabela.COLUNA_PRECO_ESTADIA), NotNull]
        public decimal PrecoEstadia { get; set; }
        [Column(CamposTabela.COLUNA_PAGAMENTO_EFETUADO), NotNull]
        public bool PagamentoEfetuado { get; set; }

        public object ShallowCopy()
        {
            return this.MemberwiseClone();
        }

    }
}
