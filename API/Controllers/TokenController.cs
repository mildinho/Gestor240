using API.Biblioteca.JWT;
using Dominio.DTO;
using Dominio.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("v1/[controller]")]
    public class TokenController : Controller
    {
        private readonly IUnitOfWork _UOW;
        public TokenController(IUnitOfWork unitOfWork)
        {
            _UOW = unitOfWork;
        }


        [HttpPost]
        public async Task<ActionResult<TokenUsuario>> Login(LoginDTO login)
        {
            var Objeto = await _UOW.Login.PesquisarPorEmailAsync(login.Email, login.Password);
            if (Objeto == null)
            {
                return NotFound(Mensagens.MSG_E002);
            }


            var ObjetoDTO = LoginDTO.ToDTO(Objeto);

            return Ok(ObjetoDTO);

        }


        

    }
}
