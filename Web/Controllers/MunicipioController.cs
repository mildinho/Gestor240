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
    public class MunicipioController : _BaseController<MunicipioController>
    {

        public MunicipioController()
        {

        }

        public async Task<IActionResult> Index()
        {
            var retornoApi = await ExecutaAPI.GetAPI("Municipio/GetAll");
            var objRetorno = JsonConvert.DeserializeObject<List<MunicipioDTO>>(retornoApi.data);

            return View(objRetorno);
        }

        [HttpGet]
        public async Task<IActionResult> Cadastrar()
        {
            ViewBag.CRUD = ConfiguraMensagem(Opcoes.Create);
            ViewBag.UF = await ListaUF();

            return View("Manutencao");
        }



        [HttpGet]
        public async Task<IActionResult> Editar(int Id)
        {

            ViewBag.CRUD = ConfiguraMensagem(Opcoes.Update);
            ViewBag.UF = await ListaUF();


            ExecutaAPI.ParametrosAPI.Add(Id.ToString());

            var retornoApi = await ExecutaAPI.GetAPI("Municipio/GetbyId");
            var objRetorno = JsonConvert.DeserializeObject<MunicipioDTO>(retornoApi.data);

            return View("Manutencao", objRetorno);
        }


        [HttpGet]
        public async Task<IActionResult> Consultar(int Id)
        {
            ViewBag.CRUD = ConfiguraMensagem(Opcoes.Read);
            ViewBag.UF = await ListaUF();

            ExecutaAPI.ParametrosAPI.Add(Id.ToString());

            var retornoApi = await ExecutaAPI.GetAPI("Municipio/GetbyId");
            if (retornoApi.success)
            {
                var objRetorno = JsonConvert.DeserializeObject<MunicipioDTO>(retornoApi.data);

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
            ViewBag.UF = await ListaUF();

            ExecutaAPI.ParametrosAPI.Add(Id.ToString());

            var retornoApi = await ExecutaAPI.GetAPI("Municipio/GetbyId");
            if (retornoApi.success)
            {
                var objRetorno = JsonConvert.DeserializeObject<MunicipioDTO>(retornoApi.data);

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
        public async Task<IActionResult> Manutencao([FromForm] MunicipioDTO municipio, Opcoes operacao)
        {
            ViewBag.UF = await ListaUF();
            if (Opcoes.Delete == (Opcoes)operacao)
            {

                ExecutaAPI.ParametrosAPI.Add(municipio.Id.ToString());

                var retornoApi = await ExecutaAPI.DeleteAPI("Municipio");
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
                    var retornoApi = await ExecutaAPI.PostAPI("Municipio", municipio);


                    if (retornoApi.success)
                    {
                        AlertNotification.Success(mensagens.MSG_S001);
                    }
                    else
                    {
                        ViewBag.CRUD = ConfiguraMensagem(Opcoes.Create);


                        AlertNotification.Error(retornoApi.data);
              
                        return View("Manutencao", municipio);
                    }
                }
                else if (Opcoes.Update == (Opcoes)operacao)
                {
                    ExecutaAPI.ParametrosAPI.Add(municipio.Id.ToString());

                    var retornoApi = await ExecutaAPI.PutAPI("Municipio", municipio);
                    if (retornoApi.success)
                    {
                        AlertNotification.Success(mensagens.MSG_S002);
                    }
                    else
                    {
                        ViewBag.CRUD = ConfiguraMensagem(Opcoes.Update);


                        AlertNotification.Error(retornoApi.data);
                      

                        return View("Manutencao", municipio);
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




        private async Task<IEnumerable<SelectListItem>> ListaUF()
        {
            var retornoApi = await ExecutaAPI.GetAPI("UF/GetAll");
            List<UFDTO> objRetorno = JsonConvert.DeserializeObject<List<UFDTO>>(retornoApi.data);

            ViewBag.Bancos = objRetorno.Select(a => new SelectListItem(a.Sigla.ToString() + " - " + a.Descricao, a.Id.ToString()));

            return ViewBag.Bancos;
        }

    }
}
