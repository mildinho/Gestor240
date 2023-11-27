using Dominio.Entidades;

namespace Dominio.Interfaces
{
    public interface ILoginRepository : IGenericoRepository<Login>
    {
        Task<IEnumerable<Login>> PesquisarPorEmailAsync(string Email, string Senha);
    }
}
