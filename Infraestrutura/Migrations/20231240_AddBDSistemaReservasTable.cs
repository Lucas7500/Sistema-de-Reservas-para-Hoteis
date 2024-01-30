using Dominio.Constantes;
using FluentMigrator;
using FluentMigrator.SqlServer;

namespace Infraestrutura.Migrations
{
    [Migration(20240103115300)]
    public class _20240103115300AddTabelaReservas : Migration
    {
        public override void Up()
        {
            Create.Table(ConstantesTabelaReservas.NOME_TABELA)
                .WithColumn(ConstantesTabelaReservas.COLUNA_ID).AsInt64().PrimaryKey().Identity(1, 1).NotNullable()
                .WithColumn(ConstantesTabelaReservas.COLUNA_NOME).AsString().NotNullable()
                .WithColumn(ConstantesTabelaReservas.COLUNA_CPF).AsString().NotNullable()
                .WithColumn(ConstantesTabelaReservas.COLUNA_TELEFONE).AsString().NotNullable()
                .WithColumn(ConstantesTabelaReservas.COLUNA_IDADE).AsInt32().NotNullable()
                .WithColumn(ConstantesTabelaReservas.COLUNA_SEXO).AsString().NotNullable()
                .WithColumn(ConstantesTabelaReservas.COLUNA_CHECK_IN).AsDate().NotNullable()
                .WithColumn(ConstantesTabelaReservas.COLUNA_CHECK_OUT).AsDate().NotNullable()
                .WithColumn(ConstantesTabelaReservas.COLUNA_PRECO_ESTADIA).AsDecimal(12, 2).NotNullable()
                .WithColumn(ConstantesTabelaReservas.COLUNA_PAGAMENTO_EFETUADO).AsBoolean().NotNullable();
        }

        public override void Down()
        {
            Delete.Table(ConstantesTabelaReservas.NOME_TABELA);
        }
    }
}
