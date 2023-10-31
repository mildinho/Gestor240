using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http.Headers;
using System.Reflection;
using Web.Interface;

namespace Web.Services
{
    public class IntegracaoApi : IIntegracaoApi
    {
        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient;


        public string TokenBearer { get; set; } = string.Empty;
        public List<string> ParametrosAPI { get; set; } = new List<string>();



        public IntegracaoApi(IConfiguration configuration)
        {
            _configuration = configuration;

            _httpClient = new();
            _httpClient.BaseAddress = new Uri(_configuration["APIConfig:UrlBase"]);
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(_configuration["APIConfig:ContentTypeJson"]));

        }

        public IntegracaoApi()
        {
        }

        public async Task<API_Retorno> GetAPI(string nameApi)
        {
            foreach (var item in ParametrosAPI)
            {
                nameApi = string.Concat(nameApi, "/" + item);
            }

            if (!String.IsNullOrEmpty(TokenBearer))
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", TokenBearer);
            }

            var response = await _httpClient.GetAsync(nameApi);
            var resposta = await Configura_Retorno(response);

            return resposta;
        }



        public async Task<API_Retorno> PostAPI<T>(string nameApi, T body)
        {
            foreach (var item in ParametrosAPI)
            {
                nameApi = string.Concat(nameApi, "/" + item);
            }

            if (!String.IsNullOrEmpty(TokenBearer))
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", TokenBearer);
            }

            var response = await _httpClient.PostAsJsonAsync(nameApi, body);
            var resposta = await Configura_Retorno(response);

            return resposta;
        }



        public async Task<API_Retorno> PutAPI<T>(string nameApi, T body)
        {
            foreach (var item in ParametrosAPI)
            {
                nameApi = string.Concat(nameApi, "/" + item);
            }

            if (!String.IsNullOrEmpty(TokenBearer))
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", TokenBearer);
            }

            var response = await _httpClient.PutAsJsonAsync(nameApi, body);
            var resposta = await Configura_Retorno(response);

            return resposta;
        }


        public async Task<API_Retorno> DeleteAPI(string nameApi)
        {
            foreach (var item in ParametrosAPI)
            {
                nameApi = string.Concat(nameApi, "/" + item);
            }
            if (!String.IsNullOrEmpty(TokenBearer))
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", TokenBearer);
            }

            var response = await _httpClient.DeleteAsync(nameApi);
            var resposta = await Configura_Retorno(response);

            return resposta;
        }



        private async Task<API_Retorno> Configura_Retorno(HttpResponseMessage response)
        {
            var jsonString = await response.Content.ReadAsStringAsync();

            //API_Retorno resposta = JsonConvert.DeserializeObject<API_Retorno>(jsonString);


            API_Retorno resposta = new API_Retorno
            {
                data = jsonString,
                statuscode = (int)response.StatusCode,
                success = response.IsSuccessStatusCode,
                message = (int)response.StatusCode == 401 ? "Acesso Negado" : response.StatusCode.ToString()
            };

            return resposta;
        }
    }
}
