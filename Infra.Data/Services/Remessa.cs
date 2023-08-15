using Dominio.Entidades;
using Dominio.Interfaces;
using Infra.Data.Contexto;
using System;
using System.Collections.Generic;
using System.IO.Enumeration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Data.Services
{
    public class Remessa : IRemessa
    {

        private readonly IUnitOfWork _UOW;
        private readonly ILayout _layout;
        public Remessa(IUnitOfWork unitOfWork, ILayout layout)
        {
            _UOW = unitOfWork;
            _layout = layout;
        }

        public Task Cobranca()
        {
            throw new NotImplementedException();
        }

        public async Task<string> Pagamento(int IdBeneficiario, int IDConta, DateTime Inicio, DateTime Fim)
        {
            Beneficiario beneficiario = await _UOW.Beneficiario.PesquisarPorIdAgregadoAsync(IdBeneficiario);

            if (beneficiario == null)
            {
                return "Beneficiário Não Encontrado";
            }

            Conta conta = await _UOW.Conta.PesquisarPorIdAgregadoAsync(IDConta);

            if (conta == null)
            {
                return "Conta Nao Encontrada";
            }

            if (conta.BeneficiarioID != IdBeneficiario)
            {
                return "Conta Não Pertence ao Beneficiário Informado";
            }


            IQueryable<Financas> ObjFinancas = await _UOW.Financas.TitulosPorVencimentoSemPagamentoAsync(IdBeneficiario, Inicio, Fim);

            if (ObjFinancas.Count() <= 0)
            {
                return "Não Registro de Finanças para os Parametros Informado";
            }



            string nomeArquivo = await _layout.Remessa_Padrao240(ObjFinancas, conta, beneficiario);

            return nomeArquivo;

        }

    }
}
