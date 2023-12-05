using Dominio;
using FluentMigrator.Runner;
using Infraestrutura;
using Infraestrutura.Extensoes;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using FluentValidation;
using FluentValidation.AspNetCore;

namespace Interacao
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();

            var builder = CriaHostBuilder();
            using var build = builder.Build();
            var servicesProvider = build.Services;
            var scope = servicesProvider.CreateScope();

            UpdateDatabase(scope.ServiceProvider);

            var form = servicesProvider.GetRequiredService<TelaListaDeReservas>();

            Application.Run(form);
        }

        private static void UpdateDatabase(IServiceProvider serviceProvider)
        {
            var runner = serviceProvider.GetRequiredService<IMigrationRunner>();

            runner.MigrateUp();
        }

        static IHostBuilder CriaHostBuilder()
        {
            return Host.CreateDefaultBuilder()
                .ConfigureServices((context, services) => {
                    services.AddScoped<TelaListaDeReservas>();
                    services.AddScoped<IRepositorio, RepositorioSqlServer>();
                    services.AddScoped<IValidator<Reserva>, ReservaFluentValidation>();
                    services.ExecutarMigracoes();
                });
        }
    }
}