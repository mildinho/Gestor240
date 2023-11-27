using Dominio.Entidades;
using Microsoft.EntityFrameworkCore;


//Add - Migration Dia14 - Project API
namespace Infra.Data.Contexto
{
    public class DBContexto : DbContext
    {
        public DBContexto(DbContextOptions<DBContexto> options) : base(options)
        {
        

        }

        public DbSet<Banco> Banco { get; set; } 
        public DbSet<TipoOperacao> TipoOperacao { get; set; }
        public DbSet<TipoServico> TipoServico { get; set; }
        public DbSet<UF>? UF { get; set; }
        public DbSet<Municipio>? Municipio { get; set; }
        public DbSet<Beneficiario>? Beneficiario { get; set; }
        public DbSet<FormaLancamento>? FormaLancamento { get; set; }
        public DbSet<TipoInscricaoEmpresa>? TipoInscricaoEmpresa { get; set; }
        public DbSet<Agencia>? Agencia { get; set; }
        public DbSet<Conta>? Conta { get; set; }
        public DbSet<Pagador>? Pagador { get; set; }
        public DbSet<Financas>? Financas { get; set; }
        public DbSet<TipoPix>? TipoPix { get; set; }
        public DbSet<TipoContaCorrente>? TipoContaCorrente { get; set; }
        public DbSet<ContaCorrente>? ContaCorrente { get; set; }
        public DbSet<Login>? Login { get; set; }

    }

}
