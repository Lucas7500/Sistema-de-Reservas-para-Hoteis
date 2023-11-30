using FluentMigrator;

namespace Infraestrutura.Migrations
{
    [Migration(20231121103700)]
    public class _20231121103700AddTabelaReservas : Migration
    {
        public override void Up()
        {
            Create.Table("TabelaReservas")
                .WithColumn("Id").AsInt32().PrimaryKey().Identity().NotNullable()
                .WithColumn("Nome").AsString().NotNullable()
                .WithColumn("Cpf").AsString().NotNullable()
                .WithColumn("Telefone").AsString().NotNullable()
                .WithColumn("Idade").AsInt32().NotNullable()
                .WithColumn("Sexo").AsString().NotNullable()
                .WithColumn("CheckIn").AsDate().NotNullable()
                .WithColumn("CheckOut").AsDate().NotNullable()
                .WithColumn("PrecoEstadia").AsDecimal().NotNullable()
                .WithColumn("PagamentoEfetuado").AsBoolean().NotNullable();
        }

        public override void Down()
        {
            Delete.Table("TabelaReservas");
        }
    }
}
