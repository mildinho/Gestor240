namespace Web.Biblioteca.Session
{
    public class ConfiguraSessao
    {
        private IHttpContextAccessor _contexto;
        public ConfiguraSessao(IHttpContextAccessor context)
        {
            _contexto = context;
        }

        public void Cadastrar(string Key, string Valor)
        {
            _contexto.HttpContext.Session.SetString(Key, Valor);
        }

        public void Atualizar(string Key, string Valor)
        {
            if (Existe(Key))
            {
                _contexto.HttpContext.Session.Remove(Key);
            }

            Cadastrar(Key, Valor);


        }

        public void Remover(string Key)
        {
            _contexto.HttpContext.Session.Remove(Key);
        }

        public string Consultar(string Key)
        {
            return _contexto.HttpContext.Session.GetString(Key);
        }

        public bool Existe(string Key)
        {

            if (_contexto.HttpContext.Session.GetString(Key) == null)
            {
                return false;
            }

            return true;
        }

        public void RemoverTodos() => _contexto.HttpContext.Session.Clear();

    }
}
