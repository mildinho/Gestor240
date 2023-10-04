using Dominio.Entidades;
using Dominio.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("v1/[controller]")]
    public class TipoServicoController : Controller
    {
        private readonly IUnitOfWork _UOW;
        public TipoServicoController(IUnitOfWork unitOfWork)
        {
            _UOW = unitOfWork;
        }

        [HttpGet("Codigo")]
        public async Task<ActionResult<TipoServico>> Codigo(string Codigo)
        {
            var Objeto = await _UOW.TipoServico.PesquisarPorCodigoAsync(Codigo);
            if (Objeto == null)
            {
                return NotFound("Registro Não Encontrado!");
            }
            return Ok(Objeto);
        }

        [HttpGet("Descricao")]
        public async Task<ActionResult<TipoServico>> Descricao(string Descricao)
        {
            var Objeto = await _UOW.TipoServico.PesquisarPorDescricaoAsync(Descricao);
            if (Objeto == null)
            {
                return NotFound("Registro Não Encontrado!");
            }
            return Ok(Objeto);
        }



        [HttpGet]
        [Route("GetAll")]
        public ActionResult<TipoServico> GetAll()
        {
            var Objeto = _UOW.TipoServico.ListarTodos();

            return Ok(Objeto);
        }




        [HttpPost]
        public async Task<ActionResult<TipoServico>> Post(TipoServico tabela)
        {
            IEnumerable<TipoServico> ObjetoLista = await _UOW.TipoServico.PesquisarPorCodigoAsync(tabela.Codigo);
            if (ObjetoLista.Any())
            {
                return BadRequest("O código deste Tipo de Operacao já existe cadastrado!");
            }

            if (ModelState.IsValid)
            {
                TipoServico Objeto = await _UOW.TipoServico.InserirAsync(tabela);

                await _UOW.SaveAsync();
                return Ok(Objeto);

            }
            return BadRequest();

        }

        [HttpPatch]
        public async Task<ActionResult<TipoServico>> Patch(int Id, TipoServico tabela)
        {
            if (Id != tabela.Id)
                return BadRequest(Mensagens.MSG_E001);


            TipoServico ObjetoPesquisa = await _UOW.TipoServico.PesquisarPorIdAsync(tabela.Id);
            if (ObjetoPesquisa == null)
            {
                return BadRequest(Mensagens.MSG_E002);
            }

            if (ModelState.IsValid)
            {

                var Objeto = await _UOW.TipoServico.AtualizarAsync(tabela);

                await _UOW.SaveAsync();
                return Ok(Objeto);

            }
            return BadRequest();

        }

        [HttpDelete("{Id}")]
        public async Task<ActionResult<int>> Delete(int Id)
        {

            await _UOW.TipoServico.DeletarAsync(Id);

            int _removidos = await _UOW.SaveAsync();
            return Ok(_removidos);

        }


    }
}
