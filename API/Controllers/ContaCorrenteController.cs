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
    public class ContaCorrenteController : Controller
    {
        private readonly IUnitOfWork _UOW;

        public ContaCorrenteController(IUnitOfWork unitOfWork)
        {
            _UOW = unitOfWork;
        }

        [HttpGet("GetbyId/{Id}")]
        public async Task<ActionResult<ContaCorrenteDTO>> GetbyId(int Id)
        {
            var Objeto = await _UOW.ContaCorrente.PesquisarPorIdAsync(Id);
            if (Objeto == null)
            {
                return NotFound(Mensagens.MSG_E002);
            }
            var ObjetoDTO = ContaCorrenteDTO.ToDTO(Objeto);

            return Ok(ObjetoDTO);
        }


        [HttpGet("GetbyIdCCIdPagador")]
        public async Task<ActionResult<ContaCorrenteDTO>> GetbyIdCCIdPagador(int IdTipoCC, int IdPagador)
        {
            IEnumerable<ContaCorrente> ObjetoLista = await _UOW.ContaCorrente.PesquisarPorTipoCC_PagadorAsync(IdTipoCC, IdPagador);
            if (ObjetoLista.Count() <= 0)
            {
                return BadRequest(Mensagens.MSG_E002);
            }


            var ObjetoDTO = ContaCorrenteDTO.ToDTO(ObjetoLista);

            return Ok(ObjetoDTO);
        }





        [HttpPost]
        public async Task<ActionResult<ContaCorrenteDTO>> Post(ContaCorrenteDTO tabela)
        {

            Pagador ObjPagador = await _UOW.Pagador.PesquisarPorIdAsync(tabela.PagadorID);
            if (ObjPagador == null)
            {
                return NotFound(Mensagens.MSG_E002);
            }

            TipoContaCorrente ObjTipoCC = await _UOW.TipoContaCorrente.PesquisarPorIdAsync(tabela.TipoContaCorrenteId);
            if (ObjTipoCC == null)
            {
                return NotFound(Mensagens.MSG_E002);
            }



            if (ModelState.IsValid)
            {
                var ObjetoEntitade = ContaCorrenteDTO.ToEntidade(tabela);
                ContaCorrente Objeto = await _UOW.ContaCorrente.InserirAsync(ObjetoEntitade);

                var ObjetoDTO = ContaCorrenteDTO.ToDTO(Objeto);
                await _UOW.SaveAsync();



                return Ok(ObjetoDTO);

            }
            return BadRequest();

        }



        [HttpDelete("{Id}")]
        public async Task<ActionResult<int>> Delete(int Id)
        {
            await _UOW.ContaCorrente.DeletarAsync(Id);

            int _removidos = await _UOW.SaveAsync();

            return Ok(_removidos);
        }


    }
}
