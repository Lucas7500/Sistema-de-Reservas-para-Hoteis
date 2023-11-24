using FluentMigrator;

namespace Sistema_de_Reservas_para_Hoteis.Migrations
{
    [Migration(20231124132600)]
    public class UpdateTabelaReservas : Migration
    {
        public override void Up()
        {
            Alter.Table("TabelaReservas").AlterColumn("PrecoEstadia").AsDecimal(12, 2).NotNullable();
        }

        public override void Down()
        {
            Delete.Table("TabelaReservas");
        }
    }
}
