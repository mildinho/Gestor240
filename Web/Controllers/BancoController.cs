﻿using Dominio.DTO;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Web.Biblioteca.CRUD;
using Web.Biblioteca.Filtro;
using Web.Biblioteca.msgDefault;
using Web.Biblioteca.Notification;

namespace Web.Controllers
{
    [LoginAutorizacao]
    public class BancoController : _BaseController<BancoController>
    {

        public BancoController()
        {

        }

        public async Task<IActionResult> Index()
        {
            return await Index_Geral<BancoDTO>("Banco/GetAll", "Index");
        }


        [HttpGet]
        public IActionResult Cadastrar()
        {
            ViewBag.CRUD = ConfiguraMensagem(Opcoes.Create);

            return View("Manutencao");
        }



        [HttpGet]
        public async Task<IActionResult> Editar(int Id)
        {
            ViewBag.CRUD = ConfiguraMensagem(Opcoes.Update);
            ExecutaAPI.ParametrosAPI.Add(Id.ToString());

            return await Editar_Geral<BancoDTO>("Banco/GetbyId", "Manutencao");
        }


        [HttpGet]
        public async Task<IActionResult> Consultar(int Id)
        {
            ViewBag.CRUD = ConfiguraMensagem(Opcoes.Read);
            ExecutaAPI.ParametrosAPI.Add(Id.ToString());

            return await Editar_Geral<BancoDTO>("Banco/GetbyId", "Manutencao");
        }


        [HttpGet]
        public async Task<IActionResult> Deletar(int Id)
        {
            ViewBag.CRUD = ConfiguraMensagem(Opcoes.Delete);
            ExecutaAPI.ParametrosAPI.Add(Id.ToString());

            var retornoApi = await ExecutaAPI.GetAPI("Banco/GetbyId");
            if (retornoApi.success)
            {
                var objRetorno = JsonConvert.DeserializeObject<BancoDTO>(retornoApi.data);
                return View("Manutencao", objRetorno);
            }
            else
            {
                AlertNotification.Error(retornoApi.data);
                return RedirectToAction(nameof(Index));

            }



        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Manutencao([FromForm] BancoDTO banco, Opcoes operacao)
        {
            if (Opcoes.Delete == (Opcoes)operacao)
            {

                ExecutaAPI.ParametrosAPI.Add(banco.Id.ToString());

                var retornoApi = await ExecutaAPI.DeleteAPI("Banco");
                if (retornoApi.success)
                {
                    AlertNotification.Success(mensagens.MSG_S003);
                }
                else
                {
                    AlertNotification.Error(retornoApi.data);
                }


                return RedirectToAction(nameof(Index));
            }
            else if (ModelState.IsValid)
            {
                if (Opcoes.Create == (Opcoes)operacao)
                {
                    var retornoApi = await ExecutaAPI.PostAPI("Banco", banco);


                    if (retornoApi.success)
                    {
                        AlertNotification.Success(mensagens.MSG_S001);
                    }
                    else
                    {
                        ViewBag.CRUD = ConfiguraMensagem(Opcoes.Create);


                        AlertNotification.Error(retornoApi.data);

                        return View("Manutencao", banco);
                    }
                }
                else if (Opcoes.Update == (Opcoes)operacao)
                {
                    ExecutaAPI.ParametrosAPI.Add(banco.Id.ToString());

                    var retornoApi = await ExecutaAPI.PutAPI("Banco", banco);
                    if (retornoApi.success)
                    {
                        AlertNotification.Success(mensagens.MSG_S002);
                    }
                    else
                    {
                        ViewBag.CRUD = ConfiguraMensagem(Opcoes.Update);


                        AlertNotification.Error(retornoApi.data);

                        return View("Manutencao", banco);
                    }


                }

                return RedirectToAction(nameof(Index));

            }


            ViewBag.CRUD = ConfiguraMensagem((Opcoes)operacao);

            return View();

        }



        private CRUD ConfiguraMensagem(Opcoes opcoes)
        {
            CRUD objCRUD = new();

            if (opcoes == Opcoes.Information)
            {
                objCRUD.Titulo = "Banco";
                objCRUD.Descricao = "Aqui você poderá configurar seu Banco Financeiro";
                objCRUD.SubTitulo = "Dados para Controlar seus Bancos";
                objCRUD.Operacao = Opcoes.Information;

            }
            else if (opcoes == Opcoes.Create)
            {
                objCRUD.Titulo = "Incluir Banco Financeiro";
                objCRUD.Descricao = "Aqui você poderá configurar seu Cadastro de Bancos Financeiros";
                objCRUD.SubTitulo = "Inserir Novo Banco";
                objCRUD.Operacao = Opcoes.Create;
            }
            else if (opcoes == Opcoes.Update)
            {
                objCRUD.Titulo = "Alterar Banco Financeiro";
                objCRUD.Descricao = "Aqui você poderá configurar seu Cadastro Bancos Financeiros";
                objCRUD.SubTitulo = "Alterar Banco Financeiro";
                objCRUD.Operacao = Opcoes.Update;
            }
            else if (opcoes == Opcoes.Delete)
            {
                objCRUD.Titulo = "Excluir Banco Financeiro";
                objCRUD.Descricao = "CUIDADO ao Excluir um Banco, Este processo é irreversivel";
                objCRUD.SubTitulo = "Excluir Banco Financeiro";
                objCRUD.Operacao = Opcoes.Delete;
            }
            else if (opcoes == Opcoes.Read)
            {
                objCRUD.Titulo = "Consultar Banco Financeiro";
                objCRUD.Descricao = "Aqui você poderá consultar seu Cadastro de Banco Financeiro";
                objCRUD.SubTitulo = "Consultar Banco Financeiro";
                objCRUD.Operacao = Opcoes.Read;
            }

            return objCRUD;
        }


    }
}
