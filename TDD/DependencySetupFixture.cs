using Dominio.Interfaces;
using Infra.Data.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace TDD
{
    public class DependencySetupFixture
    {
         public DependencySetupFixture()
        {
            var serviceCollection = new ServiceCollection();

              serviceCollection.AddScoped<IUnitOfWork, UnitOfWork>();

            ServiceProvider = serviceCollection.BuildServiceProvider();
        }

        public ServiceProvider ServiceProvider { get; private set; }
    }


}

