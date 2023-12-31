﻿using Dominio.Entidades;

namespace Dominio.Interfaces
{
    public interface IPagadorRepository : IGenericoRepository<Pagador>
    {

        Task<Pagador> PesquisarPorCNPJ_CPFAsync(string CNPJ_CPF);
        Task<IEnumerable<Pagador>> PesquisarPorNomeAsync(string Nome);
        IQueryable<Pagador> ListarTodosAgregados();

    }
}
