namespace Dominio.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {

        IBancoRepository Banco { get; }
      


        Task<int> SaveAsync();

    }
}
