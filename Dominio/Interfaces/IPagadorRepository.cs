﻿using Dominio.Entidades;

namespace Dominio.Interfaces
{
    public interface IPagadorRepository : IGenericoRepository<Pagador>
    {

        Task<IEnumerable<Pagador>> PesquisarPorCNPJ_CPFAsync(double CNPJ_CPF);
        Task<IEnumerable<Pagador>> PesquisarPorNomeAsync(string Nome);


    }
}