using Dominio.Interfaces;
using Infra.Data.Contexto;
using Infra.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace TDD
{

    public class DependencySetupFixture //: IDisposable
    {
        //https://stackoverflow.com/questions/55497800/populate-iconfiguration-for-unit-tests
        private string ConfiguracaoDB = "MARIADB";

        private string ConexaoTest = $"Server = localhost; userid = root; password = furacaoadm; database = _Gesto240_Teste";
        //private string ConexaoTest = $"Server = localhost; userid = developer; password = furacao123adm; database = _Gesto240_Teste";

        public ServiceProvider ServiceProvider { get; private set; }



        public DependencySetupFixture()
        {
            var serviceCollection = new ServiceCollection();

            ServerVersion serverVersion;
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

            ServiceProvider = serviceCollection.BuildServiceProvider();

            // Garantindo que será criado a Base de Dados
            using (var context = ServiceProvider.GetService<DBContexto>())
            {
                context.Database.EnsureCreated();
            }


        }

        public void Dispose()
        {
            using (var context = ServiceProvider.GetService<DBContexto>())
            {
                context.Database.EnsureDeleted();
            }
        }
    }


}

