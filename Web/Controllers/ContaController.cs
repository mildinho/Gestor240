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
            ViewBag.Agencias = await ListaAgencias();

            return View("Manutencao");
        }



        [HttpGet]
        public async Task<IActionResult> Editar(int Id)
        {

            ViewBag.CRUD = ConfiguraMensagem(Opcoes.Update);
            ViewBag.Bancos = await ListaBancos();
            ViewBag.Agencias = await ListaAgencias();


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
            ViewBag.Agencias = await ListaAgencias();

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
            ViewBag.Agencias = await ListaAgencias();

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
                objCRUD.Titulo = "Conta";
                objCRUD.Descricao = "Aqui você poderá configurar sua Conta";
                objCRUD.SubTitulo = "Dados para Controlar sua Conta";
                objCRUD.Operacao = Opcoes.Information;

            }
            else if (opcoes == Opcoes.Create)
            {
                objCRUD.Titulo = "Incluir Conta Financeira";
                objCRUD.Descricao = "Aqui você poderá configurar seu Cadastro de Conta Financeira";
                objCRUD.SubTitulo = "Inserir Nova Conta";
                objCRUD.Operacao = Opcoes.Create;
            }
            else if (opcoes == Opcoes.Update)
            {
                objCRUD.Titulo = "Alterar Conta Financeira";
                objCRUD.Descricao = "Aqui você poderá configurar seu Cadastro de Conta Financeira";
                objCRUD.SubTitulo = "Alterar Conta Financeira";
                objCRUD.Operacao = Opcoes.Update;
            }
            else if (opcoes == Opcoes.Delete)
            {
                objCRUD.Titulo = "Excluir Conta Financeira";
                objCRUD.Descricao = "CUIDADO ao Excluir uma Conta, Este processo é irreversivel";
                objCRUD.SubTitulo = "Excluir Conta Financeira";
                objCRUD.Operacao = Opcoes.Delete;
            }
            else if (opcoes == Opcoes.Read)
            {
                objCRUD.Titulo = "Consultar Conta Financeira";
                objCRUD.Descricao = "Aqui você poderá consultar sua Conta Financeira";
                objCRUD.SubTitulo = "Consultar Conta Financeira";
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

        private async Task<IEnumerable<SelectListItem>> ListaAgencias()
        {
            var retornoApi = await ExecutaAPI.GetAPI("Agencia/GetAll");
            List<AgenciaDTO> objRetorno = JsonConvert.DeserializeObject<List<AgenciaDTO>>(retornoApi.data);

            ViewBag.Agencias = objRetorno.Select(a => new SelectListItem(a.NumeroAgencia.ToString() + " - " + a.DigitoAgencia + " - " + a.Nome, a.Id.ToString()));

            return ViewBag.Agencias;
        }

    }
}
