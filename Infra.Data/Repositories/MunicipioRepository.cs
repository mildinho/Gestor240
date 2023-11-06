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

        public async Task<IEnumerable<Municipio>> PesquisarPorDescricaoAsync(string Descricao)
        {
            return await _context.Municipio.Where(x => x.Nome.ToLower().Contains(Descricao.ToLower())).ToListAsync();
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
    }
}
