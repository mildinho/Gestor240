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

       
        [HttpGet("Descricao")]
        public async Task<ActionResult<MunicipioDTO>> Descricao(string Descricao)
        {
            var Objeto = await _UOW.Municipio.PesquisarPorDescricaoAsync(Descricao);
            if (Objeto == null)
            {
                return NotFound("Registro Não Encontrado!");
            }
            var ObjetoDTO = MunicipioDTO.ToDTO(Objeto);

            return Ok(ObjetoDTO);
        }

        [HttpGet]
        [Route("GetAll")]
        public ActionResult<MunicipioDTO> GetAll()
        {
            var Objeto = _UOW.Municipio.ListarTodos();
            var ObjetoDTO = MunicipioDTO.ToDTO(Objeto);

            return Ok(ObjetoDTO);
        }




        [HttpPost]
        public async Task<ActionResult<MunicipioDTO>> Post(MunicipioDTO tabela)
        {
            IEnumerable<Municipio> ObjetoLista = await _UOW.Municipio.PesquisarPorDescricaoAsync(tabela.Descricao);
            if (ObjetoLista.Any())
            {
                return BadRequest("O código deste UF já existe cadastrado!");
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

        [HttpPatch]
        public async Task<ActionResult<MunicipioDTO>> Patch(int Id, MunicipioDTO tabela)
        {
            if (Id != tabela.Id)
                return BadRequest("O para ID está diferente do ID do Modelo!");


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
