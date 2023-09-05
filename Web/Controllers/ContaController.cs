using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    public class ContaController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
