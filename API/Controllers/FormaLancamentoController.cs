using Dominio.Entidades;
using Dominio.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("v1/[controller]")]
    public class FormaLancamentoController : Controller
    {
        private readonly IUnitOfWork _UOW;
        public FormaLancamentoController(IUnitOfWork unitOfWork)
        {
            _UOW = unitOfWork;
        }

        [HttpGet("Codigo")]
        public async Task<ActionResult<FormaLancamento>> Codigo(string Codigo)
        {
            var Objeto = await _UOW.FormaLancamento.PesquisarPorCodigoAsync(Codigo);

            return Ok(Objeto);
        }

        [HttpGet("Descricao")]
        public async Task<ActionResult<FormaLancamento>> Descricao(string Descricao)
        {
            var Objeto = await _UOW.FormaLancamento.PesquisarPorDescricaoAsync(Descricao);

            return Ok(Objeto);
        }



        [HttpGet]
        [Route("GetAll")]
        public ActionResult<FormaLancamento> GetAll()
        {
            var Objeto = _UOW.FormaLancamento.ListarTodos();

            return Ok(Objeto);
        }




        [HttpPost]
        public async Task<ActionResult<FormaLancamento>> Post(FormaLancamento tabela)
        {
            IEnumerable<FormaLancamento> TipoServicoLista = await _UOW.FormaLancamento.PesquisarPorCodigoAsync(tabela.Codigo);
            if (TipoServicoLista.Any())
            {
                return BadRequest("O código deste Tipo de Operacao já existe cadastrado!");
            }

            if (ModelState.IsValid)
            {
                FormaLancamento Objeto = await _UOW.FormaLancamento.InserirAsync(tabela);

                await _UOW.SaveAsync();
                return Ok(Objeto);

            }
            return BadRequest();

        }

        [HttpPatch]
        public async Task<ActionResult<FormaLancamento>> Patch(int Id, FormaLancamento tabela)
        {
            if (Id != tabela.Id)
                return BadRequest("O para ID está diferente do ID do Modelo!");


            if (ModelState.IsValid)
            {

                var Objeto = await _UOW.FormaLancamento.AtualizarAsync(tabela);

                await _UOW.SaveAsync();
                return Ok(Objeto);

            }
            return BadRequest();

        }

        [HttpDelete("{Id}")]
        public async Task<ActionResult<int>> Delete(int Id)
        {

            await _UOW.FormaLancamento.DeletarAsync(Id);

            int _removidos = await _UOW.SaveAsync();
            return Ok(_removidos);

        }


    }
}
