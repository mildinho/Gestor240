using Web.Biblioteca.Notification;
using Web.Biblioteca.Session;
using Web.Interface;

namespace Web.Services
{
    public static class DI
    {

        public static IServiceCollection Add_DI(this IServiceCollection services, IConfiguration configuration)
        {
            //Trabalhando com Sessão 
            services.AddHttpContextAccessor();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            
            services.AddDistributedMemoryCache();
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromHours(1);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });

            services.AddScoped<IntegracaoApi>();
            services.AddScoped<ConfiguraSessao>();
            services.AddScoped<SessaoUsuario>();

            return services;
        }

    }
}
