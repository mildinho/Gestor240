using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Web.Services;

namespace Web.Controllers
{
    public class BancoController : Controller
    {

        private readonly IntegracaoApi _integracaoApi;

        public BancoController(IntegracaoApi integracaoApi)
        {
            _integracaoApi = integracaoApi;
        }

        public async Task<IActionResult> Index()
        {
            var retornoApi = await _integracaoApi.GetAPI("Banco/GetAll");
            var obj =  JsonConvert.DeserializeObject<List<Object>>(retornoApi.data);

            return View(retornoApi);
        }
    }
}
