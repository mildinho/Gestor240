using Dominio.Entidades;
using Dominio.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("v1/[controller]")]
    public class UFController : Controller
    {
        private readonly IUnitOfWork _UOW;
        public UFController(IUnitOfWork unitOfWork)
        {
            _UOW = unitOfWork;
        }

        [HttpGet("Sigla")]
        public async Task<ActionResult<UF>> Sigla(string Sigla)
        {
            var Objeto = await _UOW.UF.PesquisarPorSiglaAsync(Sigla);

            return Ok(Objeto);
        }

        [HttpGet("Descricao")]
        public async Task<ActionResult<UF>> Descricao(string Descricao)
        {
            var Objeto = await _UOW.UF.PesquisarPorDescricaoAsync(Descricao);

            return Ok(Objeto);
        }

        [HttpGet]
        [Route("GetAll")]
        public ActionResult<UF> GetAll()
        {
            var Objeto = _UOW.UF.ListarTodos();

            return Ok(Objeto);
        }




        [HttpPost]
        public async Task<ActionResult<UF>> Post(UF tabela)
        {
            IEnumerable<UF> UFLista = await _UOW.UF.PesquisarPorSiglaAsync(tabela.Sigla);
            if (UFLista.Any())
            {
                return BadRequest("O código deste UF já existe cadastrado!");
            }

            if (ModelState.IsValid)
            {
                UF Objeto = await _UOW.UF.InserirAsync(tabela);

                await _UOW.SaveAsync();
                return Ok(Objeto);

            }
            return BadRequest();

        }

        [HttpPatch]
        public async Task<ActionResult<UF>> Patch(int Id, UF tabela)
        {
            if (Id != tabela.Id)
                return BadRequest("O para ID está diferente do ID do Modelo!");


            if (ModelState.IsValid)
            {

                var Objeto = await _UOW.UF.AtualizarAsync(tabela);

                await _UOW.SaveAsync();
                return Ok(Objeto);

            }
            return BadRequest();

        }

        [HttpDelete("{Id}")]
        public async Task<ActionResult<int>> Delete(int Id)
        {

            await _UOW.UF.DeletarAsync(Id);

            int _removidos = await _UOW.SaveAsync();
            return Ok(_removidos);

        }


    }
}
