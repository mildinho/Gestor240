using Dominio.Entidades;
using Dominio.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("v1/[controller]")]
    public class TipoInscricaoEmpresaController : Controller
    {
        private readonly IUnitOfWork _UOW;
        public TipoInscricaoEmpresaController(IUnitOfWork unitOfWork)
        {
            _UOW = unitOfWork;
        }

        [HttpGet("Codigo")]
        public async Task<ActionResult<TipoInscricaoEmpresa>> Codigo(int Codigo)
        {
            var Objeto = await _UOW.TipoInscricaoEmpresa.PesquisarPorCodigoAsync(Codigo);

            return Ok(Objeto);
        }

        [HttpGet("Descricao")]
        public async Task<ActionResult<TipoInscricaoEmpresa>> Descricao(string Descricao)
        {
            var Objeto = await _UOW.TipoInscricaoEmpresa.PesquisarPorDescricaoAsync(Descricao);

            return Ok(Objeto);
        }



        [HttpGet]
        [Route("GetAll")]
        public ActionResult<TipoInscricaoEmpresa> GetAll()
        {
            var Objeto = _UOW.TipoInscricaoEmpresa.ListarTodos();

            return Ok(Objeto);
        }




        [HttpPost]
        public async Task<ActionResult<TipoInscricaoEmpresa>> Post(TipoInscricaoEmpresa tabela)
        {
            IEnumerable<TipoInscricaoEmpresa> ObjetoLista = await _UOW.TipoInscricaoEmpresa.PesquisarPorCodigoAsync(tabela.Codigo);
            if (ObjetoLista.Any())
            {
                return BadRequest("O código deste Tipo de Operacao já existe cadastrado!");
            }

            if (ModelState.IsValid)
            {
                TipoInscricaoEmpresa Objeto = await _UOW.TipoInscricaoEmpresa.InserirAsync(tabela);

                await _UOW.SaveAsync();
                return Ok(Objeto);

            }
            return BadRequest();

        }

        [HttpPatch]
        public async Task<ActionResult<TipoInscricaoEmpresa>> Patch(int Id, TipoInscricaoEmpresa tabela)
        {
            if (Id != tabela.Id)
                return BadRequest("O para ID está diferente do ID do Modelo!");


            if (ModelState.IsValid)
            {

                var Objeto = await _UOW.TipoInscricaoEmpresa.AtualizarAsync(tabela);

                await _UOW.SaveAsync();
                return Ok(Objeto);

            }
            return BadRequest();

        }

        [HttpDelete("{Id}")]
        public async Task<ActionResult<int>> Delete(int Id)
        {

            await _UOW.TipoInscricaoEmpresa.DeletarAsync(Id);

            int _removidos = await _UOW.SaveAsync();
            return Ok(_removidos);

        }


    }
}
