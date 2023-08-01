using Dominio.Entidades;
using Dominio.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [ApiController]
    [Route("v1/[controller]")]
    public class EmpresaController : Controller
    {
        private readonly IUnitOfWork _UOW;
        public EmpresaController(IUnitOfWork unitOfWork)
        {
            _UOW = unitOfWork;
        }

        [HttpGet("CNPJ_CPF")]
        public async Task<ActionResult<Empresa>> CNPJ_CPFAsync(double CNPJ_CPF)
        {
            var Objeto = await _UOW.Empresa.PesquisarPorCNPJ_CPFAsync(CNPJ_CPF);

            return Ok(Objeto);
        }

        [HttpGet("Nome")]
        public async Task<ActionResult<Empresa>> Nome(string Descricao)
        {
            var Objeto = await _UOW.Empresa.PesquisarPorNomeAsync(Descricao);

            return Ok(Objeto);
        }

        [HttpGet]
        [Route("GetAll")]
        public ActionResult<Empresa> GetAll()
        {
            var Objeto = _UOW.Empresa.ListarTodos();

            return Ok(Objeto);
        }




        [HttpPost]
        public async Task<ActionResult<Empresa>> Post(Empresa tabela)
        {
            IEnumerable<Empresa> EmpresaLista = await _UOW.Empresa.PesquisarPorCNPJ_CPFAsync(tabela.CNPJ_CPF);
            if (EmpresaLista.Any())
            {
                return BadRequest("já existe este CNPJ cadastrado!");
            }

            if (ModelState.IsValid)
            {
                Empresa Objeto = await _UOW.Empresa.InserirAsync(tabela);

                await _UOW.SaveAsync();
                return Ok(Objeto);

            }
            return BadRequest();

        }

        [HttpPatch]
        public async Task<ActionResult<Empresa>> Patch(int Id, Empresa tabela)
        {
            if (Id != tabela.Id)
                return BadRequest("O para ID está diferente do ID do Modelo!");


            if (ModelState.IsValid)
            {

                var Objeto = await _UOW.Empresa.AtualizarAsync(tabela);

                await _UOW.SaveAsync();
                return Ok(Objeto);

            }
            return BadRequest();

        }

        [HttpDelete("{Id}")]
        public async Task<ActionResult<int>> Delete(int Id)
        {

            await _UOW.Empresa.DeletarAsync(Id);

            int _removidos = await _UOW.SaveAsync();
            return Ok(_removidos);

        }


    }
}
