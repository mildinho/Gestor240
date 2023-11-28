namespace API.Biblioteca.JWT
{
    public class TokenUsuario
    {
        public string Email { get; set; }
        public string Token { get; set; }
        public DateTime Validade { get; set;}
    }
}
