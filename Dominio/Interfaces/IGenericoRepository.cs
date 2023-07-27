using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Interfaces
{
    public interface IGenericoRepository<Tabela> where Tabela : class
    {

        Task<Tabela> InserirAsync(Tabela tabela);

        Task<Tabela> AtualizarAsync(Tabela tabela);

        Task DeletarAsync(int Id);

        Task<Tabela> PesquisarPorIdAsync(int Id);

        IQueryable<Tabela> ListarTodos();
    }
}
