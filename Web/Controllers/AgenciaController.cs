using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    public class AgenciaController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
