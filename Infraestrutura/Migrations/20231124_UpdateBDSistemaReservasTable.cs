using FluentMigrator;

namespace Infraestrutura.Migrations
{
    [Migration(20231124132600)]
    public class _20231124132600UpdateTabelaReservas : Migration
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
