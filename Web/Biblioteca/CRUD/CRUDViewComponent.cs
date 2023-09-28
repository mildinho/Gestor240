using Microsoft.AspNetCore.Mvc;

namespace Web.Biblioteca.CRUD
{
    public class CRUDViewComponent : ViewComponent
    {


        public IViewComponentResult Invoke()
        {
            return View();
        }


    }
}
