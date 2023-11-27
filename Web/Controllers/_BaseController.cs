using Dominio.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using Web.Services;

namespace Web.Controllers
{

    public abstract class _BaseController<T> : Controller where T : _BaseController<T>
    {

        private IConfiguration? _configuration;
        private IntegracaoApi? _integracaoApi;


        protected IConfiguration? Configuration => _configuration ?? (_configuration = HttpContext?.RequestServices.GetService<IConfiguration>());


        //protected IntegracaoApi ExecutaAPI => new IntegracaoApi(Configuration);
        protected IntegracaoApi? ExecutaAPI => _integracaoApi ?? (_integracaoApi = HttpContext?.RequestServices.GetService<IntegracaoApi>());




        public async Task<IEnumerable<SelectListItem>> ListaUF()
        {
            var retornoApi = await ExecutaAPI.GetAPI("UF/GetAll");
            List<UFDTO> objRetorno = JsonConvert.DeserializeObject<List<UFDTO>>(retornoApi.data);

            var ListaObj = objRetorno.Select(a => new SelectListItem(a.Sigla.ToString() + " - " + a.Descricao, a.Id.ToString()));

            return ListaObj;
        }

        public async Task<IEnumerable<SelectListItem>> ListaMunicipio()
        {
            var retornoApi = await ExecutaAPI.GetAPI("Municipio/GetAll");
            List<MunicipioDTO> objRetorno = JsonConvert.DeserializeObject<List<MunicipioDTO>>(retornoApi.data);

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
    }
}
