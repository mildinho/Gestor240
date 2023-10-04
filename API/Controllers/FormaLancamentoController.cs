using Dominio.DTO;
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
        public async Task<ActionResult<FormaLancamentoDTO>> Codigo(string Codigo)
        {
            var Objeto = await _UOW.FormaLancamento.PesquisarPorCodigoAsync(Codigo); 
            if (Objeto == null)
            {
                return NotFound("Registro Não Encontrado!");
            }
            var ObjetoDTO = FormaLancamentoDTO.ToDTO(Objeto);

            return Ok(ObjetoDTO);
        }

        [HttpGet("Descricao")]
        public async Task<ActionResult<FormaLancamentoDTO>> Descricao(string Descricao)
        {
            var Objeto = await _UOW.FormaLancamento.PesquisarPorDescricaoAsync(Descricao);
            if (Objeto == null)
            {
                return NotFound("Registro Não Encontrado!");
            }
            var ObjetoDTO = FormaLancamentoDTO.ToDTO(Objeto);

            return Ok(ObjetoDTO);
        }



        [HttpGet]
        [Route("GetAll")]
        public ActionResult<FormaLancamentoDTO> GetAll()
        {
            var Objeto = _UOW.FormaLancamento.ListarTodos();
            var ObjetoDTO = FormaLancamentoDTO.ToDTO(Objeto);

            return Ok(ObjetoDTO);
        }




        [HttpPost]
        public async Task<ActionResult<FormaLancamentoDTO>> Post(FormaLancamentoDTO tabela)
        {
            IEnumerable<FormaLancamento> ObjetoLista = await _UOW.FormaLancamento.PesquisarPorCodigoAsync(tabela.Codigo);
            if (ObjetoLista.Any())
            {
                return BadRequest("O código deste Tipo de Operacao já existe cadastrado!");
            }

            if (ModelState.IsValid)
            {
                
                var ObjetoEntitade = FormaLancamentoDTO.ToEntidade(tabela);
                FormaLancamento Objeto = await _UOW.FormaLancamento.InserirAsync(ObjetoEntitade);

                var ObjetoDTO = FormaLancamentoDTO.ToDTO(Objeto);
                await _UOW.SaveAsync();
                return Ok(Objeto);

            }
            return BadRequest();

        }

        [HttpPatch]
        public async Task<ActionResult<FormaLancamentoDTO>> Patch(int Id, FormaLancamentoDTO tabela)
        {
            if (Id != tabela.Id)
                return BadRequest(Mensagens.MSG_E001);


            FormaLancamento ObjetoPesquisa = await _UOW.FormaLancamento.PesquisarPorIdAsync(tabela.Id);
            if (ObjetoPesquisa == null)
            {
                return BadRequest(Mensagens.MSG_E002);
            }

            if (ModelState.IsValid)
            {

                
                var ObjetoEntitade = FormaLancamentoDTO.ToEntidade(tabela);
                var Objeto = await _UOW.FormaLancamento.AtualizarAsync(ObjetoEntitade);

                var ObjetoDTO = FormaLancamentoDTO.ToDTO(Objeto);
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
