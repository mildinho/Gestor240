using Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Interfaces
{
    public interface IMunicipioRepository : IGenericoRepository<Municipio>
    {

        Task<IEnumerable<Municipio>> PesquisarPorMunicipioAsync(string Municipio);
        Task<IEnumerable<Municipio>> PesquisarPorUFMunicipioAgregadoAsync(int IdUF, string Municipio);
        Task<Municipio> PesquisarPorIdAgregadoAsync(int Id);
        Task<IQueryable<Municipio>> ListarTodosAgregados();
    }
}
