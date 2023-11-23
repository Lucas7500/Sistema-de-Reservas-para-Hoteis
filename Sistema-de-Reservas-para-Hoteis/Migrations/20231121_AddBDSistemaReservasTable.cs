using FluentMigrator;

namespace Sistema_de_Reservas_para_Hoteis.Migrations
{
    [Migration(20231121103700)]
    public class AddTabelaReservas : Migration
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
