using FluentMigrator;
using FluentMigrator.SqlServer;

namespace Infraestrutura.Migrations
{
    [Migration(20231205124100)]
    public class _20231205124100AddTabelaReservas : Migration
    {
        public override void Up()
        {
            Create.Table("TabelaReservas")
                .WithColumn("Id").AsInt64().PrimaryKey().Identity(1,1).NotNullable()
                .WithColumn("Nome").AsString().NotNullable()
                .WithColumn("Cpf").AsString().NotNullable()
                .WithColumn("Telefone").AsString().NotNullable()
                .WithColumn("Idade").AsInt32().NotNullable()
                .WithColumn("Sexo").AsString().NotNullable()
                .WithColumn("CheckIn").AsDate().NotNullable()
                .WithColumn("CheckOut").AsDate().NotNullable()
                .WithColumn("PrecoEstadia").AsDecimal(12, 2).NotNullable()
                .WithColumn("PagamentoEfetuado").AsBoolean().NotNullable();
        }

        public override void Down()
        {
            Delete.Table("TabelaReservas");
        }
    }
}
