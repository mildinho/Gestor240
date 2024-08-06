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
            return await Index_Geral<BeneficiarioDTO>("Beneficiario/GetAll", "Index");

        }


        [HttpGet]
        public async Task<IActionResult> Cadastrar(int Id)
        {
            ViewBag.CRUD = ConfiguraMensagem(Opcoes.Create);
            ViewBag.TContaCorrente = await ListaTipoContaCorrente();
            ViewBag.TFormaLancamento = await ListaFormaLancamento();
            ViewBag.TTipoServico = await ListaTipoServico();


            ExecutaAPI.ParametrosAPI.Add(Id.ToString());

            var retornoApi = await ExecutaAPI.GetAPI("Beneficiario/GetbyId");
            BeneficiarioDTO objRetorno01 = JsonConvert.DeserializeObject<BeneficiarioDTO>(retornoApi.data);


            ExecutaAPI.ParametrosAPI.Clear();
            var retornoApi02 = await ExecutaAPI.GetAPI("Pagador/GetAll");
            List<PagadorDTO> objRetorno02 = JsonConvert.DeserializeObject<List<PagadorDTO>>(retornoApi02.data);
      

            CCViewModel CVM = new CCViewModel
            {
                Beneficiario = new BeneficiarioVM
                {
                    CNPJ_CPF = objRetorno01.CNPJ_CPF,
                    Nome = objRetorno01.Nome,
                    Fantasia = objRetorno01.Fantasia,
                    Id = objRetorno01.Id
                },

                ContaCorrenteDTO = new ContaCorrenteDTO(),
                ListaCCDTO = new List<ContaCorrenteDTO>(),
                Pagador_Lista = objRetorno02

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
