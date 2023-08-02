using Dominio.Entidades;
using Dominio.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("v1/[controller]")]
    public class TipoOperacaoController : Controller
    {
        private readonly IUnitOfWork _UOW;
        public TipoOperacaoController(IUnitOfWork unitOfWork)
        {
            _UOW = unitOfWork;
        }

        [HttpGet("Codigo")]
        public async Task<ActionResult<TipoOperacao>> Codigo(string Codigo)
        {
            var Objeto = await _UOW.TipoOperacao.PesquisarPorCodigoAsync(Codigo);

            return Ok(Objeto);
        }


        [HttpGet("Descricao")]
        public async Task<ActionResult<TipoOperacao>> Descricao(string Descricao)
        {
            var Objeto = await _UOW.TipoOperacao.PesquisarPorDescricaoAsync(Descricao);

            return Ok(Objeto);
        }



        [HttpGet]
        [Route("GetAll")]
        public ActionResult<TipoOperacao> GetAll()
        {
            var Objeto =  _UOW.TipoOperacao.ListarTodos();

            return Ok(Objeto);
        }




        [HttpPost]
        public async Task<ActionResult<TipoOperacao>> Post(TipoOperacao tabela)
        {
            IEnumerable<TipoOperacao> ObjetoLista = await _UOW.TipoOperacao.PesquisarPorCodigoAsync(tabela.Codigo);
            if (ObjetoLista.Any())
            {
                return BadRequest("O código deste Tipo de Operacao já existe cadastrado!");
            }

            if (ModelState.IsValid)
            {
                TipoOperacao Objeto = await _UOW.TipoOperacao.InserirAsync(tabela);

                await _UOW.SaveAsync();
                return Ok(Objeto);

            }
            return BadRequest();

        }

        [HttpPatch]
        public async Task<ActionResult<TipoOperacao>> Patch(int Id, TipoOperacao tabela)
        {
            if (Id != tabela.Id)
                return BadRequest("O para ID está diferente do ID do Modelo!");


            if (ModelState.IsValid)
            {

                var Objeto = await _UOW.TipoOperacao.AtualizarAsync(tabela);

                await _UOW.SaveAsync();
                return Ok(Objeto);

            }
            return BadRequest();

        }

        [HttpDelete("{Id}")]
        public async Task<ActionResult<int>> Delete(int Id)
        {

            await _UOW.TipoOperacao.DeletarAsync(Id);

            int _removidos = await _UOW.SaveAsync();
            return Ok(_removidos);

        }


    }
}
