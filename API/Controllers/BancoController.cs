using Dominio.Entidades;
using Dominio.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("v1/[controller]")]
    public class BancoController : Controller
    {
        private readonly IUnitOfWork _UOW;
        public BancoController(IUnitOfWork unitOfWork)
        {
            _UOW = unitOfWork;
        }

        [HttpGet]
        public async Task<ActionResult<Banco>> Get(string Codigo)
        {
            var Objeto = await _UOW.Banco.PesquisarPorCodigoAsync(Codigo);

            return Ok(Objeto);
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<ActionResult<Banco>> GetAll()
        {
            var Objeto = _UOW.Banco.ListarTodos();

            return Ok(Objeto);
        }




        [HttpPost]
        public async Task<ActionResult<Banco>> Post(Banco tabela)
        {

            if (ModelState.IsValid)
            {
                var Objeto = await _UOW.Banco.InserirAsync(tabela);

                await _UOW.SaveAsync();
                return Ok(Objeto);

            }
            else
            {
                var errors = string.Join(" | ", ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage));

                return BadRequest(errors);

            }

        }

    }
}
