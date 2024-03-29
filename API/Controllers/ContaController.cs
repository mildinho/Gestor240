﻿using Dominio.DTO;
using Dominio.Entidades;
using Dominio.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("v1/[controller]")]
    [Authorize]
    public class ContaController : Controller
    {
        private readonly IUnitOfWork _UOW;
        public ContaController(IUnitOfWork unitOfWork)
        {
            _UOW = unitOfWork;
        }

        [HttpGet("GetbyId/{Id}")]
        public async Task<ActionResult<ContaDTO>> GetbyId(int Id)
        {
            var Objeto = await _UOW.Conta.PesquisarPorIdAgregadoAsync(Id);
            if (Objeto == null)
            {
                return NotFound(Mensagens.MSG_E002);
            }
            var ObjetoDTO = ContaDTO.ToDTO(Objeto);

            return Ok(ObjetoDTO);
        }


        [HttpGet("Codigo")]
        public async Task<ActionResult<ContaDTO>> Get(int IdAgencia, int Conta)
        {
            var Objeto = await _UOW.Conta.PesquisarPorAgenciaContaAsync(IdAgencia, Conta);
            if (Objeto == null)
            {
                return NotFound(Mensagens.MSG_E002);
            }
            var ObjetoDTO = ContaDTO.ToDTO(Objeto);

            return Ok(ObjetoDTO);
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<ActionResult<ContaDTO>> GetAll()
        {
            var Objeto = await _UOW.Conta.ListarTodosAgregados();
            var ObjetoDTO = ContaDTO.ToDTO(Objeto);

            return Ok(ObjetoDTO);
        }




        [HttpPost]
        public async Task<ActionResult<Conta>> Post(Conta tabela)
        {
            IEnumerable<Conta> ContaLista = await _UOW.Conta.PesquisarPorAgenciaContaAsync(tabela.AgenciaId, tabela.NumeroConta);
            if (ContaLista.Any())
            {
                return BadRequest(Mensagens.MSG_E003);
            }

            Beneficiario BeneficiarioObjeto = await _UOW.Beneficiario.PesquisarPorIdAsync(tabela.BeneficiarioID);
            if (BeneficiarioObjeto == null)
            {
                return BadRequest("Beneficiário Não Encontrado");
            }

            if (ModelState.IsValid)
            {
                Conta Objeto = await _UOW.Conta.InserirAsync(tabela);

                await _UOW.SaveAsync();
                return Ok(Objeto);

            }
            return BadRequest();

        }

        [HttpPut("{Id}")]
        public async Task<ActionResult<Conta>> Put(int Id, Conta tabela)
        {
            if (Id != tabela.Id)
                return BadRequest(Mensagens.MSG_E001);


            Conta ObjetoPesquisa = await _UOW.Conta.PesquisarPorIdAsync(tabela.Id);
            if (ObjetoPesquisa == null)
            {
                return BadRequest(Mensagens.MSG_E002);
            }

            Beneficiario BeneficiarioObjeto = await _UOW.Beneficiario.PesquisarPorIdAsync(tabela.BeneficiarioID);
            if (BeneficiarioObjeto == null)
            {
                return BadRequest("Beneficiário Não Encontrado");
            }

            IEnumerable<Conta> ObjetoLista = await _UOW.Conta.PesquisarPorAgenciaContaAsync(tabela.AgenciaId, tabela.NumeroConta);
            if (ObjetoLista.Any() && ObjetoLista.FirstOrDefault().Id != Id)
            {
                return BadRequest(Mensagens.MSG_E003);
            }


            if (ModelState.IsValid)
            {

                var Objeto = await _UOW.Conta.AtualizarAsync(tabela);

                await _UOW.SaveAsync();
                return Ok(Objeto);

            }
            return BadRequest();

        }

        [HttpDelete("{Id}")]
        public async Task<ActionResult<int>> Delete(int Id)
        {

            await _UOW.Conta.DeletarAsync(Id);

            int _removidos = await _UOW.SaveAsync();
            return Ok(_removidos);

        }


    }
}
