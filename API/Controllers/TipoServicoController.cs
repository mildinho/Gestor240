using Dominio.DTO;
using Dominio.Entidades;
using Dominio.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace API.Controllers
{
    [ApiController]
    [Route("v1/[controller]")]
    public class TipoServicoController : Controller
    {
        private readonly IUnitOfWork _UOW;
        private readonly IMemoryCache _MemoryCache;
        const string _KeyCache = "GetAll_TipoServico";


        public TipoServicoController(IUnitOfWork unitOfWork, IMemoryCache memoryCache)
        {
            _UOW = unitOfWork;
            _MemoryCache = memoryCache;
        }

        [HttpGet("GetbyId/{Id}")]
        public async Task<ActionResult<TipoServico>> GetbyId(int Id)
        {
            var Objeto = await _UOW.TipoServico.PesquisarPorIdAsync(Id);
            if (Objeto == null)
            {
                return NotFound(Mensagens.MSG_E002);
            }
            return Ok(Objeto);
        }


        [HttpGet("Codigo")]
        public async Task<ActionResult<TipoServico>> Codigo(string Codigo)
        {
            var Objeto = await _UOW.TipoServico.PesquisarPorCodigoAsync(Codigo);
            if (Objeto == null)
            {
                return NotFound(Mensagens.MSG_E002);
            }
            return Ok(Objeto);
        }

        [HttpGet("Descricao")]
        public async Task<ActionResult<TipoServico>> Descricao(string Descricao)
        {
            var Objeto = await _UOW.TipoServico.PesquisarPorDescricaoAsync(Descricao);
            if (Objeto == null)
            {
                return NotFound(Mensagens.MSG_E002);
            }
            return Ok(Objeto);
        }



        [HttpGet]
        [Route("GetAll")]
        public ActionResult<TipoServico> GetAll()
        {
            IEnumerable<TipoServicoDTO> cacheValue = null;
            if (!_MemoryCache.TryGetValue(_KeyCache, out cacheValue))
            {
                var Objeto = _UOW.TipoServico.ListarTodos();

                cacheValue = TipoServicoDTO.ToDTO(Objeto);

                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromDays(30));

                _MemoryCache.Set(_KeyCache, cacheValue, cacheEntryOptions);
            }

            return Ok(cacheValue);

        }




        [HttpPost]
        public async Task<ActionResult<TipoServico>> Post(TipoServico tabela)
        {
            IEnumerable<TipoServico> ObjetoLista = await _UOW.TipoServico.PesquisarPorCodigoAsync(tabela.Codigo);
            if (ObjetoLista.Any())
            {
                return BadRequest("O código deste Tipo de Operacao já existe cadastrado!");
            }

            if (ModelState.IsValid)
            {
                TipoServico Objeto = await _UOW.TipoServico.InserirAsync(tabela);

                await _UOW.SaveAsync();
                return Ok(Objeto);

            }
            return BadRequest();

        }

        [HttpPut("{Id}")]
        public async Task<ActionResult<TipoServico>> Put(int Id, TipoServico tabela)
        {
            if (Id != tabela.Id)
                return BadRequest(Mensagens.MSG_E001);


            TipoServico ObjetoPesquisa = await _UOW.TipoServico.PesquisarPorIdAsync(tabela.Id);
            if (ObjetoPesquisa == null)
            {
                return BadRequest(Mensagens.MSG_E002);
            }

            if (ModelState.IsValid)
            {

                var Objeto = await _UOW.TipoServico.AtualizarAsync(tabela);

                await _UOW.SaveAsync();
                return Ok(Objeto);

            }
            return BadRequest();

        }

        [HttpDelete("{Id}")]
        public async Task<ActionResult<int>> Delete(int Id)
        {

            await _UOW.TipoServico.DeletarAsync(Id);

            int _removidos = await _UOW.SaveAsync();
            return Ok(_removidos);

        }


    }
}
