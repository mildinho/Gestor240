using Dominio.DTO;
using Dominio.Entidades;
using Dominio.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [ApiController]
    [Route("v1/[controller]")]
    public class BeneficiarioController : Controller
    {
        private readonly IUnitOfWork _UOW;
        public BeneficiarioController(IUnitOfWork unitOfWork)
        {
            _UOW = unitOfWork;
        }

        [HttpGet("CNPJ_CPF")]
        public async Task<ActionResult<BeneficiarioDTO>> CNPJ_CPFAsync(string CNPJ_CPF)
        {
            var Objeto = await _UOW.Beneficiario.PesquisarPorCNPJ_CPFAsync(CNPJ_CPF);
            if (Objeto == null)
            {
                return NotFound("Registro Não Encontrado!");
            }
            var ObjetoDTO = BeneficiarioDTO.ToDTO(Objeto);

            return Ok(ObjetoDTO);
        }

        [HttpGet("Nome")]
        public async Task<ActionResult<BeneficiarioDTO>> Nome(string Nome)
        {
            var Objeto = await _UOW.Beneficiario.PesquisarPorNomeAsync(Nome);
            if (Objeto == null)
            {
                return NotFound("Registro Não Encontrado!");
            }
            var ObjetoDTO = BeneficiarioDTO.ToDTO(Objeto);

            return Ok(ObjetoDTO);
        }

        [HttpGet]
        [Route("GetAll")]
        public ActionResult<BeneficiarioDTO> GetAll()
        {
            var Objeto = _UOW.Beneficiario.ListarTodos();
            var ObjetoDTO = BeneficiarioDTO.ToDTO(Objeto);

            return Ok(ObjetoDTO);
        }




        [HttpPost]
        public async Task<ActionResult<Beneficiario>> Post(BeneficiarioDTO tabela)
        {
            IEnumerable<Beneficiario> ObjetoLista = await _UOW.Beneficiario.PesquisarPorCNPJ_CPFAsync(tabela.CNPJ_CPF);
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
                var ObjetoEntitade = BeneficiarioDTO.ToEntidade(tabela);

                Beneficiario Objeto = await _UOW.Beneficiario.InserirAsync(ObjetoEntitade);
                var ObjetoDTO = BeneficiarioDTO.ToDTO(Objeto);

                await _UOW.SaveAsync();
                return Ok(ObjetoDTO);

            }
            return BadRequest();

        }

        [HttpPatch]
        public async Task<ActionResult<BeneficiarioDTO>> Patch(int Id, BeneficiarioDTO tabela)
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
                var ObjetoEntitade = BeneficiarioDTO.ToEntidade(tabela);
                var Objeto = await _UOW.Beneficiario.AtualizarAsync(ObjetoEntitade);
                var ObjetoDTO = BeneficiarioDTO.ToDTO(Objeto);

                await _UOW.SaveAsync();
                return Ok(ObjetoDTO);

            }
            return BadRequest();

        }

        [HttpDelete("{Id}")]
        public async Task<ActionResult<int>> Delete(int Id)
        {

            await _UOW.Beneficiario.DeletarAsync(Id);

            int _removidos = await _UOW.SaveAsync();
            return Ok(_removidos);

        }


    }
}
