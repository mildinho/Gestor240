using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Interfaces
{
    public interface IGenericoRepository<Tabela> where Tabela : class
    {

        Task InserirAsync(Tabela tabela);

        Task AtualizarAsync(Tabela tabela);

        Task DeletarAsync(int Id);

        Task<Tabela> SelecionarPorCodigoAsync(int Id);
    }
}
