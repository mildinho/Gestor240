using Dominio.DTO;
using Dominio.Entidades;
using Dominio.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace API.Controllers
{
    [ApiController]
    [Route("v1/[controller]")]
    [Authorize]
    public class TipoPixController : Controller
    {
        private readonly IUnitOfWork _UOW;
        private readonly IMemoryCache _MemoryCache;
        const string _KeyCache = "GetAll_TipoPix";

        public TipoPixController(IUnitOfWork unitOfWork, IMemoryCache memoryCache)
        {
            _UOW = unitOfWork;
            _MemoryCache = memoryCache;
        }

        [HttpGet("GetbyId/{Id}")]
        public async Task<ActionResult<TipoPixDTO>> GetbyId(int Id)
        {
            var Objeto = await _UOW.TipoPix.PesquisarPorIdAsync(Id);
            if (Objeto == null)
            {
                return NotFound(Mensagens.MSG_E002);
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
                return NotFound(Mensagens.MSG_E002);
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
                return NotFound(Mensagens.MSG_E002);
            }
            var ObjetoDTO = TipoPixDTO.ToDTO(Objeto);

            return Ok(ObjetoDTO);
        }



        [HttpGet]
        [Route("GetAll")]
        public ActionResult<TipoPix> GetAll()
        {

            IEnumerable<TipoPixDTO> cacheValue = null;
            if (!_MemoryCache.TryGetValue(_KeyCache, out cacheValue))
            {
                var Objeto = _UOW.TipoPix.ListarTodos();

                cacheValue = TipoPixDTO.ToDTO(Objeto);

                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromDays(30));

                _MemoryCache.Set(_KeyCache, cacheValue, cacheEntryOptions);
            }

            return Ok(cacheValue);

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

                _MemoryCache.Remove(_KeyCache);

                return Ok(ObjetoDTO);

            }
            return BadRequest();

        }

        [HttpPut("{Id}")]
        public async Task<ActionResult<TipoPixDTO>> Put(int Id, TipoPixDTO tabela)
        {
            if (Id != tabela.Id)
                return BadRequest(Mensagens.MSG_E001);


            TipoPix ObjetoPesquisa = await _UOW.TipoPix.PesquisarPorIdAsync(tabela.Id);
            if (ObjetoPesquisa == null)
            {
                return BadRequest(Mensagens.MSG_E002);
            }
            IEnumerable<TipoPix> ObjetoLista = await _UOW.TipoPix.PesquisarPorCodigoAsync(tabela.Codigo);

            if (ObjetoLista.Any() && ObjetoLista.FirstOrDefault().Id != Id)
            {
                return BadRequest(Mensagens.MSG_E003);
            }

            if (ModelState.IsValid)
            {

                var ObjetoEntitade = TipoPixDTO.ToEntidade(tabela);
                TipoPix Objeto = await _UOW.TipoPix.AtualizarAsync(ObjetoEntitade);

                var ObjetoDTO = TipoPixDTO.ToDTO(Objeto);
                await _UOW.SaveAsync();

                _MemoryCache.Remove(_KeyCache);

                return Ok(ObjetoDTO);

            }
            return BadRequest();

        }

        [HttpDelete("{Id}")]
        public async Task<ActionResult<int>> Delete(int Id)
        {
            await _UOW.TipoPix.DeletarAsync(Id);

            int _removidos = await _UOW.SaveAsync();

            _MemoryCache.Remove(_KeyCache);

            return Ok(_removidos);
        }


    }
}
