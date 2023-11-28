using API.Biblioteca.JWT;
using Dominio.DTO;
using Dominio.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("v1/[controller]")]
    public class UsuarioController : Controller
    {
        private readonly IUnitOfWork _UOW;
        public UsuarioController(IUnitOfWork unitOfWork)
        {
            _UOW = unitOfWork;
        }


        [HttpPost("Login")]
        public async Task<ActionResult<TokenUsuario>> Login(LoginDTO login)
        {
            var Objeto = await _UOW.Login.PesquisarPorEmailSenhaAsync(login.Email, login.Password);
            if (Objeto == null || Objeto.Count() == 0)
            {
                return NotFound(Mensagens.MSG_E002);
            }


            TokenUsuario tokenUsuario = TokenService.Generate(login);
           

            return Ok(tokenUsuario);

        }



        [HttpPost("Registrar")]
        public async Task<ActionResult<TokenUsuario>> Cadastrar(LoginDTO login)
        {
            var Objeto = await _UOW.Login.PesquisarPorEmailAsync(login.Email);
            if (Objeto == null || Objeto.Count() == 0)
            {
                return NotFound(Mensagens.MSG_E002);
            }


            TokenUsuario tokenUsuario = TokenService.Generate(login);


            return Ok(tokenUsuario);

        }


    }
}
