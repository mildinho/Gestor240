using Dominio.Entidades;
using Dominio.Interfaces;
using Infra.Data.Contexto;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Infra.Data.Repositories
{
    public class MunicipioRepository : GenericoRepository<Municipio>, IMunicipioRepository
    {
        private readonly DBContexto _context;

        public MunicipioRepository(DBContexto context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Municipio>> PesquisarPorMunicipioAsync(string Municipio)
        {
            return await _context.Municipio.Include(a => a.UF).Where(x => x.Nome.ToLower().Contains(Municipio.ToLower())).ToListAsync();
        }


        public virtual async Task<Municipio> PesquisarPorIdAgregadoAsync(int Id)
        {
            return await _context.Municipio.
                 Include(a => a.UF).
                 Where(x => x.Id == Id).FirstOrDefaultAsync();

        }
        public async Task<IQueryable<Municipio>> ListarTodosAgregados()
        {
            var urls = await _context.Municipio.Include(a => a.UF).ToListAsync();
            return urls.AsQueryable();
        }


        public async Task<IEnumerable<Municipio>> PesquisarPorUFMunicipioAgregadoAsync(int IdUF, string Municipio)
        {
            return await _context.Municipio.
                Include(a => a.UF).
                Where(x => x.UFId == IdUF && x.Nome.ToUpper() == Municipio.ToUpper()).
            ToListAsync();
        }


        public async Task<IEnumerable<Municipio>> PesquisarPorUFAgregadoAsync(int IdUF)
        {
            return await _context.Municipio.
                Include(a => a.UF).
                Where(x => x.UFId == IdUF).ToListAsync();
        }

    }
}
