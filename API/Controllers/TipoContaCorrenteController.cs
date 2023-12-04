using Dominio.DTO;
using Dominio.Entidades;
using Dominio.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace API.Controllers
{
    [ApiController]
    [Route("v1/[controller]")]
    public class TipoContaCorrenteController : Controller
    {
        private readonly IUnitOfWork _UOW;
        private readonly IMemoryCache _MemoryCache;
        const string _KeyCache = "GetAll_TipoContaCorrente";


        public TipoContaCorrenteController(IUnitOfWork unitOfWork, IMemoryCache memoryCache)
        {
            _UOW = unitOfWork;
            _MemoryCache = memoryCache;
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
            var Objeto = await _UOW.TipoContaCorrente.PesquisarPorDescricaoAsync(Descricao, false);
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
            IEnumerable<TipoContaCorrenteDTO> cacheValue = null;
            if (!_MemoryCache.TryGetValue(_KeyCache, out cacheValue))
            {
                var Objeto = _UOW.TipoContaCorrente.ListarTodos();

                cacheValue = TipoContaCorrenteDTO.ToDTO(Objeto);

                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromDays(30));

                _MemoryCache.Set(_KeyCache, cacheValue, cacheEntryOptions);
            }

            return Ok(cacheValue);
        }




        [HttpPost]
        public async Task<ActionResult<TipoContaCorrenteDTO>> Post(TipoContaCorrenteDTO tabela)
        {
            IEnumerable<TipoContaCorrente> ObjetoLista = await _UOW.TipoContaCorrente.PesquisarPorDescricaoAsync(tabela.Descricao, true);
            if (ObjetoLista.Any())
            {
                return BadRequest(Mensagens.MSG_E003);
            }

            if (ModelState.IsValid)
            {
                var ObjetoEntitade = TipoContaCorrenteDTO.ToEntidade(tabela);
                TipoContaCorrente Objeto = await _UOW.TipoContaCorrente.InserirAsync(ObjetoEntitade);

                var ObjetoDTO = TipoContaCorrenteDTO.ToDTO(Objeto);
                await _UOW.SaveAsync();

                _MemoryCache.Remove(_KeyCache);

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


            IEnumerable<TipoContaCorrente> ObjetoLista = await _UOW.TipoContaCorrente.PesquisarPorDescricaoAsync(tabela.Descricao, true);
            if (ObjetoLista.Any() && ObjetoLista.FirstOrDefault().Id != Id)
            {
                return BadRequest(Mensagens.MSG_E003);
            }
            if (ModelState.IsValid)
            {

                var ObjetoEntitade = TipoContaCorrenteDTO.ToEntidade(tabela);
                TipoContaCorrente Objeto = await _UOW.TipoContaCorrente.AtualizarAsync(ObjetoEntitade);

                var ObjetoDTO = TipoContaCorrenteDTO.ToDTO(Objeto);
                await _UOW.SaveAsync();

                _MemoryCache.Remove(_KeyCache);

                return Ok(ObjetoDTO);

            }
            return BadRequest();

        }

        [HttpDelete("{Id}")]
        public async Task<ActionResult<int>> Delete(int Id)
        {
            await _UOW.TipoContaCorrente.DeletarAsync(Id);

            int _removidos = await _UOW.SaveAsync();

            _MemoryCache.Remove(_KeyCache);

            return Ok(_removidos);
        }


    }
}
