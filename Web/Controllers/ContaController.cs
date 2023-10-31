using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    public class ContaController : _BaseController<ContaController>
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
