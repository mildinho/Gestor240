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

        [HttpGet("Codigo")]
        public async Task<ActionResult<Banco>> Get(int Codigo)
        {
            var Objeto = await _UOW.Banco.PesquisarPorCodigoAsync(Codigo);

            return Ok(Objeto);
        }

        [HttpGet]
        [Route("GetAll")]
        public ActionResult<Banco> GetAll()
        {
            var Objeto = _UOW.Banco.ListarTodos();

            return Ok(Objeto);
        }




        [HttpPost]
        public async Task<ActionResult<Banco>> Post(Banco tabela)
        {
            IEnumerable<Banco> BancoLista = await _UOW.Banco.PesquisarPorCodigoAsync(tabela.Codigo);
            if (BancoLista.Any())
            {
                return BadRequest("O código deste Banco já existe cadastrado!");
            }

            if (ModelState.IsValid)
            {
                Banco Objeto = await _UOW.Banco.InserirAsync(tabela);

                await _UOW.SaveAsync();
                return Ok(Objeto);

            }
            return BadRequest();

        }

        [HttpPatch]
        public async Task<ActionResult<Banco>> Patch(int Id, Banco tabela)
        {
            if (Id != tabela.Id)
                return BadRequest("O para ID está diferente do ID do Modelo!");


            if (ModelState.IsValid)
            {

                var Objeto = await _UOW.Banco.AtualizarAsync(tabela);

                await _UOW.SaveAsync();
                return Ok(Objeto);

            }
            return BadRequest();

        }

        [HttpDelete("{Id}")]
        public async Task<ActionResult<int>> Delete(int Id)
        {

            await _UOW.Banco.DeletarAsync(Id);

            int _removidos = await _UOW.SaveAsync();
            return Ok(_removidos);

        }


    }
}
