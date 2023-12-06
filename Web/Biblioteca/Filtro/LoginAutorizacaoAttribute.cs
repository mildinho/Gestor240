using Dominio.Entidades;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Web.Biblioteca.Session;
using Dominio.DTO;

namespace Web.Biblioteca.Filtro
{
    public class LoginAutorizacaoAttribute : Attribute, IAuthorizationFilter
    {
        SessaoUsuario _loginUsuario;
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            _loginUsuario = (SessaoUsuario)context.HttpContext.RequestServices.GetService(typeof(SessaoUsuario));

            
            //TokenUsuario obj = _loginUsuario.GetToken();
            
            //if (obj == null)
            //{
            //    context.Result = new RedirectToActionResult("Login", "Home", null);
            //}
            
            

         }
    }
}
