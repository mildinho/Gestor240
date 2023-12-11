using Dominio.DTO;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Web.Biblioteca.CRUD;
using Web.Biblioteca.Filtro;
using Web.Biblioteca.msgDefault;
using Web.Biblioteca.Notification;

namespace Web.Controllers
{
    [LoginAutorizacao]
    public class TipoContaCorrenteController : _BaseController<TipoContaCorrenteController>
    {

        public TipoContaCorrenteController()
        {

        }

        public async Task<IActionResult> Index()
        {
            return await Index_Geral<TipoContaCorrenteDTO>("TipoContaCorrente/GetAll", "Index");
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

            return await Editar_Geral<TipoContaCorrenteDTO>("TipoContacorrente/GetbyId", "Manutencao");
        }


        [HttpGet]
        public async Task<IActionResult> Consultar(int Id)
        {
            ViewBag.CRUD = ConfiguraMensagem(Opcoes.Read);
            ExecutaAPI.ParametrosAPI.Add(Id.ToString());

            var retornoApi = await ExecutaAPI.GetAPI("TipoContaCorrente/GetbyId");
            if (retornoApi.success)
            {
                var objRetorno = JsonConvert.DeserializeObject<TipoContaCorrenteDTO>(retornoApi.data);
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

            var retornoApi = await ExecutaAPI.GetAPI("TipoContaCorrente/GetbyId");
            if (retornoApi.success)
            {
                var objRetorno = JsonConvert.DeserializeObject<TipoContaCorrenteDTO>(retornoApi.data);
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
        public async Task<IActionResult> Manutencao([FromForm] TipoContaCorrenteDTO tipoCC, Opcoes operacao)
        {
            if (Opcoes.Delete == (Opcoes)operacao)
            {


                ExecutaAPI.ParametrosAPI.Add(tipoCC.Id.ToString());

                var retornoApi = await ExecutaAPI.DeleteAPI("TipoContaCorrente");
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
                    var retornoApi = await ExecutaAPI.PostAPI("TipoContaCorrente", tipoCC);


                    if (retornoApi.success)
                    {
                        AlertNotification.Success(mensagens.MSG_S001);
                    }
                    else
                    {
                        ViewBag.CRUD = ConfiguraMensagem(Opcoes.Create);


                        AlertNotification.Error(retornoApi.data);

                        return View("Manutencao", tipoCC);
                    }
                }
                else if (Opcoes.Update == (Opcoes)operacao)
                {
                    ExecutaAPI.ParametrosAPI.Add(tipoCC.Id.ToString());

                    var retornoApi = await ExecutaAPI.PutAPI("TipoContaCorrente", tipoCC);
                    if (retornoApi.success)
                    {
                        AlertNotification.Success(mensagens.MSG_S002);
                    }
                    else
                    {
                        ViewBag.CRUD = ConfiguraMensagem(Opcoes.Update);


                        AlertNotification.Error(retornoApi.data);

                        return View("Manutencao", tipoCC);
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
                objCRUD.Titulo = "Tipo Conta Corrente";
                objCRUD.Descricao = "Aqui você poderá configurar o Conta de Corrente";
                objCRUD.SubTitulo = "Dados para Controlar as Contas Correntes";
                objCRUD.Operacao = Opcoes.Information;

            }
            else if (opcoes == Opcoes.Create)
            {
                objCRUD.Titulo = "Incluir Conta Corrente";
                objCRUD.Descricao = "Aqui você poderá configurar o Conta de Corrente";
                objCRUD.SubTitulo = "Inserir Novo Conta de Corrente";
                objCRUD.Operacao = Opcoes.Create;
            }
            else if (opcoes == Opcoes.Update)
            {
                objCRUD.Titulo = "Alterar Conta Corrente";
                objCRUD.Descricao = "Aqui você poderá configurar seu Cadastro de Conta de Corrente";
                objCRUD.SubTitulo = "Alterar Conta de Corrente";
                objCRUD.Operacao = Opcoes.Update;
            }
            else if (opcoes == Opcoes.Delete)
            {
                objCRUD.Titulo = "Excluir Conta Corrente";
                objCRUD.Descricao = "CUIDADO ao Excluir um Conta de Corrente, Este processo é irreversivel";
                objCRUD.SubTitulo = "Excluir Conta Corrente";
                objCRUD.Operacao = Opcoes.Delete;
            }
            else if (opcoes == Opcoes.Read)
            {
                objCRUD.Titulo = "Consultar Conta Corrente";
                objCRUD.Descricao = "Aqui você poderá consultar seu Cadastro de Conta de Corrente";
                objCRUD.SubTitulo = "Consultar Conta de Corrente";
                objCRUD.Operacao = Opcoes.Read;
            }

            return objCRUD;
        }



    }
}
