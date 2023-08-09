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
        public async Task<ActionResult<Beneficiario>> CNPJ_CPFAsync(string CNPJ_CPF)
        {
            var Objeto = await _UOW.Beneficiario.PesquisarPorCNPJ_CPFAsync(CNPJ_CPF);

            return Ok(Objeto);
        }

        [HttpGet("Nome")]
        public async Task<ActionResult<Beneficiario>> Nome(string Descricao)
        {
            var Objeto = await _UOW.Beneficiario.PesquisarPorNomeAsync(Descricao);

            return Ok(Objeto);
        }

        [HttpGet]
        [Route("GetAll")]
        public ActionResult<Beneficiario> GetAll()
        {
            var Objeto = _UOW.Beneficiario.ListarTodos();

            return Ok(Objeto);
        }




        [HttpPost]
        public async Task<ActionResult<Beneficiario>> Post(Beneficiario tabela)
        {
            IEnumerable<Beneficiario> ObjetoLista = await _UOW.Beneficiario.PesquisarPorCNPJ_CPFAsync(tabela.CNPJ_CPF);
            if (ObjetoLista.Any())
            {
                return BadRequest("já existe este CNPJ cadastrado!");
            }

            UF objUF = await _UOW.UF.PesquisarPorIdAsync(tabela.UFId);
            if (objUF == null)
            {
                return BadRequest("UF não Encontrado!");
            }


            if (ModelState.IsValid)
            {
                Beneficiario Objeto = await _UOW.Beneficiario.InserirAsync(tabela);

                await _UOW.SaveAsync();
                return Ok(Objeto);

            }
            return BadRequest();

        }

        [HttpPatch]
        public async Task<ActionResult<Beneficiario>> Patch(int Id, Beneficiario tabela)
        {
            if (Id != tabela.Id)
                return BadRequest("O para ID está diferente do ID do Modelo!");


            UF objUF = await _UOW.UF.PesquisarPorIdAsync(tabela.UFId);
            if (objUF == null)
            {
                return BadRequest("UF não Encontrado!");
            }


            if (ModelState.IsValid)
            {

                var Objeto = await _UOW.Beneficiario.AtualizarAsync(tabela);

                await _UOW.SaveAsync();
                return Ok(Objeto);

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
