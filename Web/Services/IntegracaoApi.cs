using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Reflection;
using Web.Interface;

namespace Web.Services
{
    public class IntegracaoApi : IIntegracaoApi
    {
        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient;

        public IntegracaoApi(IConfiguration configuration)
        {
            _configuration = configuration;

            _httpClient = new();
            _httpClient.BaseAddress = new Uri(_configuration["APIConfig:UrlBase"]);
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(_configuration["APIConfig:ContentTypeJson"]));

        }

        public async Task<API_Retorno> GetAPI(string nameApi, string token = "")
        {
            if (! String.IsNullOrEmpty(token))
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }

            var response = await _httpClient.GetAsync(nameApi);
            var resposta = await Verifica_Acesso(response);

            return resposta;
        }

        public async Task<API_Retorno> GetAPI(string nameApi, List<string> paramapi, string token = "")
        {
            foreach (var item in paramapi)
            {
                nameApi = string.Concat(nameApi, "/" + item);
            }

            if (!String.IsNullOrEmpty(token))
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }

            var response = await _httpClient.GetAsync(nameApi);
            var resposta = await Verifica_Acesso(response);

            return resposta;
        }

        public async Task<API_Retorno> PostAPI<T>(string nameApi, T body, string token = "")
        {
            if (!String.IsNullOrEmpty(token))
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }

            var response = await _httpClient.PostAsJsonAsync(nameApi, body);
            var resposta = await Verifica_Acesso(response);

            return resposta;
        }

        public async Task<API_Retorno> PostAPI<T>(string nameApi, List<string> paramapi, T body, string token = "")
        {
            foreach (var item in paramapi)
            {
                nameApi = string.Concat(nameApi, "/" + item);
            }

            if (!String.IsNullOrEmpty(token))
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }

            var response = await _httpClient.PostAsJsonAsync(nameApi, body);
            var resposta = await Verifica_Acesso(response);

            return resposta;
        }

        public async Task<API_Retorno> PutAPI<T>(string nameApi, T body, string token = "")
        {
            if (!String.IsNullOrEmpty(token))
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }

            var response = await _httpClient.PutAsJsonAsync(nameApi, body);
            var resposta = await Verifica_Acesso(response);

            return resposta;
        }

        public async Task<API_Retorno> PutAPI<T>(string nameApi, List<string> paramapi, T body, string token = "")
        {
            foreach (var item in paramapi)
            {
                nameApi = string.Concat(nameApi, "/" + item);
            }

            if (!String.IsNullOrEmpty(token))
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }

            var response = await _httpClient.PutAsJsonAsync(nameApi, body);
            var resposta = await Verifica_Acesso(response);

            return resposta;
        }

        public async Task<API_Retorno> DeleteAPI(string nameApi, string token = "")
        {
            if (!String.IsNullOrEmpty(token))
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }

            var response = await _httpClient.DeleteAsync(nameApi);
            var resposta = await Verifica_Acesso(response);

            return resposta;
        }

        public async Task<API_Retorno> DeleteAPI(string nameApi, List<string> paramapi, string token = "")
        {
            foreach (var item in paramapi)
            {
                nameApi = string.Concat(nameApi, "/" + item);
            }

            if (!String.IsNullOrEmpty(token))
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }

            var response = await _httpClient.DeleteAsync(nameApi);
            var resposta = await Verifica_Acesso(response);

            return resposta;
        }

        public async Task<API_Retorno> Verifica_Acesso2(HttpResponseMessage response)
        {
            API_Retorno resposta = await response.Content.ReadAsAsync<API_Retorno>();
        
            if ((int)response.StatusCode == 401)
            {
                resposta.statuscode = 401;
                resposta.success = false;
                resposta.mensage = "Acesso Negado";
            }
            return resposta;
        }

        public async Task<API_Retorno> Verifica_Acesso(HttpResponseMessage response)
        {
            //API_Retorno resposta = await response.Content.ReadAsAsync<API_Retorno>();
            var jsonString = await response.Content.ReadAsStringAsync();

            List<API_Retorno> resposta = JsonConvert.DeserializeObject<List<API_Retorno>>(jsonString);

            if ((int)response.StatusCode == 401)
            {
                resposta[0].statuscode = 401;
                resposta[0].success = false;
                resposta[0].mensage = "Acesso Negado";
            }
            return resposta[0];
        }
    }
}
