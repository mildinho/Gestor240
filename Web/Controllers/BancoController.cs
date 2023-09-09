using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Web.Services;

namespace Web.Controllers
{
    public class BancoController : Controller
    {

        private readonly IConfiguration _configuration;

        public BancoController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<IActionResult> Index()
        {
            IntegracaoApi ret = new(_configuration);
            List<string> param = new();

            var retornoApi = await ret.GetAPI("Banco/GetAll");
            return View();
        }
    }
}
