using Web.Interface;

namespace Web.Services
{
    public static class DI
    {

        public static IServiceCollection Add_DI(this IServiceCollection services, IConfiguration configuration)
        {


            services.AddScoped<IntegracaoApi>();
            

          
            return services;
        }

    }
}
