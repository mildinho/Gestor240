using Dominio.DTO;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Web.Biblioteca.CRUD;
using Web.Biblioteca.Filtro;
using Web.Models;

namespace Web.Controllers
{
    [LoginAutorizacao]
    public class TituloLancamentoController : _BaseController<TituloLancamentoController>
    {

        public TituloLancamentoController()
        {

        }

        public async Task<IActionResult> Index()
        {
            return await Index_Geral<PagadorDTO>("Pagador/GetAll", "Index");

        }


        [HttpGet]
        public async Task<IActionResult> Cadastrar(int Id)
        {
            ViewBag.CRUD = ConfiguraMensagem(Opcoes.Create);
            ViewBag.TContaCorrente = await ListaTipoContaCorrente();


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

            objCRUD.Titulo = "Lançamento de Títulos ao Pagador";
            objCRUD.Operacao = Opcoes.Create;

            return objCRUD;
        }



    }
}
