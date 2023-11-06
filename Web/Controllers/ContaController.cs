using Dominio.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using Web.Biblioteca.CRUD;
using Web.Biblioteca.msgDefault;
using Web.Biblioteca.Notification;
using Web.Services;


namespace Web.Controllers
{
    public class ContaController : _BaseController<ContaController>
    {

        public ContaController()
        {

        }

        public async Task<IActionResult> Index()
        {
            var retornoApi = await ExecutaAPI.GetAPI("Conta/GetAll");
            var objRetorno = JsonConvert.DeserializeObject<List<ContaDTO>>(retornoApi.data);

            return View(objRetorno);
        }

        [HttpGet]
        public async Task<IActionResult> Cadastrar()
        {
            ViewBag.CRUD = ConfiguraMensagem(Opcoes.Create);
            ViewBag.Bancos = await ListaBancos();

            return View("Manutencao");
        }



        [HttpGet]
        public async Task<IActionResult> Editar(int Id)
        {

            ViewBag.CRUD = ConfiguraMensagem(Opcoes.Update);
            ViewBag.Bancos = await ListaBancos();


            ExecutaAPI.ParametrosAPI.Add(Id.ToString());

            var retornoApi = await ExecutaAPI.GetAPI("Conta/GetbyId");
            var objRetorno = JsonConvert.DeserializeObject<ContaDTO>(retornoApi.data);

            return View("Manutencao", objRetorno);
        }


        [HttpGet]
        public async Task<IActionResult> Consultar(int Id)
        {
            ViewBag.CRUD = ConfiguraMensagem(Opcoes.Read);
            ViewBag.Bancos = await ListaBancos();

            ExecutaAPI.ParametrosAPI.Add(Id.ToString());

            var retornoApi = await ExecutaAPI.GetAPI("Conta/GetbyId");
            if (retornoApi.success)
            {
                var objRetorno = JsonConvert.DeserializeObject<ContaDTO>(retornoApi.data);

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
            ViewBag.Bancos = await ListaBancos();

            ExecutaAPI.ParametrosAPI.Add(Id.ToString());

            var retornoApi = await ExecutaAPI.GetAPI("Conta/GetbyId");
            if (retornoApi.success)
            {
                var objRetorno = JsonConvert.DeserializeObject<ContaDTO>(retornoApi.data);

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
        public async Task<IActionResult> Manutencao([FromForm] ContaDTO conta, Opcoes operacao)
        {
            ViewBag.Bancos = await ListaBancos();
            if (Opcoes.Delete == (Opcoes)operacao)
            {

                ExecutaAPI.ParametrosAPI.Add(conta.Id.ToString());

                var retornoApi = await ExecutaAPI.DeleteAPI("Conta");
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
                    var retornoApi = await ExecutaAPI.PostAPI("Conta", conta);


                    if (retornoApi.success)
                    {
                        AlertNotification.Success(mensagens.MSG_S001);
                    }
                    else
                    {
                        ViewBag.CRUD = ConfiguraMensagem(Opcoes.Create);


                        AlertNotification.Error(retornoApi.data);
              
                        return View("Manutencao", conta);
                    }
                }
                else if (Opcoes.Update == (Opcoes)operacao)
                {
                    ExecutaAPI.ParametrosAPI.Add(conta.Id.ToString());

                    var retornoApi = await ExecutaAPI.PutAPI("Conta", conta);
                    if (retornoApi.success)
                    {
                        AlertNotification.Success(mensagens.MSG_S002);
                    }
                    else
                    {
                        ViewBag.CRUD = ConfiguraMensagem(Opcoes.Update);


                        AlertNotification.Error(retornoApi.data);
                      

                        return View("Manutencao", conta);
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
                objCRUD.Titulo = "Agência";
                objCRUD.Descricao = "Aqui você poderá configurar sua Agência";
                objCRUD.SubTitulo = "Dados para Controlar sua Agência";
                objCRUD.Operacao = Opcoes.Information;

            }
            else if (opcoes == Opcoes.Create)
            {
                objCRUD.Titulo = "Incluir Agência Financeira";
                objCRUD.Descricao = "Aqui você poderá configurar seu Cadastro de Agência Financeira";
                objCRUD.SubTitulo = "Inserir Nova Agência";
                objCRUD.Operacao = Opcoes.Create;
            }
            else if (opcoes == Opcoes.Update)
            {
                objCRUD.Titulo = "Alterar Agência Financeira";
                objCRUD.Descricao = "Aqui você poderá configurar seu Cadastro de Agência Financeira";
                objCRUD.SubTitulo = "Alterar Agência Financeira";
                objCRUD.Operacao = Opcoes.Update;
            }
            else if (opcoes == Opcoes.Delete)
            {
                objCRUD.Titulo = "Excluir Agência Financeira";
                objCRUD.Descricao = "CUIDADO ao Excluir uma Agência, Este processo é irreversivel";
                objCRUD.SubTitulo = "Excluir Agência Financeira";
                objCRUD.Operacao = Opcoes.Delete;
            }
            else if (opcoes == Opcoes.Read)
            {
                objCRUD.Titulo = "Consultar Agência Financeira";
                objCRUD.Descricao = "Aqui você poderá consultar sua Agência Financeira";
                objCRUD.SubTitulo = "Consultar Agência Financeira";
                objCRUD.Operacao = Opcoes.Read;
            }

            return objCRUD;
        }




        private async Task<IEnumerable<SelectListItem>> ListaBancos()
        {
            var retornoApi = await ExecutaAPI.GetAPI("Banco/GetAll");
            List<BancoDTO> objRetorno = JsonConvert.DeserializeObject<List<BancoDTO>>(retornoApi.data);

            ViewBag.Bancos = objRetorno.Select(a => new SelectListItem(a.Codigo.ToString() + " - " + a.Nome, a.Id.ToString()));

            return ViewBag.Bancos;
        }

    }
}
