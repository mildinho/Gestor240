using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    public abstract class _BaseController<T> : Controller where T : _BaseController<T>
    {

        [Route("/PageNotFound")]
        public IActionResult PageNotFound()
        {
            return View();
        }
    }
}
