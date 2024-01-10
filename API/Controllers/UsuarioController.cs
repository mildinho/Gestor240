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



        [HttpGet("GetbyId/{Id}")]
        public async Task<ActionResult<LoginRegistroDTO>> GetbyId(int Id)
        {
            var Objeto = await _UOW.Login.PesquisarPorIdAsync(Id);
            if (Objeto == null)
            {
                return NotFound(Mensagens.MSG_E002);
            }
            var ObjetoDTO = LoginRegistroDTO.ToDTO(Objeto);
            ObjetoDTO.Password = _UOW.Login.DesCriptografar(ObjetoDTO.Password);

            return Ok(ObjetoDTO);
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
            tokenUsuario.Id = Objeto.Id.ToString();
            tokenUsuario.Nome = Objeto.Nome.Trim();
            tokenUsuario.PrimeiroNome = tokenUsuario.Nome;


            if (tokenUsuario.Nome.IndexOf(' ') > 0)
                tokenUsuario.PrimeiroNome = tokenUsuario.Nome.Substring(0, tokenUsuario.Nome.IndexOf(' '));

            string remoteIpAddress = HttpContext.Connection.RemoteIpAddress?.ToString();

            LoginHistorico loginHistorico = new LoginHistorico
            {
                Data = DateTime.Now,
                EMail = tokenUsuario.Email,
                IP = remoteIpAddress,
                Nome = tokenUsuario.Nome,
                IdUsuario = tokenUsuario.Id
            };


            _UOW.LoginHistorico.InserirAsync(loginHistorico);
            await _UOW.SaveAsync();

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
                login.Id = 0;
                login.Password = _UOW.Login.Criptografar(login.Password);

                var ObjetoEntitade = LoginRegistroDTO.ToEntidade(login);
                Login Objeto = await _UOW.Login.InserirAsync(ObjetoEntitade);

                await _UOW.SaveAsync();

                return true;
            }
            return BadRequest();



        }

        [HttpPut("Profile/{Id}")]
        public async Task<ActionResult<bool>> Profile(int Id, LoginRegistroDTO tabela)
        {
            if (Id != tabela.Id)
                return BadRequest(Mensagens.MSG_E001);


            Login ObjetoPesquisa = await _UOW.Login.PesquisarPorIdAsync(tabela.Id);
            if (ObjetoPesquisa == null)
            {
                return BadRequest(Mensagens.MSG_E002);
            }


            if (ModelState.IsValid)
            {
                tabela.Password = _UOW.Login.Criptografar(tabela.Password);
                
                var ObjetoEntitade = LoginRegistroDTO.ToEntidade(tabela);


                Login Objeto = await _UOW.Login.AtualizarAsync(ObjetoEntitade);

                await _UOW.SaveAsync();

                return Ok(true);

            }
            return BadRequest();

        }


        [HttpGet("LogbyDate/{dataInicial}/{dataFinal}")]
        public async Task<ActionResult<LoginRegistroDTO>> LogbyDate(DateTime dataInicial, DateTime dataFinal)
        {
            var Objeto = await _UOW.LoginHistorico.PesquisarPorDataAsync(dataInicial, dataFinal);
            if (Objeto == null)
            {
                return NotFound(Mensagens.MSG_E002);
            }
            var ObjetoDTO = LoginHistoricoDTO.ToDTO(Objeto);

            return Ok(ObjetoDTO);
        }


    }
}
