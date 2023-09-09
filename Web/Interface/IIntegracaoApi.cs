namespace Web.Interface
{
    public interface IIntegracaoApi
    {
        Task<API_Retorno> GetAPI(string nameApi, string token);
        Task<API_Retorno> GetAPI(string nameApi, List<string> paramapi, string token);
        Task<API_Retorno> PostAPI<T>(string nameApi, T body, string token);
        Task<API_Retorno> PostAPI<T>(string nameApi, List<string> paramapi, T body, string token);
        Task<API_Retorno> PutAPI<T>(string nameApi, T body, string token);
        Task<API_Retorno> PutAPI<T>(string nameApi, List<string> paramapi, T body, string token);
        Task<API_Retorno> DeleteAPI(string nameApi, string token);
        Task<API_Retorno> DeleteAPI(string nameApi, List<string> paramapi, string token);
    }
}