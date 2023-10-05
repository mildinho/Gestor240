using Dominio.DTO;
using Dominio.Entidades;
using Dominio.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [ApiController]
    [Route("v1/[controller]")]
    public class PagadorController : Controller
    {
        private readonly IUnitOfWork _UOW;
        public PagadorController(IUnitOfWork unitOfWork)
        {
            _UOW = unitOfWork;
        }


        [HttpGet("GetbyId/{Id}")]
        public async Task<ActionResult<PagadorDTO>> GetbyId(int Id)
        {
            var Objeto = await _UOW.Pagador.PesquisarPorIdAsync(Id);
            if (Objeto == null)
            {
                return NotFound("Registro Não Encontrado!");
            }
            var ObjetoDTO = PagadorDTO.ToDTO(Objeto);

            return Ok(ObjetoDTO);
        }


        [HttpGet("CNPJ_CPF")]
        public async Task<ActionResult<PagadorDTO>> CNPJ_CPFAsync(string CNPJ_CPF)
        {
            var Objeto = await _UOW.Pagador.PesquisarPorCNPJ_CPFAsync(CNPJ_CPF);
            if (Objeto == null)
            {
                return NotFound("Registro Não Encontrado!");
            }
            var ObjetoDTO = PagadorDTO.ToDTO(Objeto);

            return Ok(ObjetoDTO);
        }

        [HttpGet("Nome")]
        public async Task<ActionResult<PagadorDTO>> Nome(string Nome)
        {
            var Objeto = await _UOW.Pagador.PesquisarPorNomeAsync(Nome);
            if (Objeto == null)
            {
                return NotFound("Registro Não Encontrado!");
            }
            var ObjetoDTO = PagadorDTO.ToDTO(Objeto);

            return Ok(ObjetoDTO);
        }

        [HttpGet]
        [Route("GetAll")]
        public ActionResult<PagadorDTO> GetAll()
        {
            var Objeto = _UOW.Pagador.ListarTodos();
            var ObjetoDTO = PagadorDTO.ToDTO(Objeto);

            return Ok(ObjetoDTO);
        }




        [HttpPost]
        public async Task<ActionResult<Pagador>> Post(PagadorDTO tabela)
        {
            IEnumerable<Pagador> ObjetoLista = await _UOW.Pagador.PesquisarPorCNPJ_CPFAsync(tabela.CNPJ_CPF);
            if (ObjetoLista.Any())
            {
                return BadRequest(Mensagens.MSG_E003);
            }

            UF objUF = await _UOW.UF.PesquisarPorIdAsync(tabela.UFId);
            if (objUF == null)
            {
                return BadRequest("UF não Encontrado!");
            }


            if (ModelState.IsValid)
            {
                var ObjetoEntitade = PagadorDTO.ToEntidade(tabela);

                Pagador Objeto = await _UOW.Pagador.InserirAsync(ObjetoEntitade);
                var ObjetoDTO = PagadorDTO.ToDTO(Objeto);

                await _UOW.SaveAsync();
                return Ok(ObjetoDTO);

            }
            return BadRequest();

        }

        [HttpPatch]
        public async Task<ActionResult<PagadorDTO>> Patch(int Id, PagadorDTO tabela)
        {
            if (Id != tabela.Id)
                return BadRequest(Mensagens.MSG_E001);


            UF objUF = await _UOW.UF.PesquisarPorIdAsync(tabela.UFId);
            if (objUF == null)
            {
                return BadRequest("UF não Encontrado!");
            }


            if (ModelState.IsValid)
            {
                var ObjetoEntitade = PagadorDTO.ToEntidade(tabela);
                var Objeto = await _UOW.Pagador.AtualizarAsync(ObjetoEntitade);
                var ObjetoDTO = PagadorDTO.ToDTO(Objeto);

                await _UOW.SaveAsync();
                return Ok(ObjetoDTO);

            }
            return BadRequest();

        }

        [HttpDelete("{Id}")]
        public async Task<ActionResult<int>> Delete(int Id)
        {

            await _UOW.Pagador.DeletarAsync(Id);

            int _removidos = await _UOW.SaveAsync();
            return Ok(_removidos);

        }


    }
}
