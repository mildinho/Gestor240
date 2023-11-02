using Dominio.DTO;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Web.Biblioteca.CRUD;
using Web.Biblioteca.msgDefault;
using Web.Biblioteca.Notification;
using Web.Services;

namespace Web.Controllers
{
    public class UFController : _BaseController<UFController>
    {

        public UFController()
        {

        }

        public async Task<IActionResult> Index()
        {
            var retornoApi = await ExecutaAPI.GetAPI("UF/GetAll");
            var objRetorno = JsonConvert.DeserializeObject<List<UFDTO>>(retornoApi.data);

            return View(objRetorno);
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

            var retornoApi = await ExecutaAPI.GetAPI("UF/GetbyId");
            var objRetorno = JsonConvert.DeserializeObject<UFDTO>(retornoApi.data);



            return View("Manutencao", objRetorno);
        }


        [HttpGet]
        public async Task<IActionResult> Consultar(int Id)
        {
            ViewBag.CRUD = ConfiguraMensagem(Opcoes.Read);
            ExecutaAPI.ParametrosAPI.Add(Id.ToString());

            var retornoApi = await ExecutaAPI.GetAPI("UF/GetbyId");
            if (retornoApi.success)
            {
                var objRetorno = JsonConvert.DeserializeObject<UFDTO>(retornoApi.data);
                return View("Manutencao", objRetorno);
            }
            else
            {
                AlertNotification.Error(retornoApi.data);
                return RedirectToAction(nameof(Index));

            }
        }


        [HttpGet]
        public async Task<IActionResult> Deletar(int Id)
        {
            ViewBag.CRUD = ConfiguraMensagem(Opcoes.Delete);
            ExecutaAPI.ParametrosAPI.Add(Id.ToString());

            var retornoApi = await ExecutaAPI.GetAPI("UF/GetbyId");
            if (retornoApi.success)
            {
                var objRetorno = JsonConvert.DeserializeObject<UFDTO>(retornoApi.data);
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
        public async Task<IActionResult> Manutencao([FromForm] UFDTO uf, Opcoes operacao)
        {
            if (Opcoes.Delete == (Opcoes)operacao)
            {

                ExecutaAPI.ParametrosAPI.Add(uf.Id.ToString());

                var retornoApi = await ExecutaAPI.DeleteAPI("UF");
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
                    var retornoApi = await ExecutaAPI.PostAPI("UF", uf);


                    if (retornoApi.success)
                    {
                        AlertNotification.Success(mensagens.MSG_S001);
                    }
                    else
                    {
                        ViewBag.CRUD = ConfiguraMensagem(Opcoes.Create);


                        AlertNotification.Error(retornoApi.data);

                        return View("Manutencao", uf);
                    }
                }
                else if (Opcoes.Update == (Opcoes)operacao)
                {
                    ExecutaAPI.ParametrosAPI.Add(uf.Id.ToString());

                    var retornoApi = await ExecutaAPI.PutAPI("UF", uf);
                    if (retornoApi.success)
                    {
                        AlertNotification.Success(mensagens.MSG_S002);
                    }
                    else
                    {
                        ViewBag.CRUD = ConfiguraMensagem(Opcoes.Update);


                        AlertNotification.Error(retornoApi.data);

                        return View("Manutencao", uf);
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
                objCRUD.Titulo = "Unidade Federativa";
                objCRUD.Descricao = "Aqui você poderá configurar o Estado (UF)";
                objCRUD.SubTitulo = "Dados para Controlar os Estados";
                objCRUD.Operacao = Opcoes.Information;

            }
            else if (opcoes == Opcoes.Create)
            {
                objCRUD.Titulo = "Incluir Unidade Federativa";
                objCRUD.Descricao = "Aqui você poderá configurar sua UF";
                objCRUD.SubTitulo = "Inserir Nova UF";
                objCRUD.Operacao = Opcoes.Create;
            }
            else if (opcoes == Opcoes.Update)
            {
                objCRUD.Titulo = "Alterar UF";
                objCRUD.Descricao = "Aqui você poderá configurar seu Cadastro de UF";
                objCRUD.SubTitulo = "Alterar UF";
                objCRUD.Operacao = Opcoes.Update;
            }
            else if (opcoes == Opcoes.Delete)
            {
                objCRUD.Titulo = "Excluir UF";
                objCRUD.Descricao = "CUIDADO ao Excluir uma UF, Este processo é irreversivel";
                objCRUD.SubTitulo = "Excluir UF";
                objCRUD.Operacao = Opcoes.Delete;
            }
            else if (opcoes == Opcoes.Read)
            {
                objCRUD.Titulo = "Consultar UF";
                objCRUD.Descricao = "Aqui você poderá consultar seu Cadastro de UF";
                objCRUD.SubTitulo = "Consultar UF";
                objCRUD.Operacao = Opcoes.Read;
            }

            return objCRUD;
        }




    }
}
