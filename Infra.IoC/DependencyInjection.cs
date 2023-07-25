using Dominio.Interfaces;
using Infra.Data.Contexto;
using Infra.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infra.IoC
{
    public static class DependencyInjection
    {

        public static IServiceCollection AddInfraStructure(this IServiceCollection services,
            IConfiguration configuration)
        {


            string connectionString = configuration.GetConnectionString("Gestor240");
            var serverVersion = new MySqlServerVersion(ServerVersion.AutoDetect(connectionString));

            services.AddDbContext<DBContexto>(options =>
                    options.UseMySql(connectionString, serverVersion));

            services.AddScoped<IUnitOfWork, UnitOfWork>();


            return services;
        }
    }
}