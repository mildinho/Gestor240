using Dominio.Entidades;
using Dominio.Interfaces;
using Infra.Data.Contexto;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace Infra.Data.Repositories
{
    public class AgenciaRepository : GenericoRepository<Agencia>, IAgenciaRepository
    {
        private readonly DBContexto _context;

        public AgenciaRepository(DBContexto context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Agencia>> PesquisarPorBancoAgenciaAsync(int IdBanco, int Agencia)
        {
            return await _context.Agencia.Where(x => x.BancoId == IdBanco && x.NumeroAgencia == Agencia).ToListAsync();
        }
        public async Task<IEnumerable<Agencia>> PesquisarPorBancoAgenciaAgregadoAsync(int IdBanco, int Agencia)
        {
           

            return await _context.Agencia.
                Include(a => a.Banco).
                Where(x => x.BancoId == IdBanco && x.NumeroAgencia == Agencia).
            ToListAsync();
        }

        public virtual async Task<Agencia> PesquisarPorIdAgregadoAsync(int Id)
        {
            return await _context.Agencia.
                 Include(a => a.Banco).
                 Where(x => x.Id == Id).FirstOrDefaultAsync();
             
        }


    }
}
