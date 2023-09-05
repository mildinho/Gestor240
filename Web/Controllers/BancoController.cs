using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    public class BancoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
