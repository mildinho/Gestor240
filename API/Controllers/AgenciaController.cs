using Dominio.DTO;
using Dominio.Entidades;
using Dominio.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("v1/[controller]")]
    [Authorize]
    public class AgenciaController : Controller
    {
        private readonly IUnitOfWork _UOW;
        public AgenciaController(IUnitOfWork unitOfWork)
        {
            _UOW = unitOfWork;
        }

        [HttpGet("GetbyId/{Id}")]
        public async Task<ActionResult<AgenciaDTO>> GetbyId(int Id)
        {
            var Objeto = await _UOW.Agencia.PesquisarPorIdAsync(Id);
            if (Objeto == null)
            {
                return NotFound(Mensagens.MSG_E002);
            }

            var ObjetoDTO = AgenciaDTO.ToDTO(Objeto);

            return Ok(ObjetoDTO);

        }


        [HttpGet("Codigo")]
        public async Task<ActionResult<AgenciaDTO>> Get(int IdBanco, int Agencia)
        {
            var Objeto = await _UOW.Agencia.PesquisarPorBancoAgenciaAgregadoAsync(IdBanco, Agencia);
            if (Objeto == null)
            {
                return NotFound(Mensagens.MSG_E002);
            }

            var ObjetoDTO = AgenciaDTO.ToDTO(Objeto);
            return Ok(ObjetoDTO);
        }


        [HttpGet("GetByIdBanco/{IdBanco}")]
        public async Task<ActionResult<AgenciaDTO>> GetByIdBanco(int IdBanco)
        {
            var Objeto = await _UOW.Agencia.PesquisarPorBancoAgregadoAsync(IdBanco);
            if (Objeto == null)
            {
                return NotFound(Mensagens.MSG_E002);
            }

            var ObjetoDTO = AgenciaDTO.ToDTO(Objeto);
            return Ok(ObjetoDTO);
        }


        [HttpGet]
        [Route("GetAll")]
        public async Task<ActionResult<AgenciaDTO>> GetAll()
        {
            var Objeto = await _UOW.Agencia.ListarTodosAgregados();
            var ObjetoDTO = AgenciaDTO.ToDTO(Objeto);

            return Ok(ObjetoDTO);
        }




        [HttpPost]
        public async Task<ActionResult<AgenciaDTO>> Post(AgenciaDTO tabela)
        {
            IEnumerable<Agencia> ObjetoLista = await _UOW.Agencia.PesquisarPorBancoAgenciaAsync(tabela.BancoId, tabela.NumeroAgencia);
            if (ObjetoLista.Any())
            {
                return BadRequest(Mensagens.MSG_E003);
            }

            if (ModelState.IsValid)
            {
                var ObjetoEntitade = AgenciaDTO.ToEntidade(tabela);
                Agencia Objeto = await _UOW.Agencia.InserirAsync(ObjetoEntitade);
                var ObjetoDTO = AgenciaDTO.ToDTO(Objeto);

                await _UOW.SaveAsync();
                return Ok(ObjetoDTO);

            }
            return BadRequest();

        }

        [HttpPut("{Id}")]
        public async Task<ActionResult<AgenciaDTO>> Put(int Id, AgenciaDTO tabela)
        {
            if (Id != tabela.Id)
                return BadRequest(Mensagens.MSG_E001);

            Agencia ObjetoPesquisa = await _UOW.Agencia.PesquisarPorIdAsync(tabela.Id);
            if (ObjetoPesquisa == null)
            {
                return BadRequest(Mensagens.MSG_E002);
            }

            IEnumerable<Agencia> ObjetoLista = await _UOW.Agencia.PesquisarPorBancoAgenciaAsync(tabela.BancoId, tabela.NumeroAgencia);
            if (ObjetoLista.Any() && ObjetoLista.FirstOrDefault().Id != Id)
            {
                return BadRequest(Mensagens.MSG_E003);
            }

            if (ModelState.IsValid)
            {
                var ObjetoEntitade = AgenciaDTO.ToEntidade(tabela);
                var Objeto = await _UOW.Agencia.AtualizarAsync(ObjetoEntitade);
                var ObjetoDTO = AgenciaDTO.ToDTO(Objeto);

                await _UOW.SaveAsync();
                return Ok(ObjetoDTO);

            }
            return BadRequest();

        }

        [HttpDelete("{Id}")]
        public async Task<ActionResult<int>> Delete(int Id)
        {

            await _UOW.Agencia.DeletarAsync(Id);

            int _removidos = await _UOW.SaveAsync();
            return Ok(_removidos);

        }


    }
}
