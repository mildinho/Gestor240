using Dominio.Entidades;
using Microsoft.EntityFrameworkCore;


//Add - Migration Dia14 - Project API
namespace Infra.Data.Contexto
{
    public class DBContexto : DbContext
    {
        public DBContexto(DbContextOptions<DBContexto> options)
           : base(options)
        {

        }

        public DbSet<Banco> Banco { get; set; }
        public DbSet<TipoOperacao> TipoOperacao { get; set; }
        public DbSet<TipoServico> TipoServico { get; set; }
    }
}
