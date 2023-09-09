using Dominio.Entidades;
using Dominio.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("v1/[controller]")]
    public class TipoPixoController : Controller
    {
        private readonly IUnitOfWork _UOW;
        public TipoPixoController(IUnitOfWork unitOfWork)
        {
            _UOW = unitOfWork;
        }

        [HttpGet("Codigo")]
        public async Task<ActionResult<TipoPix>> Codigo(string Codigo)
        {
            var Objeto = await _UOW.TipoPix.PesquisarPorCodigoAsync(Codigo);

            return Ok(Objeto);
        }

        [HttpGet("Descricao")]
        public async Task<ActionResult<TipoPix>> Descricao(string Descricao)
        {
            var Objeto = await _UOW.TipoPix.PesquisarPorDescricaoAsync(Descricao);

            return Ok(Objeto);
        }



        [HttpGet]
        [Route("GetAll")]
        public ActionResult<TipoPix> GetAll()
        {
            var Objeto = _UOW.TipoPix.ListarTodos();

            return Ok(Objeto);
        }




        [HttpPost]
        public async Task<ActionResult<TipoPix>> Post(TipoPix tabela)
        {
            IEnumerable<TipoPix> ObjetoLista = await _UOW.TipoPix.PesquisarPorCodigoAsync(tabela.Codigo);
            if (ObjetoLista.Any())
            {
                return BadRequest(Mensagens.MSG_E003);
            }

           if (ModelState.IsValid)
            {
                TipoPix Objeto = await _UOW.TipoPix.InserirAsync(tabela);

                await _UOW.SaveAsync();
                return Ok(Objeto);

            }
            return BadRequest();

        }

        [HttpPatch]
        public async Task<ActionResult<TipoPix>> Patch(int Id, TipoPix tabela)
        {
            if (Id != tabela.Id)
                return BadRequest(Mensagens.MSG_E001);


            TipoPix ObjetoPesquisa = await _UOW.TipoPix.PesquisarPorIdAsync(tabela.Id);
            if (ObjetoPesquisa == null)
            {
                return BadRequest(Mensagens.MSG_E002);
            }


            if (ModelState.IsValid)
            {

                var Objeto = await _UOW.TipoPix.AtualizarAsync(tabela);

                await _UOW.SaveAsync();
                return Ok(Objeto);

            }
            return BadRequest();

        }

        [HttpDelete("{Id}")]
        public async Task<ActionResult<int>> Delete(int Id)
        {

            await _UOW.TipoPix.DeletarAsync(Id);

            int _removidos = await _UOW.SaveAsync();
            return Ok(_removidos);

        }


    }
}
