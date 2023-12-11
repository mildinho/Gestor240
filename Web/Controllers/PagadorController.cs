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
    public class PagadorController : _BaseController<PagadorController>
    {

        public PagadorController()
        {

        }

        public async Task<IActionResult> Index()
        {
            return await Index_Geral<PagadorDTO>("Pagador/GetAll", "Index");
        }

        [HttpGet]
        public async Task<IActionResult> Cadastrar()
        {
            ViewBag.CRUD = ConfiguraMensagem(Opcoes.Create);

            ViewBag.UF = await ListaUF();
            ViewBag.TipoInscricaoEmpresa = await ListaTipoInscricaoEmpresa();

            return View("Manutencao");
        }



        [HttpGet]
        public async Task<IActionResult> Editar(int Id)
        {
            ViewBag.CRUD = ConfiguraMensagem(Opcoes.Update);

            ViewBag.UF = await ListaUF();
            ViewBag.TipoInscricaoEmpresa = await ListaTipoInscricaoEmpresa();

            ExecutaAPI.ParametrosAPI.Add(Id.ToString());

            var retornoApi = await ExecutaAPI.GetAPI("Pagador/GetbyId");
            var objRetorno = JsonConvert.DeserializeObject<PagadorDTO>(retornoApi.data);

            return View("Manutencao", objRetorno);
        }


        [HttpGet]
        public async Task<IActionResult> Consultar(int Id)
        {
            ViewBag.CRUD = ConfiguraMensagem(Opcoes.Read);
            ViewBag.UF = await ListaUF();
            ViewBag.TipoInscricaoEmpresa = await ListaTipoInscricaoEmpresa();

            ExecutaAPI.ParametrosAPI.Add(Id.ToString());

            var retornoApi = await ExecutaAPI.GetAPI("Pagador/GetbyId");
            if (retornoApi.success)
            {
                var objRetorno = JsonConvert.DeserializeObject<PagadorDTO>(retornoApi.data);

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
            ViewBag.TipoInscricaoEmpresa = await ListaTipoInscricaoEmpresa();

            ExecutaAPI.ParametrosAPI.Add(Id.ToString());

            var retornoApi = await ExecutaAPI.GetAPI("Pagador/GetbyId");
            if (retornoApi.success)
            {
                var objRetorno = JsonConvert.DeserializeObject<PagadorDTO>(retornoApi.data);

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
        public async Task<IActionResult> Manutencao([FromForm] PagadorDTO pagador, Opcoes operacao)
        {
            ViewBag.UF = await ListaUF();
            ViewBag.TipoInscricaoEmpresa = await ListaTipoInscricaoEmpresa();

            if (pagador.UFId > 0)
                ViewBag.Municipio = await ListaMunicipioPorIdUF(pagador.UFId);

            if (Opcoes.Delete == (Opcoes)operacao)
            {

                ExecutaAPI.ParametrosAPI.Add(pagador.Id.ToString());

                var retornoApi = await ExecutaAPI.DeleteAPI("Pagador");
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
                    var retornoApi = await ExecutaAPI.PostAPI("Pagador", pagador);


                    if (retornoApi.success)
                    {
                        AlertNotification.Success(mensagens.MSG_S001);
                    }
                    else
                    {
                        ViewBag.CRUD = ConfiguraMensagem(Opcoes.Create);


                        AlertNotification.Error(retornoApi.data);
              
                        return View("Manutencao", pagador);
                    }
                }
                else if (Opcoes.Update == (Opcoes)operacao)
                {
                    ExecutaAPI.ParametrosAPI.Add(pagador.Id.ToString());

                    var retornoApi = await ExecutaAPI.PutAPI("Pagador", pagador);
                    if (retornoApi.success)
                    {
                        AlertNotification.Success(mensagens.MSG_S002);
                    }
                    else
                    {
                        ViewBag.CRUD = ConfiguraMensagem(Opcoes.Update);


                        AlertNotification.Error(retornoApi.data);
                      

                        return View("Manutencao", pagador);
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
                objCRUD.Titulo = "Pagador";
                objCRUD.Descricao = "Aqui você poderá configurar o Pagador";
                objCRUD.SubTitulo = "Dados para Controlar o Pagador";
                objCRUD.Operacao = Opcoes.Information;

            }
            else if (opcoes == Opcoes.Create)
            {
                objCRUD.Titulo = "Incluir Pagador";
                objCRUD.Descricao = "Aqui você poderá configurar o Cadastro de Pagador";
                objCRUD.SubTitulo = "Inserir Novo Pagador";
                objCRUD.Operacao = Opcoes.Create;
            }
            else if (opcoes == Opcoes.Update)
            {
                objCRUD.Titulo = "Alterar Pagador";
                objCRUD.Descricao = "Aqui você poderá configurar seu Cadastro de Pagador";
                objCRUD.SubTitulo = "Alterar Pagador";
                objCRUD.Operacao = Opcoes.Update;
            }
            else if (opcoes == Opcoes.Delete)
            {
                objCRUD.Titulo = "Excluir Pagador";
                objCRUD.Descricao = "CUIDADO ao Excluir um Pagador, Este processo é irreversivel";
                objCRUD.SubTitulo = "Excluir Pagador";
                objCRUD.Operacao = Opcoes.Delete;
            }
            else if (opcoes == Opcoes.Read)
            {
                objCRUD.Titulo = "Consultar Pagador";
                objCRUD.Descricao = "Aqui você poderá consultar o Pagador";
                objCRUD.SubTitulo = "Consultar Pagador";
                objCRUD.Operacao = Opcoes.Read;
            }

            return objCRUD;
        }


    }
}
