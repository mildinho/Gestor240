﻿using Dominio.Entidades;
using Dominio.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Drawing;

namespace API.Controllers
{
    [ApiController]
    [Route("v1/[controller]")]
    public class AgenciaController : Controller
    {
        private readonly IUnitOfWork _UOW;
        public AgenciaController(IUnitOfWork unitOfWork)
        {
            _UOW = unitOfWork;
        }

        [HttpGet("Codigo")]
        public async Task<ActionResult<Agencia>> Get(int Banco, string Agencia, string Conta)
        {
            var Objeto = await _UOW.Agencia.PesquisarPorBancoAgenciaContaAsync(Banco, Agencia, Conta);

            return Ok(Objeto);
        }

        [HttpGet]
        [Route("GetAll")]
        public ActionResult<Agencia> GetAll()
        {
            var Objeto = _UOW.Agencia.ListarTodos();

            return Ok(Objeto);
        }




        [HttpPost]
        public async Task<ActionResult<Agencia>> Post(Agencia tabela)
        {
            IEnumerable<Agencia> ObjetoLista = await _UOW.Agencia.
                PesquisarPorBancoAgenciaContaAsync(tabela.BancoId, tabela.NumeroAgencia, tabela.NumeroConta);
            if (ObjetoLista.Any())
            {
                return BadRequest("O código desta Agencia/Banco/Conta já existe cadastrado!");
            }

            if (ModelState.IsValid)
            {
                Agencia Objeto = await _UOW.Agencia.InserirAsync(tabela);

                await _UOW.SaveAsync();
                return Ok(Objeto);

            }
            return BadRequest();

        }

        [HttpPatch]
        public async Task<ActionResult<Agencia>> Patch(int Id, Agencia tabela)
        {
            if (Id != tabela.Id)
                return BadRequest("O para ID está diferente do ID do Modelo!");


            if (ModelState.IsValid)
            {

                var Objeto = await _UOW.Agencia.AtualizarAsync(tabela);

                await _UOW.SaveAsync();
                return Ok(Objeto);

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
