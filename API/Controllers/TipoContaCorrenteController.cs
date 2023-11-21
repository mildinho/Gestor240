using Dominio.DTO;
using Dominio.Entidades;
using Dominio.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("v1/[controller]")]
    public class TipoContaCorrenteController : Controller
    {
        private readonly IUnitOfWork _UOW;
        public TipoContaCorrenteController(IUnitOfWork unitOfWork)
        {
            _UOW = unitOfWork;
        }

        [HttpGet("GetbyId/{Id}")]
        public async Task<ActionResult<TipoContaCorrenteDTO>> GetbyId(int Id)
        {
            var Objeto = await _UOW.TipoContaCorrente.PesquisarPorIdAsync(Id);
            if (Objeto == null)
            {
                return NotFound(Mensagens.MSG_E002);
            }
            var ObjetoDTO = TipoContaCorrenteDTO.ToDTO(Objeto);

            return Ok(ObjetoDTO);
        }


        [HttpGet("Descricao")]
        public async Task<ActionResult<TipoContaCorrenteDTO>> Descricao(string Descricao)
        {
            var Objeto = await _UOW.TipoContaCorrente.PesquisarPorDescricaoAsync(Descricao);
            if (Objeto == null)
            {
                return NotFound(Mensagens.MSG_E002);
            }
            var ObjetoDTO = TipoContaCorrenteDTO.ToDTO(Objeto);

            return Ok(ObjetoDTO);
        }



        [HttpGet]
        [Route("GetAll")]
        public ActionResult<TipoContaCorrenteDTO> GetAll()
        {
            var Objeto = _UOW.TipoContaCorrente.ListarTodos();
            var ObjetoDTO = TipoContaCorrenteDTO.ToDTO(Objeto);

            return Ok(ObjetoDTO);
        }




        [HttpPost]
        public async Task<ActionResult<TipoPixDTO>> Post(TipoContaCorrenteDTO tabela)
        {
           if (ModelState.IsValid)
            {
               
                var ObjetoEntitade = TipoContaCorrenteDTO.ToEntidade(tabela);
                TipoContaCorrente Objeto = await _UOW.TipoContaCorrente.InserirAsync(ObjetoEntitade);

                var ObjetoDTO = TipoContaCorrenteDTO.ToDTO(Objeto);
                await _UOW.SaveAsync();

                return Ok(ObjetoDTO);

            }
            return BadRequest();

        }

        [HttpPut("{Id}")]
        public async Task<ActionResult<TipoContaCorrenteDTO>> Put(int Id, TipoContaCorrenteDTO tabela)
        {
            if (Id != tabela.Id)
                return BadRequest(Mensagens.MSG_E001);


            TipoContaCorrente ObjetoPesquisa = await _UOW.TipoContaCorrente.PesquisarPorIdAsync(tabela.Id);
            if (ObjetoPesquisa == null)
            {
                return BadRequest(Mensagens.MSG_E002);
            }

            if (ModelState.IsValid)
            {

                var ObjetoEntitade = TipoContaCorrenteDTO.ToEntidade(tabela);
                TipoContaCorrente Objeto = await _UOW.TipoContaCorrente.AtualizarAsync(ObjetoEntitade);

                var ObjetoDTO = TipoContaCorrenteDTO.ToDTO(Objeto);
                await _UOW.SaveAsync();

                return Ok(ObjetoDTO);

            }
            return BadRequest();

        }

        [HttpDelete("{Id}")]
        public async Task<ActionResult<int>> Delete(int Id)
        {

            await _UOW.TipoContaCorrente.DeletarAsync(Id);

            int _removidos = await _UOW.SaveAsync();
            return Ok(_removidos);

        }


    }
}
