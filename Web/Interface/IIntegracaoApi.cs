namespace Web.Interface
{
    public interface IIntegracaoApi
    {
        Task<API_Retorno> GetAPI(string nameApi);
        Task<API_Retorno> PostAPI<T>(string nameApi, T body);
        Task<API_Retorno> PutAPI<T>(string nameApi, T body);
        Task<API_Retorno> DeleteAPI(string nameApi);

    }
}