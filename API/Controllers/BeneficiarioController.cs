﻿using Dominio.DTO;
using Dominio.Entidades;
using Dominio.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [ApiController]
    [Route("v1/[controller]")]
    [Authorize]
    public class BeneficiarioController : Controller
    {
        private readonly IUnitOfWork _UOW;
        public BeneficiarioController(IUnitOfWork unitOfWork)
        {
            _UOW = unitOfWork;
        }

        [HttpGet("GetbyId/{Id}")]
        public async Task<ActionResult<AgenciaDTO>> GetbyId(int Id)
        {
            var Objeto = await _UOW.Beneficiario.PesquisarPorIdAsync(Id);
            if (Objeto == null)
            {
                return NotFound(Mensagens.MSG_E002);
            }
            var ObjetoDTO = BeneficiarioDTO.ToDTO(Objeto);

            return Ok(ObjetoDTO);

        }

        [HttpGet("CNPJ_CPF/{CNPJ_CPF}")]
        public async Task<ActionResult<BeneficiarioDTO>> CNPJ_CPF(string CNPJ_CPF)
        {
            var Objeto = await _UOW.Beneficiario.PesquisarPorCNPJ_CPFAsync(CNPJ_CPF);

            if (Objeto == null)
            {
                return NotFound(Mensagens.MSG_E002);
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
                return NotFound(Mensagens.MSG_E002);
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
            Beneficiario ObjBeneficiario = await _UOW.Beneficiario.PesquisarPorCNPJ_CPFAsync(tabela.CNPJ_CPF);
            if (ObjBeneficiario != null)
            {
                return BadRequest(Mensagens.MSG_E005);
            }

            UF objUF = await _UOW.UF.PesquisarPorIdAsync(tabela.UFId);
            if (objUF == null)
            {
                return BadRequest(Mensagens.MSG_E002);
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

        [HttpPut("{Id}")]
        public async Task<ActionResult<BeneficiarioDTO>> Put(int Id, BeneficiarioDTO tabela)
        {
            if (Id != tabela.Id)
                return BadRequest(Mensagens.MSG_E001);


            UF objUF = await _UOW.UF.PesquisarPorIdAsync(tabela.UFId);
            if (objUF == null)
            {
                return BadRequest(Mensagens.MSG_E002);
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
