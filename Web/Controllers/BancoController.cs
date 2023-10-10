using Dominio.DTO;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Web.Biblioteca.CRUD;
using Web.Biblioteca.msgDefault;
using Web.Biblioteca.Notification;
using Web.Services;

namespace Web.Controllers
{
    public class BancoController : Controller
    {

        private readonly IntegracaoApi _integracaoApi;

        public BancoController(IntegracaoApi integracaoApi)
        {
            _integracaoApi = integracaoApi;

        }

        public async Task<IActionResult> Index()
        {
            var retornoApi = await _integracaoApi.GetAPI("Banco/GetAll");
            var objRetorno = JsonConvert.DeserializeObject<List<BancoDTO>>(retornoApi.data);

            return View(objRetorno);
        }

        [HttpGet]
        public async Task<IActionResult> Cadastrar()
        {
            ViewBag.CRUD = ConfiguraMensagem(Opcoes.Create);

            return View("Manutencao");
        }



        [HttpGet]
        public async Task<IActionResult> Editar(int Id)
        {
            List<string> parametros = new();
            parametros.Add(Id.ToString());


            ViewBag.CRUD = ConfiguraMensagem(Opcoes.Update);
            _integracaoApi.ParametrosAPI = parametros;

            var retornoApi = await _integracaoApi.GetAPI("Banco/GetbyId");
            var objRetorno = JsonConvert.DeserializeObject<BancoDTO>(retornoApi.data);



            return View("Manutencao", objRetorno);
        }


        [HttpGet]
        public async Task<IActionResult> Consultar(int Id)
        {
            List<string> parametros = new();
            parametros.Add(Id.ToString());


            ViewBag.CRUD = ConfiguraMensagem(Opcoes.Read);
            _integracaoApi.ParametrosAPI = parametros;

            var retornoApi = await _integracaoApi.GetAPI("Banco/GetbyId");
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


        [HttpGet]
        public async Task<IActionResult> Deletar(int Id)
        {
            List<string> parametros = new();
            parametros.Add(Id.ToString());


            ViewBag.CRUD = ConfiguraMensagem(Opcoes.Delete);
            _integracaoApi.ParametrosAPI = parametros;

            var retornoApi = await _integracaoApi.GetAPI("Banco/GetbyId");
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
            List<string> parametros = new();



            if (Opcoes.Delete == (Opcoes)operacao)
            {

                parametros.Add(banco.Id.ToString());
                _integracaoApi.ParametrosAPI = parametros;

                var retornoApi = await _integracaoApi.DeleteAPI("Banco");
                if (retornoApi.success)
                {
                    AlertNotification.Success(mensagens.MSG_S002);
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
                    var retornoApi = await _integracaoApi.PostAPI("Banco", banco);


                    if (retornoApi.success)
                    {
                        AlertNotification.Success(mensagens.MSG_S001);
                    }
                    else
                    {
                        AlertNotification.Error(retornoApi.data);
                    }
                }
                else if (Opcoes.Update == (Opcoes)operacao)
                {
                    parametros.Add(banco.Id.ToString());
                    _integracaoApi.ParametrosAPI = parametros;

                    var retornoApi = await _integracaoApi.PutAPI("Banco", banco);
                    if (retornoApi.success)
                    {
                        AlertNotification.Success(mensagens.MSG_S002);
                    }
                    else
                    {
                        AlertNotification.Error(retornoApi.data);
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



        [Route("/PageNotFound")]
        public IActionResult PageNotFound()
        {
            return View();
        }
    }
}
