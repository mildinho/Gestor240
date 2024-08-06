namespace API.Biblioteca.JWT
{
    public static class Settings
    {
        public static string SecretKey = "369f7627819b16966f7d9a6e83ddfeb5e2003ba3138cb02b5af4966f1567558a14522b53c907c3beb541d03b16c1dc187f7d22cc51f34e2c40372bb52b929aa2";
        public static string Issuer = "Gestor20";
        public static string Audience = "www.gestor240.com.br";
        public static DateTime Expires = DateTime.UtcNow.AddMinutes(50);
    }
}
