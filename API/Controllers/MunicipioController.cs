using Dominio.DTO;
using Dominio.Entidades;
using Dominio.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("v1/[controller]")]
    public class MunicipioController : Controller
    {
        private readonly IUnitOfWork _UOW;
        public MunicipioController(IUnitOfWork unitOfWork)
        {
            _UOW = unitOfWork;
        }


        [HttpGet("GetbyId/{Id}")]
        public async Task<ActionResult<MunicipioDTO>> GetbyId(int Id)
        {
            var Objeto = await _UOW.Municipio.PesquisarPorIdAsync(Id);
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
            var Objeto = await _UOW.Municipio.PesquisarPorDescricaoAsync(Descricao);
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
            var Objeto = await _UOW.Municipio.ListarTodosAgregados();
            var ObjetoDTO = MunicipioDTO.ToDTO(Objeto);

            return Ok(ObjetoDTO);
        }




        [HttpPost]
        public async Task<ActionResult<MunicipioDTO>> Post(MunicipioDTO tabela)
        {
            IEnumerable<Municipio> ObjetoLista = await _UOW.Municipio.PesquisarPorDescricaoAsync(tabela.Nome);
            if (ObjetoLista.Any())
            {
                return BadRequest(Mensagens.MSG_E003);
            }

            if (ModelState.IsValid)
            {
                var ObjetoEntitade = MunicipioDTO.ToEntidade(tabela);
                Municipio Objeto = await _UOW.Municipio.InserirAsync(ObjetoEntitade);

                var ObjetoDTO = MunicipioDTO.ToDTO(Objeto);

                await _UOW.SaveAsync();

                return Ok(ObjetoDTO);

            }
            return BadRequest();

        }

        [HttpPut("{Id}")]
        public async Task<ActionResult<MunicipioDTO>> Put(int Id, MunicipioDTO tabela)
        {
            if (Id != tabela.Id)
                return BadRequest(Mensagens.MSG_E001);

            Municipio ObjetoPesquisa = await _UOW.Municipio.PesquisarPorIdAsync(tabela.Id);
            if (ObjetoPesquisa == null)
            {
                return BadRequest(Mensagens.MSG_E002);
            }

            if (ModelState.IsValid)
            {

                var ObjetoEntitade = MunicipioDTO.ToEntidade(tabela);
                Municipio Objeto = await _UOW.Municipio.InserirAsync(ObjetoEntitade);

                var ObjetoDTO = MunicipioDTO.ToDTO(Objeto);

                await _UOW.SaveAsync();

                return Ok(ObjetoDTO);

            }
            return BadRequest();

        }

        [HttpDelete("{Id}")]
        public async Task<ActionResult<int>> Delete(int Id)
        {

            await _UOW.Municipio.DeletarAsync(Id);

            int _removidos = await _UOW.SaveAsync();
            return Ok(_removidos);

        }


    }
}
