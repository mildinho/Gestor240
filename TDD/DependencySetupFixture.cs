using Dominio.Interfaces;
using Infra.Data.Contexto;
using Infra.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace TDD
{
    public class DependencySetupFixture 
    {

        private string ConexaoTest = $"Server = localhost; userid = developer; password = furacao123adm; database = _Gesto240_Teste";

        public ServiceProvider ServiceProvider { get; private set; }


        public DependencySetupFixture()
        {
            var serviceCollection = new ServiceCollection();
            ServerVersion serverVersion;

            serverVersion = new MySqlServerVersion(
                ServerVersion.AutoDetect(ConexaoTest));

            serviceCollection.AddDbContext<DBContexto>(options =>
            {
                options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
                options.UseMySql(ConexaoTest, serverVersion);

            });

            serviceCollection.AddScoped<IUnitOfWork, UnitOfWork>();

            ServiceProvider = serviceCollection.BuildServiceProvider();

            using (var context = ServiceProvider.GetService<DBContexto>())
            {
                context.Database.EnsureCreated();

            }



        }

    }


}

