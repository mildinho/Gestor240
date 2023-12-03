using Dominio.DTO;
using Dominio.Entidades;
using Dominio.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace API.Controllers
{
    [ApiController]
    [Route("v1/[controller]")]
    public class FormaLancamentoController : Controller
    {
        private readonly IUnitOfWork _UOW;
        private readonly IMemoryCache _MemoryCache;
        const string _KeyCache = "GetAll_FormaLancamento";

        public FormaLancamentoController(IUnitOfWork unitOfWork, IMemoryCache memoryCache)
        {
            _UOW = unitOfWork;
            _MemoryCache = memoryCache;
        }

        [HttpGet("GetbyId/{Id}")]
        public async Task<ActionResult<FormaLancamentoDTO>> GetbyId(int Id)
        {
            var Objeto = await _UOW.FormaLancamento.PesquisarPorIdAsync(Id);
            if (Objeto == null)
            {
                return NotFound(Mensagens.MSG_E002);
            }
            var ObjetoDTO = FormaLancamentoDTO.ToDTO(Objeto);

            return Ok(ObjetoDTO);

        }


        [HttpGet("Codigo")]
        public async Task<ActionResult<FormaLancamentoDTO>> Codigo(string Codigo)
        {
            var Objeto = await _UOW.FormaLancamento.PesquisarPorCodigoAsync(Codigo);
            if (Objeto == null)
            {
                return NotFound(Mensagens.MSG_E002);
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
                return NotFound(Mensagens.MSG_E002);
            }
            var ObjetoDTO = FormaLancamentoDTO.ToDTO(Objeto);

            return Ok(ObjetoDTO);
        }



        [HttpGet]
        [Route("GetAll")]
        public ActionResult<FormaLancamentoDTO> GetAll()
        {
            IEnumerable<FormaLancamentoDTO> cacheValue = null;
            if (!_MemoryCache.TryGetValue(_KeyCache, out cacheValue))
            {
                var Objeto = _UOW.FormaLancamento.ListarTodos();

                cacheValue = FormaLancamentoDTO.ToDTO(Objeto);

                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromDays(30));

                _MemoryCache.Set(_KeyCache, cacheValue, cacheEntryOptions);
            }

            return Ok(cacheValue);

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

        [HttpPut("{Id}")]
        public async Task<ActionResult<FormaLancamentoDTO>> Put(int Id, FormaLancamentoDTO tabela)
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
