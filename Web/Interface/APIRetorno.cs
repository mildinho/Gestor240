namespace Web.Interface
{
    public class API_Retorno
    {
        public bool success { get; set; } = true;
        public int statuscode { get; set; } = 200;
        public string message { get; set; } = "OK";
        public dynamic data { get; set; } = "";
        public byte[] databyte { get; set; } = Convert.FromBase64String("");



        public API_Retorno ApiOk()
        {
            return this;
        }
        public API_Retorno ApiOk(string mensagem)
        {
            message = mensagem;
            return this;
        }
        public API_Retorno ApiOk(dynamic dados)
        {
            data = dados;
            return this;
        }
        public API_Retorno ApiOk(string mensagem, dynamic dados)
        {
            message = mensagem;
            data = dados;
            return this;
        }
        public API_Retorno ApiError()
        {
            success = false;
            statuscode = 400;
            message = "ERRO";
            return this;
        }
        public API_Retorno ApiError(string mensagem)
        {
            success = false;
            statuscode = 400;
            message = mensagem;
            return this;
        }
        public API_Retorno ApiError(dynamic dados)
        {
            success = false;
            statuscode = 400;
            message = "ERRO";
            data = dados;
            return this;
        }
        public API_Retorno ApiError(string mensagem, dynamic dados)
        {
            success = false;
            statuscode = 400;
            message = mensagem;
            data = dados;
            return this;
        }
    }
}
