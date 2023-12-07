using Dominio.Entidades;

namespace Dominio.Interfaces
{
    public interface ILoginRepository : IGenericoRepository<Login>
    {
        Task<Login> PesquisarPorEmailSenhaAsync(string Email, string Senha);
        Task<IEnumerable<Login>> PesquisarPorEmailAsync(string Email);

        string Criptografar(string Texto);
        string DesCriptografar(string Texto);
    }
}
