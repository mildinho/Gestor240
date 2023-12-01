using Dominio.DTO;
using Dominio.Entidades;
using Dominio.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace API.Controllers
{
    [ApiController]
    [Route("v1/[controller]")]
    public class BancoController : Controller
    {
        private readonly IUnitOfWork _UOW;
        private readonly IMemoryCache _MemoryCache;
        const string _KeyCache = "GetAll_Banco";


        public BancoController(IUnitOfWork unitOfWork, IMemoryCache memoryCache)
        {
            _UOW = unitOfWork;
            _MemoryCache = memoryCache;
        }


        [HttpGet("GetbyId/{Id}")]
        public async Task<ActionResult<BancoDTO>> GetbyId(int Id)
        {
            var Objeto = await _UOW.Banco.PesquisarPorIdAsync(Id);
            if (Objeto == null)
            {
                return NotFound(Mensagens.MSG_E002);
            }

            var ObjetoDTO = BancoDTO.ToDTO(Objeto);

            return Ok(ObjetoDTO);

        }


        [HttpGet("Codigo")]
        public async Task<ActionResult<BancoDTO>> Get(int Codigo)
        {
            var Objeto = await _UOW.Banco.PesquisarPorCodigoAsync(Codigo);

            if (Objeto == null)
            {
                return NotFound(Mensagens.MSG_E002);
            }

            var ObjetoDTO = BancoDTO.ToDTO(Objeto);

            return Ok(ObjetoDTO);
        }

        [HttpGet]
        [Route("GetAll")]
        public ActionResult<BancoDTO> GetAll()
        {

            IEnumerable<BancoDTO> cacheValue = null;
            if (!_MemoryCache.TryGetValue(_KeyCache, out cacheValue))
            {
                var Objeto = _UOW.Banco.ListarTodos();

                cacheValue = BancoDTO.ToDTO(Objeto);

                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromDays(30));

                _MemoryCache.Set(_KeyCache, cacheValue, cacheEntryOptions);
            }

            return Ok(cacheValue);

        }



        [HttpPost]
        public async Task<ActionResult<BancoDTO>> Post(BancoDTO tabela)
        {
            IEnumerable<Banco> ObjetoLista = await _UOW.Banco.PesquisarPorCodigoAsync(tabela.Codigo);
            if (ObjetoLista.Any())
            {
                return BadRequest(Mensagens.MSG_E003);
            }

            if (ModelState.IsValid)
            {
                var ObjetoEntitade = BancoDTO.ToEntidade(tabela);
                Banco Objeto = await _UOW.Banco.InserirAsync(ObjetoEntitade);

                var ObjetoDTO = BancoDTO.ToDTO(Objeto);

                await _UOW.SaveAsync();

                _MemoryCache.Remove(_KeyCache);

                return Ok(ObjetoDTO);

            }
            return BadRequest();

        }

        [HttpPut("{Id}")]
        public async Task<ActionResult<BancoDTO>> Put(int Id, BancoDTO tabela)
        {
            if (Id != tabela.Id)
                return BadRequest(Mensagens.MSG_E001);


            Banco ObjetoPesquisa = await _UOW.Banco.PesquisarPorIdAsync(tabela.Id);
            if (ObjetoPesquisa == null)
            {
                return BadRequest(Mensagens.MSG_E002);
            }

            IEnumerable<Banco> ObjetoLista = await _UOW.Banco.PesquisarPorCodigoAsync(tabela.Codigo);
            if (ObjetoLista.Any() && ObjetoLista.FirstOrDefault().Id != Id)
            {
                return BadRequest(Mensagens.MSG_E003);
            }


            if (ModelState.IsValid)
            {

                var ObjetoEntitade = BancoDTO.ToEntidade(tabela);
                var Objeto = await _UOW.Banco.AtualizarAsync(ObjetoEntitade);

                var ObjetoDTO = BancoDTO.ToDTO(Objeto);

                await _UOW.SaveAsync();

                _MemoryCache.Remove(_KeyCache);

                return Ok(ObjetoDTO);

            }
            return BadRequest();

        }

        [HttpDelete("{Id}")]
        public async Task<ActionResult<int>> Delete(int Id)
        {

            await _UOW.Banco.DeletarAsync(Id);

            int _removidos = await _UOW.SaveAsync();

            _MemoryCache.Remove(_KeyCache);


            return Ok(_removidos);

        }




    }
}
