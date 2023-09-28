using Microsoft.AspNetCore.Mvc;

namespace Web.Biblioteca.CRUD
{
    public class CRUD_AcaoViewComponent : ViewComponent
    {


        public IViewComponentResult Invoke()
        {
            return View();
        }


    }
}
