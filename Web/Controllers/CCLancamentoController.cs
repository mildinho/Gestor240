using Dominio.DTO;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Web.Biblioteca.CRUD;
using Web.Biblioteca.Filtro;
using Web.Biblioteca.msgDefault;
using Web.Biblioteca.Notification;
using Web.Models;

namespace Web.Controllers
{
    [LoginAutorizacao]
    public class CCLancamentoController : _BaseController<CCLancamentoController>
    {

        public CCLancamentoController()
        {

        }

        public async Task<IActionResult> Index()
        {
            return await Index_Geral<PagadorDTO>("Pagador/GetAll", "Index");

        }


        [HttpGet]
        public async Task<IActionResult> Cadastrar(int Id)
        {
            ExecutaAPI.TokenBearer = UsuarioLogado.GetToken().Token;
            ViewBag.CRUD = ConfiguraMensagem(Opcoes.Create);

            ViewBag.TContaCorrente = await ListaTipoContaCorrente();
            ViewBag.Token = ExecutaAPI.TokenBearer;


            ExecutaAPI.ParametrosAPI.Add(Id.ToString());

            var retornoApi = await ExecutaAPI.GetAPI("Pagador/GetbyId");
            PagadorDTO objRetorno = JsonConvert.DeserializeObject<PagadorDTO>(retornoApi.data);

            CCViewModel CVM = new CCViewModel
            {
                Pagador = new PagadorVM
                {
                    CNPJ_CPF = objRetorno.CNPJ_CPF,
                    Nome = objRetorno.Nome,
                    Fantasia = objRetorno.Fantasia,
                    Id = objRetorno.Id
                },

                ContaCorrenteDTO = new ContaCorrenteDTO(),
                ListaCCDTO = new List<ContaCorrenteDTO>()

            };


            return View("Manutencao", CVM);
        }


        private static CRUD ConfiguraMensagem(Opcoes opcoes)
        {
            CRUD objCRUD = new();

            objCRUD.Titulo = "Movimentação da Conta Corrente";
            objCRUD.Descricao = "Aqui você poderá configurar seu o Movimento da Conta Corrente";
            objCRUD.SubTitulo = "Movimentação da Conta Corrente";
            objCRUD.Operacao = Opcoes.Create;

            return objCRUD;
        }



    }
}
