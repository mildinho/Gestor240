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

        [HttpGet("GetbyId/{Id}")]
        public async Task<ActionResult<UFDTO>> GetbyId(int Id)
        {
            var Objeto = await _UOW.UF.PesquisarPorIdAsync(Id);
            if (Objeto == null)
            {
                return NotFound(Mensagens.MSG_E002);
            }
            var ObjetoDTO = UFDTO.ToDTO(Objeto);

            return Ok(ObjetoDTO);
        }


        [HttpGet("Sigla")]
        public async Task<ActionResult<UFDTO>> Sigla(string Sigla)
        {
            var Objeto = await _UOW.UF.PesquisarPorSiglaAsync(Sigla);
            if (Objeto == null)
            {
                return NotFound(Mensagens.MSG_E002);
            }
            var ObjetoDTO = UFDTO.ToDTO(Objeto);

            return Ok(ObjetoDTO);
        }

        [HttpGet("Descricao")]
        public async Task<ActionResult<UFDTO>> Descricao(string Descricao)
        {
            var Objeto = await _UOW.UF.PesquisarPorDescricaoAsync(Descricao);
            if (Objeto == null)
            {
                return NotFound(Mensagens.MSG_E002);
            }
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
                return BadRequest(Mensagens.MSG_E003);
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

        [HttpPut("{Id}")]
        public async Task<ActionResult<UFDTO>> Put(int Id, UFDTO tabela)
        {
            if (Id != tabela.Id)
                return BadRequest(Mensagens.MSG_E001);

            UF ObjetoPesquisa = await _UOW.UF.PesquisarPorIdAsync(tabela.Id);
            if (ObjetoPesquisa == null)
            {
                return BadRequest(Mensagens.MSG_E002);
            }

            IEnumerable<UF> ObjetoLista = await _UOW.UF.PesquisarPorSiglaAsync(tabela.Sigla);
            if (ObjetoLista.Any() && ObjetoLista.FirstOrDefault().Id != Id)
            {
                return BadRequest(Mensagens.MSG_E003);
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

        [HttpDelete("{Id}")]
        public async Task<ActionResult<int>> Delete(int Id)
        {

            await _UOW.UF.DeletarAsync(Id);

            int _removidos = await _UOW.SaveAsync();
            return Ok(_removidos);

        }


    }
}
