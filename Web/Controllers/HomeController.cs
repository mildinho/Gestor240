using Dominio.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Rotativa.AspNetCore;
using System.Diagnostics;
using Web.Biblioteca.msgDefault;
using Web.Biblioteca.Notification;
using Web.Interface;
using Web.Models;

namespace Web.Controllers
{
    public class HomeController : _BaseController<HomeController>
    {

        public HomeController()
        {

        }

        public IActionResult Index()
        {
            DashBoard dashBoard = new();
            return View(dashBoard);
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login( LoginDTO login)
        {

            if (ModelState.IsValid)
            {
                var retornoApi = await ExecutaAPI.PostAPI("Usuario/Login", login);

                if (retornoApi.success)
                {
                    TokenUsuarioDTO obj = JsonConvert.DeserializeObject<TokenUsuarioDTO>(retornoApi.data);

                    UsuarioLogado.GravaToken(obj);

                    //AlertNotification.Success("É bom tê-lo de volta, " + obj.Nome);

                    return Ok(obj);
                }
                else
                {
                    //AlertNotification.Info(mensagens.MSG_E001);
                    return NotFound(retornoApi.data);
                    //return View("Login", login);
                }
            }

            return View("Login", login);

        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult ErrorAPI(API_Retorno api_Retorno)
        {
            return View(
                new ErrorViewModel
                {
                    RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier,
                    Message = api_Retorno.statuscode.ToString() + " => " + api_Retorno.message + "<br>" + api_Retorno.data.ToString()
                }
                );
        }


        [Route("/PageNotFound")]
        public IActionResult PageNotFound()
        {
            return View();
        }


        [AllowAnonymous]
        public IActionResult Logout()
        {
            UsuarioLogado.Logout();
            return RedirectToAction("Index", "Home");

        }

        [AllowAnonymous]
        public IActionResult AddUser()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddUser([FromForm] LoginRegistroDTO addUser)
        {

            if (ModelState.IsValid)
            {
                var retornoApi = await ExecutaAPI.PostAPI("Usuario/Registrar", addUser);

                if (retornoApi.success)
                {
                    AlertNotification.Success("Obrigado pelo seu registro " + addUser.Nome);

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    AlertNotification.Error(mensagens.MSG_E002);
                    return View("AddUser", addUser);
                }
            }

            return View("AddUser", addUser);

        }



        [HttpGet]
        public async Task<IActionResult> Profile()
        {

            ExecutaAPI.ParametrosAPI.Add(UsuarioLogado.GetToken().Id.ToString());

            return await Editar_Geral<LoginRegistroDTO>("Usuario/GetbyId", "Profile");
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Profile([FromForm] LoginRegistroDTO profileUser)
        {

            if (ModelState.IsValid)
            {
                ExecutaAPI.ParametrosAPI.Add(profileUser.Id.ToString());
                var retornoApi = await ExecutaAPI.PutAPI("Usuario/Profile", profileUser);

                if (retornoApi.success)
                {

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    AlertNotification.Error(mensagens.MSG_E002);
                    return View("Profile", profileUser);
                }
            }

            return View("Profile", profileUser);

        }


        [HttpGet]
        public async Task<IActionResult> Relatorio_LogUsuario()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Relatorio_LogUsuario(DateTime Inicio, DateTime Fim)
        {
            ExecutaAPI.ParametrosAPI.Clear();
            ExecutaAPI.ParametrosAPI.Add(Inicio.ToString("yyyy-MM-dd") + " 00:00:00");
            ExecutaAPI.ParametrosAPI.Add(Fim.ToString("yyyy-MM-dd") + " 23:59:59");
            var retornoApi = await ExecutaAPI.GetAPI("Usuario/LogbyDate");


            if (retornoApi.success)
            {
                string footer = "--footer-center \"Impresso em: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm") + "  Pág.: [page]/[toPage]\"" + " --footer-line --footer-font-size \"12\" --footer-spacing 1 --footer-font-name \"calibri light\"";

                var objRetorno = JsonConvert.DeserializeObject<List<LoginHistoricoDTO>>(retornoApi.data);
                return new ViewAsPdf("pdf_Relatorio01", objRetorno)
                {
                    FileName = "Atividades.pdf",
                    PageOrientation = Rotativa.AspNetCore.Options.Orientation.Portrait,
                    PageSize = Rotativa.AspNetCore.Options.Size.A4,
                    CustomSwitches = footer
                };

            }
            else
            {
                AlertNotification.Error("Não Há Registro de Atividades");
                return RedirectToAction("Index", "Home");

            }
        }


    }
}