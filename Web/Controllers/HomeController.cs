using Dominio.DTO;
using Dominio.Entidades;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using Web.Biblioteca.msgDefault;
using Web.Biblioteca.Notification;
using Web.Biblioteca.Session;
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
        public async Task<IActionResult> Login([FromForm] LoginDTO login)
        {

            if (ModelState.IsValid)
            {
                var retornoApi = await ExecutaAPI.PostAPI("Usuario/Login", login);

                if (retornoApi.success)
                {
                    TokenUsuarioDTO obj = JsonConvert.DeserializeObject<TokenUsuarioDTO>(retornoApi.data);

                    UsuarioLogado.GravaToken(obj);

                    AlertNotification.Success("É bom tê-lo de volta, " + obj.Nome);

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    AlertNotification.Info(mensagens.MSG_E001);
                    return View("Login", login);
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



        public IActionResult Logout()
        {
            UsuarioLogado.Logout();
            return RedirectToAction("Index", "Home");

        }

        [HttpGet]
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

    }
}