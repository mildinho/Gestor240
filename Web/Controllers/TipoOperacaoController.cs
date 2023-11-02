using Dominio.DTO;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Web.Biblioteca.CRUD;
using Web.Biblioteca.msgDefault;
using Web.Biblioteca.Notification;
using Web.Services;

namespace Web.Controllers
{
    public class TipoOperacaoController : _BaseController<TipoOperacaoController>
    {

        public TipoOperacaoController()
        {

        }

        public async Task<IActionResult> Index()
        {
            var retornoApi = await ExecutaAPI.GetAPI("TipoOperacao/GetAll");
            var objRetorno = JsonConvert.DeserializeObject<List<TipoOperacaoDTO>>(retornoApi.data);

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

            var retornoApi = await ExecutaAPI.GetAPI("TipoOperacao/GetbyId");
            var objRetorno = JsonConvert.DeserializeObject<TipoOperacaoDTO>(retornoApi.data);



            return View("Manutencao", objRetorno);
        }


        [HttpGet]
        public async Task<IActionResult> Consultar(int Id)
        {
            ViewBag.CRUD = ConfiguraMensagem(Opcoes.Read);
            ExecutaAPI.ParametrosAPI.Add(Id.ToString());

            var retornoApi = await ExecutaAPI.GetAPI("TipoOperacao/GetbyId");
            if (retornoApi.success)
            {
                var objRetorno = JsonConvert.DeserializeObject<TipoOperacaoDTO>(retornoApi.data);
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

            var retornoApi = await ExecutaAPI.GetAPI("TipoOperacao/GetbyId");
            if (retornoApi.success)
            {
                var objRetorno = JsonConvert.DeserializeObject<TipoOperacaoDTO>(retornoApi.data);
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
        public async Task<IActionResult> Manutencao([FromForm] TipoOperacaoDTO tipoOperacao, Opcoes operacao)
        {

            if (Opcoes.Delete == (Opcoes)operacao)
            {

                ExecutaAPI.ParametrosAPI.Add(tipoOperacao.Id.ToString());

                var retornoApi = await ExecutaAPI.DeleteAPI("TipoOperacao");
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
                    var retornoApi = await ExecutaAPI.PostAPI("TipoOperacao", tipoOperacao);


                    if (retornoApi.success)
                    {
                        AlertNotification.Success(mensagens.MSG_S001);
                    }
                    else
                    {
                        ViewBag.CRUD = ConfiguraMensagem(Opcoes.Create);


                        AlertNotification.Error(retornoApi.data);

                        return View("Manutencao", tipoOperacao);
                    }
                }
                else if (Opcoes.Update == (Opcoes)operacao)
                {
       
                    ExecutaAPI.ParametrosAPI.Add(tipoOperacao.Id.ToString());

                    var retornoApi = await ExecutaAPI.PutAPI("TipoOperacao", tipoOperacao);
                    if (retornoApi.success)
                    {
                        AlertNotification.Success(mensagens.MSG_S002);
                    }
                    else
                    {
                        ViewBag.CRUD = ConfiguraMensagem(Opcoes.Update);


                        AlertNotification.Error(retornoApi.data);

                        return View("Manutencao", tipoOperacao);
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
                objCRUD.Titulo = "Tipo Operação";
                objCRUD.Descricao = "Aqui você poderá configurar o Tipo de Operação";
                objCRUD.SubTitulo = "Dados para Controlar os Estados";
                objCRUD.Operacao = Opcoes.Information;

            }
            else if (opcoes == Opcoes.Create)
            {
                objCRUD.Titulo = "Incluir Tipo de Operação";
                objCRUD.Descricao = "Aqui você poderá configurar o Tipo de Operação";
                objCRUD.SubTitulo = "Inserir Novo Tipo de Operação";
                objCRUD.Operacao = Opcoes.Create;
            }
            else if (opcoes == Opcoes.Update)
            {
                objCRUD.Titulo = "Alterar Tipo de Operação";
                objCRUD.Descricao = "Aqui você poderá configurar seu Cadastro de Tipo de Operaçao";
                objCRUD.SubTitulo = "Alterar Tipo de Operação";
                objCRUD.Operacao = Opcoes.Update;
            }
            else if (opcoes == Opcoes.Delete)
            {
                objCRUD.Titulo = "Excluir Tipo de Operação";
                objCRUD.Descricao = "CUIDADO ao Excluir um Tipo de Operação, Este processo é irreversivel";
                objCRUD.SubTitulo = "Excluir Tipo de Operação";
                objCRUD.Operacao = Opcoes.Delete;
            }
            else if (opcoes == Opcoes.Read)
            {
                objCRUD.Titulo = "Consultar Tipo de Operação";
                objCRUD.Descricao = "Aqui você poderá consultar seu Cadastro de Tipo de Operação";
                objCRUD.SubTitulo = "Consultar Tipo de Operação";
                objCRUD.Operacao = Opcoes.Read;
            }

            return objCRUD;
        }



    }
}
