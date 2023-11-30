using Dominio.DTO;
using Dominio.Entidades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using Web.Biblioteca.CRUD;
using Web.Biblioteca.msgDefault;
using Web.Biblioteca.Notification;
using Web.Services;


namespace Web.Controllers
{
    public class BeneficiarioController : _BaseController<BeneficiarioController>
    {

        public BeneficiarioController()
        {

        }

        public async Task<IActionResult> Index()
        {
            var retornoApi = await ExecutaAPI.GetAPI("Beneficiario/GetAll");
            var objRetorno = JsonConvert.DeserializeObject<List<BeneficiarioDTO>>(retornoApi.data);

            return View(objRetorno);
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

            var retornoApi = await ExecutaAPI.GetAPI("Beneficiario/GetbyId");
            var objRetorno = JsonConvert.DeserializeObject<BeneficiarioDTO>(retornoApi.data);

            if (objRetorno.UFId > 0)
                ViewBag.Municipio = await ListaMunicipioPorIdUF(objRetorno.UFId);

            return View("Manutencao", objRetorno);
        }


        [HttpGet]
        public async Task<IActionResult> Consultar(int Id)
        {
            ViewBag.CRUD = ConfiguraMensagem(Opcoes.Read);
            ViewBag.UF = await ListaUF();
            ViewBag.TipoInscricaoEmpresa = await ListaTipoInscricaoEmpresa();

            ExecutaAPI.ParametrosAPI.Add(Id.ToString());

            var retornoApi = await ExecutaAPI.GetAPI("Beneficiario/GetbyId");
            if (retornoApi.success)
            {
                var objRetorno = JsonConvert.DeserializeObject<BeneficiarioDTO>(retornoApi.data);
                if (objRetorno.UFId > 0)
                    ViewBag.Municipio = await ListaMunicipioPorIdUF(objRetorno.UFId);

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

            var retornoApi = await ExecutaAPI.GetAPI("Beneficiario/GetbyId");
            if (retornoApi.success)
            {

                var objRetorno = JsonConvert.DeserializeObject<BeneficiarioDTO>(retornoApi.data);
                if (objRetorno.UFId > 0)
                    ViewBag.Municipio = await ListaMunicipioPorIdUF(objRetorno.UFId);

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
        public async Task<IActionResult> Manutencao([FromForm] BeneficiarioDTO beneficiario, Opcoes operacao)
        {
            ViewBag.UF = await ListaUF();
            ViewBag.TipoInscricaoEmpresa = await ListaTipoInscricaoEmpresa();
            
            if (beneficiario.UFId > 0)
                ViewBag.Municipio = await ListaMunicipioPorIdUF(beneficiario.UFId);

            if (Opcoes.Delete == (Opcoes)operacao)
            {

                ExecutaAPI.ParametrosAPI.Add(beneficiario.Id.ToString());

                var retornoApi = await ExecutaAPI.DeleteAPI("Beneficiario");
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
                    var retornoApi = await ExecutaAPI.PostAPI("Beneficiario", beneficiario);


                    if (retornoApi.success)
                    {
                        AlertNotification.Success(mensagens.MSG_S001);
                    }
                    else
                    {
                        ViewBag.CRUD = ConfiguraMensagem(Opcoes.Create);


                        AlertNotification.Error(retornoApi.data);
              
                        return View("Manutencao", beneficiario);
                    }
                }
                else if (Opcoes.Update == (Opcoes)operacao)
                {
                    ExecutaAPI.ParametrosAPI.Add(beneficiario.Id.ToString());

                    var retornoApi = await ExecutaAPI.PutAPI("Beneficiario", beneficiario);
                    if (retornoApi.success)
                    {
                        AlertNotification.Success(mensagens.MSG_S002);
                    }
                    else
                    {
                        ViewBag.CRUD = ConfiguraMensagem(Opcoes.Update);


                        AlertNotification.Error(retornoApi.data);
                      

                        return View("Manutencao", beneficiario);
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
                objCRUD.Titulo = "Beneficiário";
                objCRUD.Descricao = "Aqui você poderá configurar o Beneficiário";
                objCRUD.SubTitulo = "Dados para Controlar o Beneficiário";
                objCRUD.Operacao = Opcoes.Information;

            }
            else if (opcoes == Opcoes.Create)
            {
                objCRUD.Titulo = "Incluir Beneficiário";
                objCRUD.Descricao = "Aqui você poderá configurar o Cadastro de Beneficiário";
                objCRUD.SubTitulo = "Inserir Novo Beneficiário";
                objCRUD.Operacao = Opcoes.Create;
            }
            else if (opcoes == Opcoes.Update)
            {
                objCRUD.Titulo = "Alterar Beneficiário";
                objCRUD.Descricao = "Aqui você poderá configurar seu Cadastro de Beneficiário";
                objCRUD.SubTitulo = "Alterar Beneficiário";
                objCRUD.Operacao = Opcoes.Update;
            }
            else if (opcoes == Opcoes.Delete)
            {
                objCRUD.Titulo = "Excluir Beneficiário";
                objCRUD.Descricao = "CUIDADO ao Excluir um Beneficiário, Este processo é irreversivel";
                objCRUD.SubTitulo = "Excluir Beneficiário";
                objCRUD.Operacao = Opcoes.Delete;
            }
            else if (opcoes == Opcoes.Read)
            {
                objCRUD.Titulo = "Consultar Beneficiário";
                objCRUD.Descricao = "Aqui você poderá consultar o Beneficiário";
                objCRUD.SubTitulo = "Consultar Beneficiário";
                objCRUD.Operacao = Opcoes.Read;
            }

            return objCRUD;
        }


    }
}
