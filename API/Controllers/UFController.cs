using Dominio.DTO;
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
        public async Task<ActionResult<UFDTO>> Sigla(string Sigla)
        {
            var Objeto = await _UOW.UF.PesquisarPorSiglaAsync(Sigla);
            var ObjetoDTO = UFDTO.ToDTO(Objeto);

            return Ok(ObjetoDTO);
        }

        [HttpGet("Descricao")]
        public async Task<ActionResult<UFDTO>> Descricao(string Descricao)
        {
            var Objeto = await _UOW.UF.PesquisarPorDescricaoAsync(Descricao);
            var ObjetoDTO = UFDTO.ToDTO(Objeto);

            return Ok(ObjetoDTO);
        }

        [HttpGet]
        [Route("GetAll")]
        public ActionResult<UFDTO> GetAll()
        {
            var Objeto = _UOW.UF.ListarTodos();
            var ObjetoDTO = UFDTO.ToDTO(Objeto);

            return Ok(ObjetoDTO);
        }




        [HttpPost]
        public async Task<ActionResult<UFDTO>> Post(UFDTO tabela)
        {
            IEnumerable<UF> ObjetoLista = await _UOW.UF.PesquisarPorSiglaAsync(tabela.Sigla);
            if (ObjetoLista.Any())
            {
                return BadRequest("O código deste UF já existe cadastrado!");
            }

            if (ModelState.IsValid)
            {
                var ObjetoEntitade = UFDTO.ToEntidade(tabela);
                UF Objeto = await _UOW.UF.InserirAsync(ObjetoEntitade);

                var ObjetoDTO = UFDTO.ToDTO(Objeto);

                await _UOW.SaveAsync();

                return Ok(ObjetoDTO);

            }
            return BadRequest();

        }

        [HttpPatch]
        public async Task<ActionResult<UFDTO>> Patch(int Id, UFDTO tabela)
        {
            if (Id != tabela.Id)
                return BadRequest("O para ID está diferente do ID do Modelo!");


            if (ModelState.IsValid)
            {

                var ObjetoEntitade = UFDTO.ToEntidade(tabela);
                UF Objeto = await _UOW.UF.InserirAsync(ObjetoEntitade);

                var ObjetoDTO = UFDTO.ToDTO(Objeto);

                await _UOW.SaveAsync();

                return Ok(ObjetoDTO);

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
