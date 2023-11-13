using Dominio.DTO;
using Dominio.Entidades;
using Dominio.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("v1/[controller]")]
    public class TipoInscricaoEmpresaController : Controller
    {
        private readonly IUnitOfWork _UOW;
        public TipoInscricaoEmpresaController(IUnitOfWork unitOfWork)
        {
            _UOW = unitOfWork;
        }

        [HttpGet("GetbyId/{Id}")]
        public async Task<ActionResult<TipoInscricaoEmpresaDTO>> GetbyId(int Id)
        {
            var Objeto = await _UOW.TipoInscricaoEmpresa.PesquisarPorIdAsync(Id);
            if (Objeto == null)
            {
                return NotFound(Mensagens.MSG_E002);
            }
            var ObjetoDTO = TipoInscricaoEmpresaDTO.ToDTO(Objeto);

            return Ok(ObjetoDTO);
        }


        [HttpGet("Codigo")]
        public async Task<ActionResult<TipoInscricaoEmpresaDTO>> Codigo(int Codigo)
        {
            var Objeto = await _UOW.TipoInscricaoEmpresa.PesquisarPorCodigoAsync(Codigo);
            if (Objeto == null)
            {
                return NotFound(Mensagens.MSG_E002);
            }
            var ObjetoDTO = TipoInscricaoEmpresaDTO.ToDTO(Objeto);

            return Ok(ObjetoDTO);
        }

        [HttpGet("Descricao")]
        public async Task<ActionResult<TipoInscricaoEmpresaDTO>> Descricao(string Descricao)
        {
            var Objeto = await _UOW.TipoInscricaoEmpresa.PesquisarPorDescricaoAsync(Descricao);
            if (Objeto == null)
            {
                return NotFound(Mensagens.MSG_E002);
            }
            var ObjetoDTO = TipoInscricaoEmpresaDTO.ToDTO(Objeto);

            return Ok(ObjetoDTO);
        }



        [HttpGet]
        [Route("GetAll")]
        public ActionResult<TipoInscricaoEmpresaDTO> GetAll()
        {
            var Objeto = _UOW.TipoInscricaoEmpresa.ListarTodos();
            var ObjetoDTO = TipoInscricaoEmpresaDTO.ToDTO(Objeto);

            return Ok(ObjetoDTO);
        }




        [HttpPost]
        public async Task<ActionResult<TipoInscricaoEmpresaDTO>> Post(TipoInscricaoEmpresaDTO tabela)
        {
            IEnumerable<TipoInscricaoEmpresa> ObjetoLista = await _UOW.TipoInscricaoEmpresa.PesquisarPorCodigoAsync(tabela.Codigo);
            if (ObjetoLista.Any())
            {
                return BadRequest("O código deste Tipo de Operacao já existe cadastrado!");
            }

            if (ModelState.IsValid)
            {
                var ObjetoEntitade = TipoInscricaoEmpresaDTO.ToEntidade(tabela);
                TipoInscricaoEmpresa Objeto = await _UOW.TipoInscricaoEmpresa.InserirAsync(ObjetoEntitade);
                var ObjetoDTO = TipoInscricaoEmpresaDTO.ToDTO(Objeto);

                await _UOW.SaveAsync();
                return Ok(Objeto);

            }
            return BadRequest();

        }

        [HttpPut("{Id}")]
        public async Task<ActionResult<TipoInscricaoEmpresaDTO>> Put(int Id, TipoInscricaoEmpresaDTO tabela)
        {
            if (Id != tabela.Id)
                return BadRequest(Mensagens.MSG_E001);


            TipoInscricaoEmpresa ObjetoPesquisa = await _UOW.TipoInscricaoEmpresa.PesquisarPorIdAsync(tabela.Id);
            if (ObjetoPesquisa == null)
            {
                return BadRequest(Mensagens.MSG_E002);
            }

            if (ModelState.IsValid)
            {

                var ObjetoEntitade = TipoInscricaoEmpresaDTO.ToEntidade(tabela);
                TipoInscricaoEmpresa Objeto = await _UOW.TipoInscricaoEmpresa.InserirAsync(ObjetoEntitade);
                var ObjetoDTO = TipoInscricaoEmpresaDTO.ToDTO(Objeto);

                await _UOW.SaveAsync();
                return Ok(Objeto);

            }
            return BadRequest();

        }

        [HttpDelete("{Id}")]
        public async Task<ActionResult<int>> Delete(int Id)
        {

            await _UOW.TipoInscricaoEmpresa.DeletarAsync(Id);

            int _removidos = await _UOW.SaveAsync();
            return Ok(_removidos);

        }


    }
}
