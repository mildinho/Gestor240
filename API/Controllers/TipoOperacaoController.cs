using Dominio.DTO;
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
        public async Task<ActionResult<TipoOperacaoDTO>> Codigo(string Codigo)
        {
            var Objeto = await _UOW.TipoOperacao.PesquisarPorCodigoAsync(Codigo);
            var ObjetoDTO = TipoOperacaoDTO.ToDTO(Objeto);

            return Ok(ObjetoDTO);
        }


        [HttpGet("Descricao")]
        public async Task<ActionResult<TipoOperacaoDTO>> Descricao(string Descricao)
        {
            var Objeto = await _UOW.TipoOperacao.PesquisarPorDescricaoAsync(Descricao);
            var ObjetoDTO = TipoOperacaoDTO.ToDTO(Objeto);
            return Ok(ObjetoDTO);

        }



        [HttpGet]
        [Route("GetAll")]
        public ActionResult<TipoOperacao> GetAll()
        {
            var Objeto =  _UOW.TipoOperacao.ListarTodos();
            var ObjetoDTO = TipoOperacaoDTO.ToDTO(Objeto);
            return Ok(ObjetoDTO);
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
                return BadRequest(Mensagens.MSG_E001);


            TipoOperacao ObjetoPesquisa = await _UOW.TipoOperacao.PesquisarPorIdAsync(tabela.Id);
            if (ObjetoPesquisa == null)
            {
                return BadRequest(Mensagens.MSG_E002);
            }

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
