using API.Biblioteca.JWT;
using Dominio.DTO;
using Dominio.Entidades;
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


        [HttpGet]
        public async Task<ActionResult<TokenUsuario>> Login(Login login)
        {
            var Objeto = await _UOW.Banco.PesquisarPorIdAsync(Id);
            if (Objeto == null)
            {
                return NotFound(Mensagens.MSG_E002);
            }


            var ObjetoDTO = BancoDTO.ToDTO(Objeto);

            return Ok(ObjetoDTO);

        }


        

    }
}
