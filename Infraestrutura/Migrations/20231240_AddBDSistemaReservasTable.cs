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
            Create.Table(CamposTabelaReservas.NOME_TABELA)
                .WithColumn(CamposTabelaReservas.COLUNA_ID).AsInt64().PrimaryKey().Identity(1, 1).NotNullable()
                .WithColumn(CamposTabelaReservas.COLUNA_NOME).AsString().NotNullable()
                .WithColumn(CamposTabelaReservas.COLUNA_CPF).AsString().NotNullable()
                .WithColumn(CamposTabelaReservas.COLUNA_TELEFONE).AsString().NotNullable()
                .WithColumn(CamposTabelaReservas.COLUNA_IDADE).AsInt32().NotNullable()
                .WithColumn(CamposTabelaReservas.COLUNA_SEXO).AsString().NotNullable()
                .WithColumn(CamposTabelaReservas.COLUNA_CHECK_IN).AsDate().NotNullable()
                .WithColumn(CamposTabelaReservas.COLUNA_CHECK_OUT).AsDate().NotNullable()
                .WithColumn(CamposTabelaReservas.COLUNA_PRECO_ESTADIA).AsDecimal(12, 2).NotNullable()
                .WithColumn(CamposTabelaReservas.COLUNA_PAGAMENTO_EFETUADO).AsBoolean().NotNullable();
        }

        public override void Down()
        {
            Delete.Table(CamposTabelaReservas.NOME_TABELA);
        }
    }
}
