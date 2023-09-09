namespace Web.Interface
{
    public class API_Retorno
    {
        public bool success { get; set; } = true;
        public int statuscode { get; set; } = 200;
        public string mensage { get; set; } = "OK";
        public dynamic data { get; set; } = "";
        public byte[] databyte { get; set; } = Convert.FromBase64String("");



        public API_Retorno ApiOk()
        {
            return this;
        }
        public API_Retorno ApiOk(string message)
        {
            mensage = message;
            return this;
        }
        public API_Retorno ApiOk(dynamic dados)
        {
            data = dados;
            return this;
        }
        public API_Retorno ApiOk(string message, dynamic dados)
        {
            mensage = message;
            data = dados;
            return this;
        }
        public API_Retorno ApiError()
        {
            success = false;
            statuscode = 400;
            mensage = "ERRO";
            return this;
        }
        public API_Retorno ApiError(string message)
        {
            success = false;
            statuscode = 400;
            mensage = message;
            return this;
        }
        public API_Retorno ApiError(dynamic dados)
        {
            success = false;
            statuscode = 400;
            mensage = "ERRO";
            data = dados;
            return this;
        }
        public API_Retorno ApiError(string message, dynamic dados)
        {
            success = false;
            statuscode = 400;
            mensage = message;
            data = dados;
            return this;
        }
    }
}
