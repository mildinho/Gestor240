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
            return await _context.Municipio.Where(x => x.Descricao.ToLower().Contains(Descricao.ToLower())).ToListAsync();
        }

     
    }
}
