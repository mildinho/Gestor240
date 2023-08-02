using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class RetornoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
