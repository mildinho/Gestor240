using Dominio.DTO;
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

        [HttpGet("GetbyId/{Id}")]
        public async Task<ActionResult<TipoPixDTO>> GetbyId(int Id)
        {
            var Objeto = await _UOW.TipoPix.PesquisarPorIdAsync(Id);
            if (Objeto == null)
            {
                return NotFound("Registro Não Encontrado!");
            }
            var ObjetoDTO = TipoPixDTO.ToDTO(Objeto);

            return Ok(ObjetoDTO);
        }


        [HttpGet("Codigo")]
        public async Task<ActionResult<TipoPixDTO>> Codigo(string Codigo)
        {
            var Objeto = await _UOW.TipoPix.PesquisarPorCodigoAsync(Codigo);
            if (Objeto == null)
            {
                return NotFound("Registro Não Encontrado!");
            }
            var ObjetoDTO = TipoPixDTO.ToDTO(Objeto);

            return Ok(ObjetoDTO);
        }

        [HttpGet("Descricao")]
        public async Task<ActionResult<TipoPix>> Descricao(string Descricao)
        {
            var Objeto = await _UOW.TipoPix.PesquisarPorDescricaoAsync(Descricao);
            if (Objeto == null)
            {
                return NotFound("Registro Não Encontrado!");
            }
            var ObjetoDTO = TipoPixDTO.ToDTO(Objeto);

            return Ok(ObjetoDTO);
        }



        [HttpGet]
        [Route("GetAll")]
        public ActionResult<TipoPix> GetAll()
        {
            var Objeto = _UOW.TipoPix.ListarTodos();
            var ObjetoDTO = TipoPixDTO.ToDTO(Objeto);

            return Ok(ObjetoDTO);
        }




        [HttpPost]
        public async Task<ActionResult<TipoPixDTO>> Post(TipoPixDTO tabela)
        {
            IEnumerable<TipoPix> ObjetoLista = await _UOW.TipoPix.PesquisarPorCodigoAsync(tabela.Codigo);
            if (ObjetoLista.Any())
            {
                return BadRequest(Mensagens.MSG_E003);
            }

           if (ModelState.IsValid)
            {
               
                var ObjetoEntitade = TipoPixDTO.ToEntidade(tabela);
                TipoPix Objeto = await _UOW.TipoPix.InserirAsync(ObjetoEntitade);

                var ObjetoDTO = TipoPixDTO.ToDTO(Objeto);
                await _UOW.SaveAsync();

                return Ok(ObjetoDTO);

            }
            return BadRequest();

        }

        [HttpPatch]
        public async Task<ActionResult<TipoPixDTO>> Patch(int Id, TipoPixDTO tabela)
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

                var ObjetoEntitade = TipoPixDTO.ToEntidade(tabela);
                TipoPix Objeto = await _UOW.TipoPix.InserirAsync(ObjetoEntitade);

                var ObjetoDTO = TipoPixDTO.ToDTO(Objeto);
                await _UOW.SaveAsync();

                return Ok(ObjetoDTO);

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
