using Dominio.Interfaces;
using Infra.Data.Contexto;
using Infra.Data.Repositories;
using Infra.Data.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

/*
https://learn.microsoft.com/en-us/aspnet/core/fundamentals/dependency-injection?view=aspnetcore-6.0
*/

namespace Infra.IoC
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfraStructure(this IServiceCollection services, IConfiguration configuration)
        {

            ServerVersion serverVersion;

            string ConfiguracaoDB = "MYSQL";

            if (configuration.GetConnectionString("BancoDados").ToUpper().Length > 0)
                ConfiguracaoDB = configuration.GetConnectionString("BancoDados").ToUpper();



            if (ConfiguracaoDB == "MYSQL" || ConfiguracaoDB == "MARIADB")
            {
                if (ConfiguracaoDB == "MYSQL")
                {
                    serverVersion = new MySqlServerVersion(ServerVersion.AutoDetect(configuration.GetConnectionString("ConexaoDB")));
                }
                else
                {
                    serverVersion = new MariaDbServerVersion(ServerVersion.AutoDetect(configuration.GetConnectionString("ConexaoDB")));
                }

                services.AddDbContext<DBContexto>(options =>
                    {
                        options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
                        options.UseMySql(configuration.GetConnectionString("ConexaoDB"),
                            serverVersion,
                            builder => builder.MigrationsAssembly("API"));
                    });
            }
            else
            {
                services.AddDbContext<DBContexto>(options =>
                {
                    options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
                    options.UseSqlServer(configuration.GetConnectionString("ConexaoDB"));

                });
            }

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IRemessa, Remessa>();
            services.AddScoped<ILayout, Febraban240>();

            return services;
        }
    }
}