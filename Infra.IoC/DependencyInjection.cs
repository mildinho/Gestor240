using Dominio.Interfaces;
using Infra.Data.Contexto;
using Infra.Data.Repositories;
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
            
            bool Casa = false;
            ServerVersion serverVersion ;
            if (Casa)
            {
                serverVersion = new MySqlServerVersion(ServerVersion.AutoDetect(configuration.GetConnectionString("ConexaoDB")));
            }
            else
            {
                serverVersion = new MariaDbServerVersion(ServerVersion.AutoDetect(configuration.GetConnectionString("ConexaoDB")));
            }



            services.AddDbContext<DBContexto>(options =>
                    options.UseMySql(configuration.GetConnectionString("ConexaoDB"), serverVersion,
                    builder => builder.MigrationsAssembly("API")));

            services.AddScoped<IUnitOfWork, UnitOfWork>();


            return services;
        }
    }
}