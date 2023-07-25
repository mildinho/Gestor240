using Dominio.Entidades;
using Microsoft.EntityFrameworkCore;

namespace Infra.Data.Contexto
{
    public class DBContexto : DbContext
    {
        public DBContexto(DbContextOptions<DBContexto> options)
           : base(options)
        {
        }



        public DbSet<Banco> Banco { get; set; }
    }
}
