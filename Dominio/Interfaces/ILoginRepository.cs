using Dominio.Entidades;

namespace Dominio.Interfaces
{
    public interface ILoginRepository : IGenericoRepository<Login>
    {
        Task<IEnumerable<Login>> PesquisarPorEmailSenhaAsync(string Email, string Senha);
        Task<IEnumerable<Login>> PesquisarPorEmailAsync(string Email);
    }
}
