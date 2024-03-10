using Dominio.Entidades;
using Dominio.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace API.Controllers
{
    [ApiController]
    [Route("v1/[controller]")]
    [Authorize]
    public class RemessaController : Controller
    {
        private readonly IUnitOfWork _UOW;
        private readonly IRemessa _remessa;
        public RemessaController(IUnitOfWork unitOfWork, IRemessa remessa)
        {
            _UOW = unitOfWork;
            _remessa = remessa;
        }


        [HttpGet]
        [Route("GerarPagamento")]
        public async Task<ActionResult<JsonResult>> Gerar(int IdBeneficiario, int IDConta, DateTime Inicio, DateTime Fim)
        {
            string file = await _remessa.Pagamento(IdBeneficiario, IDConta, Inicio, Fim);

            return File(Encoding.UTF8.GetBytes("Fsdfadsfa"),
                "text/plain",
                 string.Format("{0}.hl7", 1));
        }
    }
}
