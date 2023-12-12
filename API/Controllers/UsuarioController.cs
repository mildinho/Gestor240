using API.Biblioteca.JWT;
using Dominio.DTO;
using Dominio.Entidades;
using Dominio.Interfaces;
using Microsoft.AspNetCore.Http.Features;
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
        public async Task<ActionResult<TokenUsuarioDTO>> Login(LoginDTO login)
        {
            login.Password = _UOW.Login.Criptografar(login.Password);

            Login Objeto = await _UOW.Login.PesquisarPorEmailSenhaAsync(login.Email, login.Password);
            if (Objeto == null)
            {
                return NotFound(Mensagens.MSG_E002);
            }


            TokenUsuarioDTO tokenUsuario = TokenService.Generate(login);
            tokenUsuario.Nome = Objeto.Nome.Trim();
            tokenUsuario.PrimeiroNome = tokenUsuario.Nome;

            if (tokenUsuario.Nome.IndexOf(' ') > 0)
                tokenUsuario.PrimeiroNome = tokenUsuario.Nome.Substring(0, tokenUsuario.Nome.IndexOf(' '));

            string remoteIpAddress = HttpContext.Connection.RemoteIpAddress?.ToString();

            LoginHistorico loginHistorico = new LoginHistorico
            {
                Data = DateTime.Now,
                EMail = tokenUsuario.Email,
                IP = remoteIpAddress
            };
            

            _UOW.LoginHistorico.InserirAsync(loginHistorico);
            _UOW.SaveAsync();

            return Ok(tokenUsuario);

        }



        [HttpPost("Registrar")]
        public async Task<ActionResult<bool>> Cadastrar(LoginRegistroDTO login)
        {
            var ObjetoLista = await _UOW.Login.PesquisarPorEmailAsync(login.Email);
            if (ObjetoLista.Any())
            {
                return BadRequest(Mensagens.MSG_E004);
            }

            if (ModelState.IsValid)
            {
                login.Password = _UOW.Login.Criptografar(login.Password);

                var ObjetoEntitade = LoginRegistroDTO.ToEntidade(login);
                Login Objeto = await _UOW.Login.InserirAsync(ObjetoEntitade);

                await _UOW.SaveAsync();

                return true;
            }
            return BadRequest();



        }


    }
}
