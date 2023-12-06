namespace Dominio.DTO
{
    public class TokenUsuario
    {
        public string Nome { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Token { get; set; } = string.Empty;
        public DateTime Validade { get; set; } = DateTime.Now;
    }
}
