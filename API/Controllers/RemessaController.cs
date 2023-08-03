using Dominio.Entidades;
using Dominio.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("v1/[controller]")]

    public class RemessaController : Controller
    {
        private readonly IUnitOfWork _UOW;
        public RemessaController(IUnitOfWork unitOfWork)
        {
            _UOW = unitOfWork;
        }


        [HttpGet]
        [Route("GetAll")]
        public ActionResult<JsonResult> Gerar(DateTime Inicio, DateTime Fim)
        {
            

            return Ok();
        }
    }
}
