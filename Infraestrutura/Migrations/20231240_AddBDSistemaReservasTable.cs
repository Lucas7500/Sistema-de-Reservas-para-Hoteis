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
            Create.Table(CamposTabela.NOME_TABELA)
                .WithColumn(CamposTabela.COLUNA_ID).AsInt64().PrimaryKey().Identity(1, 1).NotNullable()
                .WithColumn(CamposTabela.COLUNA_NOME).AsString().NotNullable()
                .WithColumn(CamposTabela.COLUNA_CPF).AsString().NotNullable()
                .WithColumn(CamposTabela.COLUNA_TELEFONE).AsString().NotNullable()
                .WithColumn(CamposTabela.COLUNA_IDADE).AsInt32().NotNullable()
                .WithColumn(CamposTabela.COLUNA_SEXO).AsString().NotNullable()
                .WithColumn(CamposTabela.COLUNA_CHECK_IN).AsDate().NotNullable()
                .WithColumn(CamposTabela.COLUNA_CHECK_OUT).AsDate().NotNullable()
                .WithColumn(CamposTabela.COLUNA_PRECO_ESTADIA).AsDecimal(12, 2).NotNullable()
                .WithColumn(CamposTabela.COLUNA_PAGAMENTO_EFETUADO).AsBoolean().NotNullable();
        }

        public override void Down()
        {
            Delete.Table(CamposTabela.NOME_TABELA);
        }
    }
}
