﻿using Dominio.Entidades;

namespace Dominio.Interfaces
{
    public interface IRemessa
    {
        Task<string> Pagamento(int IdBeneficiario, DateTime Inicio, DateTime Fim);
        Task Cobranca();
        

    }
}
