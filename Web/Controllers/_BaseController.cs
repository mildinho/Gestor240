using Microsoft.AspNetCore.Mvc;
using Web.Services;

namespace Web.Controllers
{

    public abstract class _BaseController<T> : Controller where T : _BaseController<T>
    {

        private IConfiguration? _configuration;
        private IntegracaoApi? _integracaoApi;


        protected IConfiguration? Configuration => _configuration ?? (_configuration = HttpContext?.RequestServices.GetService<IConfiguration>());


        //protected IntegracaoApi ExecutaAPI => new IntegracaoApi(Configuration);
        protected IntegracaoApi? ExecutaAPI => _integracaoApi ?? (_integracaoApi = HttpContext?.RequestServices.GetService<IntegracaoApi>());


    }
}
