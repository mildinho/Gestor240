using Dominio.DTO;
using Dominio.Entidades;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using Web.Biblioteca.msgDefault;
using Web.Biblioteca.Notification;
using Web.Biblioteca.Session;
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

            var retornoApi = await ExecutaAPI.PostAPI("Usuario/Login", login);

            if (ModelState.IsValid)
            {
                if (retornoApi.success)
                {
                    TokenUsuario obj = JsonConvert.DeserializeObject<TokenUsuario>(retornoApi.data);

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
    }
}