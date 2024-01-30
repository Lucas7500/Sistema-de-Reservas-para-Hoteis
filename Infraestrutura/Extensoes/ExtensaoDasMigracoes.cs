using FluentMigrator.Runner;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Infraestrutura.Extensoes
{
    public static class ExtensaoDasMigracoes
    {
        public static void ExecutarMigracoes(this IServiceCollection service)
        {
            const string nomeConexao = "BDSistemaReservas";

            service
               .AddFluentMigratorCore()
                .ConfigureRunner(rb => rb
                    .AddSqlServer()
                    .WithGlobalConnectionString(System.Configuration.ConfigurationManager.ConnectionStrings[nomeConexao].ConnectionString)
                    .ScanIn(Assembly.GetExecutingAssembly()).For.Migrations())
                .AddLogging(lb => lb.AddFluentMigratorConsole())
                .BuildServiceProvider(false);
        }
    }
}
