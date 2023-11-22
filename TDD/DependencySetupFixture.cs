using Dominio.Interfaces;
using Infra.Data.Contexto;
using Infra.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace TDD
{
    public class DependencySetupFixture 
    {
        //https://stackoverflow.com/questions/55497800/populate-iconfiguration-for-unit-tests
        private string ConexaoTest = $"Server = localhost; userid = developer; password = furacao123adm; database = _Gesto240_Teste";
        private string ConfiguracaoDB = "MYSQL";


        public ServiceProvider ServiceProvider { get; private set; }
        public IConfiguration configuration { get; private set; }

        public DependencySetupFixture()
        {
            var serviceCollection = new ServiceCollection();
            //configuration = new ConfigurationBuilder();

            
            ServerVersion serverVersion;

            ConfiguracaoDB = configuration.GetConnectionString("BancoDados").ToUpper();
            if (ConfiguracaoDB == "MYSQL")
            {
                serverVersion = new MySqlServerVersion(ServerVersion.AutoDetect(ConexaoTest));
            }
            else
            {
                serverVersion = new MariaDbServerVersion(ServerVersion.AutoDetect(ConexaoTest));
            }


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

