using FluentMigrator;

namespace Sistema_de_Reservas_para_Hoteis
{
    public class AddTabelaReservas : Migration
    {
        public override void Up()
        {
            Create.Table("TabelaReservas")
                .WithColumn("Id").AsInt64().PrimaryKey().Identity()
                .WithColumn("Nome").AsString()
                .WithColumn("Cpf").AsString()
                .WithColumn("Telefone").AsString()
                .WithColumn("Idade").AsInt32()
                .WithColumn("Sexo").AsInt16()
                .WithColumn("CheckIn").AsDateTime()
                .WithColumn("CheckOut").AsDateTime()
                .WithColumn("PrecoEstadia").AsDecimal()
                .WithColumn("PagamentoEfetuado").AsBoolean();
        }

        public override void Down()
        {
            Delete.Table("TabelaReservas");
        }
    }
}
}
