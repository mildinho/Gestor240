namespace Dominio.DTO
{
    public class TokenUsuarioDTO
    {
        public string Id { get; set; }  
        public string Nome { get; set; } = string.Empty;
        public string PrimeiroNome { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Token { get; set; } = string.Empty;
        public bool SexoMasculino { get; set; } = true;
        public DateTime Validade { get; set; } = DateTime.Now;
    }
}
