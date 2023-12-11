using Dominio.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using Web.Biblioteca.Session;
using Web.Interface;
using Web.Services;

namespace Web.Controllers
{

    public abstract class _BaseController<T> : Controller where T : _BaseController<T>
    {
        private IConfiguration? _configuration;
        private IntegracaoApi? _integracaoApi;
        private SessaoUsuario? _sessaoUsuario;



        protected IConfiguration? Configuration => _configuration ?? (_configuration = HttpContext?.RequestServices.GetService<IConfiguration>());
        protected IntegracaoApi? ExecutaAPI => _integracaoApi ?? (_integracaoApi = HttpContext?.RequestServices.GetService<IntegracaoApi>());
        protected SessaoUsuario? UsuarioLogado => _sessaoUsuario ?? (_sessaoUsuario = HttpContext?.RequestServices.GetService<SessaoUsuario>());


        public async Task<IEnumerable<SelectListItem>> ListaUF()
        {
            var retornoApi = await ExecutaAPI.GetAPI("UF/GetAll");
            List<UFDTO> objRetorno = JsonConvert.DeserializeObject<List<UFDTO>>(retornoApi.data);

            var ListaObj = objRetorno.Select(a => new SelectListItem(a.Sigla.ToString() + " - " + a.Descricao, a.Id.ToString()));

            return ListaObj;
        }


        public async Task<IEnumerable<SelectListItem>> ListaMunicipioPorIdUF(int IdUF)
        {
            ExecutaAPI.ParametrosAPI.Clear();
            ExecutaAPI.ParametrosAPI.Add(IdUF.ToString());
            var retornoApi = await ExecutaAPI.GetAPI("Municipio/GetbyIdUF");
            List<MunicipioDTO> objRetorno = JsonConvert.DeserializeObject<List<MunicipioDTO>>(retornoApi.data);
            ExecutaAPI.ParametrosAPI.Clear();

            var ListaObj = objRetorno.Select(a => new SelectListItem(a.Nome, a.Id.ToString()));

            return ListaObj;
        }


        public async Task<IEnumerable<SelectListItem>> ListaTipoInscricaoEmpresa()
        {
            var retornoApi = await ExecutaAPI.GetAPI("TipoInscricaoEmpresa/GetAll");
            List<TipoInscricaoEmpresaDTO> objRetorno = JsonConvert.DeserializeObject<List<TipoInscricaoEmpresaDTO>>(retornoApi.data);

            var ListaObj = objRetorno.Select(a => new SelectListItem(a.Descricao, a.Id.ToString()));

            return ListaObj;
        }

        public async Task<IEnumerable<SelectListItem>> ListaTipoContaCorrente()
        {
            var retornoApi = await ExecutaAPI.GetAPI("TipoContaCorrente/GetAll");
            List<TipoContaCorrenteDTO> objRetorno = JsonConvert.DeserializeObject<List<TipoContaCorrenteDTO>>(retornoApi.data);

            ViewBag.TContaCorrente = objRetorno.Select(a => new SelectListItem(a.Id.ToString() + " - " + a.Descricao, a.Id.ToString()));

            return ViewBag.TContaCorrente;
        }


        public async Task<IActionResult> Index_Geral<T>(string Rota, string ViewName)
        {
            var retornoApi = await ExecutaAPI.GetAPI(Rota);

            if (retornoApi.statuscode == 401)
                return RedirectToAction("Login", "Home");

            if (!retornoApi.success)
                return RedirectToAction("ErrorAPI", "Home", retornoApi);

            try
            {
                var objRetorno = JsonConvert.DeserializeObject<List<T>>(retornoApi.data);
                return View(ViewName, objRetorno);
            }
            catch (Exception)
            {

                return RedirectToAction("ErrorAPI", "Home", retornoApi);
            }
            
        }



        public async Task<IActionResult> Editar_Geral<T>(string Rota, string ViewName)
        {
            var retornoApi = await ExecutaAPI.GetAPI(Rota);

            if (retornoApi.statuscode == 401)
                return RedirectToAction("Login", "Home");

            if (!retornoApi.success)
                return RedirectToAction("ErrorAPI", "Home", retornoApi);

            try
            {
                var objRetorno = JsonConvert.DeserializeObject<T>(retornoApi.data);
                return View(ViewName, objRetorno);
            }
            catch (Exception)
            {

                return RedirectToAction("ErrorAPI", "Home", retornoApi);
            }

        }


    }
}
