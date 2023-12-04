using Dominio.DTO;
using Dominio.Entidades;
using Dominio.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace API.Controllers
{
    [ApiController]
    [Route("v1/[controller]")]
    public class MunicipioController : Controller
    {
        private readonly IUnitOfWork _UOW;
        private readonly IMemoryCache _MemoryCache;
        const string _KeyCache = "GetAll_Municipio";

        public MunicipioController(IUnitOfWork unitOfWork, IMemoryCache memoryCache)
        {
            _UOW = unitOfWork;
            _MemoryCache = memoryCache;
        }


        [HttpGet("GetbyId/{Id}")]
        public async Task<ActionResult<MunicipioDTO>> GetbyId(int Id)
        {
            var Objeto = await _UOW.Municipio.PesquisarPorIdAgregadoAsync(Id);
            if (Objeto == null)
            {
                return NotFound(Mensagens.MSG_E002);
            }
            var ObjetoDTO = MunicipioDTO.ToDTO(Objeto);

            return Ok(ObjetoDTO);
        }

        [HttpGet("GetbyIdUF/{Id}")]
        public async Task<ActionResult<MunicipioDTO>> GetbyIdUF(int Id)
        {
            var Objeto = await _UOW.Municipio.PesquisarPorUFAgregadoAsync(Id);
            if (Objeto == null)
            {
                return NotFound(Mensagens.MSG_E002);
            }
            var ObjetoDTO = MunicipioDTO.ToDTO(Objeto);

            return Ok(ObjetoDTO);
        }


        [HttpGet("Descricao")]
        public async Task<ActionResult<MunicipioDTO>> Descricao(string Descricao)
        {
            var Objeto = await _UOW.Municipio.PesquisarPorMunicipioAsync(Descricao);
            if (Objeto == null)
            {
                return NotFound(Mensagens.MSG_E002);
            }
            var ObjetoDTO = MunicipioDTO.ToDTO(Objeto);

            return Ok(ObjetoDTO);
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<ActionResult<MunicipioDTO>> GetAll()
        {
            IEnumerable<MunicipioDTO> cacheValue = null;
            if (!_MemoryCache.TryGetValue(_KeyCache, out cacheValue))
            {
                var Objeto = await _UOW.Municipio.ListarTodosAgregados();

                cacheValue = MunicipioDTO.ToDTO(Objeto);

                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromDays(30));

                _MemoryCache.Set(_KeyCache, cacheValue, cacheEntryOptions);
            }

            return Ok(cacheValue);

        }




        [HttpPost]
        public async Task<ActionResult<MunicipioDTO>> Post(MunicipioDTO tabela)
        {
            IEnumerable<Municipio> ObjetoLista = await _UOW.Municipio.PesquisarPorUFMunicipioAgregadoAsync(tabela.UFId,tabela.Nome);
            if (ObjetoLista.Any())
            {
                return BadRequest(Mensagens.MSG_E003);
            }

            if (ModelState.IsValid)
            {
                var ObjetoEntitade = MunicipioDTO.ToEntidade(tabela);
                Municipio Objeto = await _UOW.Municipio.InserirAsync(ObjetoEntitade);
              
                await _UOW.SaveAsync();

                Objeto = await _UOW.Municipio.PesquisarPorIdAgregadoAsync(Objeto.Id);
                var ObjetoDTO = MunicipioDTO.ToDTO(Objeto);

                _MemoryCache.Remove(_KeyCache);

                return Ok(ObjetoDTO);

            }
            return BadRequest();

        }

        [HttpPut("{Id}")]
        public async Task<ActionResult<MunicipioDTO>> Put(int Id, MunicipioDTO tabela)
        {
            if (Id != tabela.Id)
                return BadRequest(Mensagens.MSG_E001);

            Municipio ObjetoPesquisa = await _UOW.Municipio.PesquisarPorIdAgregadoAsync(tabela.Id);
            if (ObjetoPesquisa == null)
            {
                return BadRequest(Mensagens.MSG_E002);
            }

            IEnumerable<Municipio> ObjetoLista = await _UOW.Municipio.PesquisarPorUFMunicipioAgregadoAsync(tabela.UFId, tabela.Nome);
            if (ObjetoLista.Any() && ObjetoLista.FirstOrDefault().Id != Id)
            {
                return BadRequest(Mensagens.MSG_E003);
            }
            if (ModelState.IsValid)
            {

                var ObjetoEntitade = MunicipioDTO.ToEntidade(tabela);
                Municipio Objeto = await _UOW.Municipio.AtualizarAsync(ObjetoEntitade);
                await _UOW.SaveAsync();


                Objeto = await _UOW.Municipio.PesquisarPorIdAgregadoAsync(tabela.Id);

                var ObjetoDTO = MunicipioDTO.ToDTO(Objeto);

                _MemoryCache.Remove(_KeyCache);

                return Ok(ObjetoDTO);
            }
            return BadRequest();

        }

        [HttpDelete("{Id}")]
        public async Task<ActionResult<int>> Delete(int Id)
        {
            await _UOW.Municipio.DeletarAsync(Id);

            int _removidos = await _UOW.SaveAsync();

            _MemoryCache.Remove(_KeyCache);

            return Ok(_removidos);
        }


    }
}
