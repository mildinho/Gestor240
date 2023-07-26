using Dominio.Entidades;
using Dominio.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("v1/[controller]")]
    public class BancoController : Controller
    {
        private readonly IUnitOfWork _IOW;
        public BancoController(IUnitOfWork unitOfWork)
        {
            _IOW = unitOfWork;
        }

        [HttpGet]
        public async Task<ActionResult<Banco>> Get(string Codigo)
        {
            var Objeto = await _IOW.Banco.PesquisarPorCodigoAsync(Codigo);

            if (Objeto == null) return NotFound();

            return Ok(Objeto);
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<ActionResult<Banco>> GetAll()
        {
            var Objeto = _IOW.Banco.ListarTodos();

            if (Objeto == null) return NotFound();

            return Ok(Objeto);
        }

    


    [HttpPost]
        public async Task<ActionResult<Banco>> Post(Banco tabela)
        {
            await _IOW.Banco.InserirAsync(tabela);

            await _IOW.SaveAsync();


            return Ok();
        }

    }
}
