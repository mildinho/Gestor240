using Dominio.DTO;
using Newtonsoft.Json;

namespace Web.Biblioteca.Session
{
    public class SessaoUsuario
    {
        private string _key = "Login.Usuario";
        private ConfiguraSessao _sessao;

        public SessaoUsuario(ConfiguraSessao sessao)
        {
            _sessao = sessao;
        }


        public void GravaToken(TokenUsuarioDTO token)
        {
           string obj =  JsonConvert.SerializeObject(token);
            _sessao.Cadastrar(_key, obj);
        }

        public TokenUsuarioDTO GetToken()
        {
            if (_sessao.Existe(_key))
            {
                string obj = _sessao.Consultar(_key);
                TokenUsuarioDTO login = JsonConvert.DeserializeObject<TokenUsuarioDTO>(obj); 

                return login;
            }
            else
            {
                return null;
            }
        }

        

        public void Logout()
        {
            _sessao.RemoverTodos();
        }

    }
}
