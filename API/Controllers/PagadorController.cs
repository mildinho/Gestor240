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

        [HttpGet("CNPJ_CPF")]
        public async Task<ActionResult<Pagador>> CNPJ_CPFAsync(string CNPJ_CPF)
        {
            var Objeto = await _UOW.Pagador.PesquisarPorCNPJ_CPFAsync(CNPJ_CPF);

            return Ok(Objeto);
        }

        [HttpGet("Nome")]
        public async Task<ActionResult<Pagador>> Nome(string Nome)
        {
            var Objeto = await _UOW.Pagador.PesquisarPorNomeAsync(Nome);

            return Ok(Objeto);
        }

        [HttpGet]
        [Route("GetAll")]
        public ActionResult<Pagador> GetAll()
        {
            var Objeto = _UOW.Pagador.ListarTodos();

            return Ok(Objeto);
        }




        [HttpPost]
        public async Task<ActionResult<Pagador>> Post(Pagador tabela)
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
                Pagador Objeto = await _UOW.Pagador.InserirAsync(tabela);

                await _UOW.SaveAsync();
                return Ok(Objeto);

            }
            return BadRequest();

        }

        [HttpPatch]
        public async Task<ActionResult<Pagador>> Patch(int Id, Pagador tabela)
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

                var Objeto = await _UOW.Pagador.AtualizarAsync(tabela);

                await _UOW.SaveAsync();
                return Ok(Objeto);

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
