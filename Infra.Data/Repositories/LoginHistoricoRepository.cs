using Dominio.Entidades;
using Dominio.Interfaces;
using Infra.Data.Contexto;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Infra.Data.Repositories
{
    public class LoginHistoricoRepository : GenericoRepository<LoginHistorico>, ILoginHistoricoRepository
    {
        private readonly DBContexto _context;

        public LoginHistoricoRepository(DBContexto context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<LoginHistorico>> PesquisarPorDataAsync(DateTime Inicio, DateTime Fim)
        {
            return await _context.LoginHistorico.
                Where(x => x.Data >= Inicio && x.Data <= Fim).ToListAsync();
        }



    }



}
