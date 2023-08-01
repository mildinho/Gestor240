﻿using Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Data.Contexto
{
    public class DBManipulaDados
    {
        public static void Cadastrar(DBContexto dbContext)
        {
            ArgumentNullException.ThrowIfNull(dbContext, nameof(dbContext));

            dbContext.Database.EnsureCreated();

            if (!dbContext.TipoInscricaoEmpresa.Any())
            {
                var registros = new TipoInscricaoEmpresa[]
                {
                    new TipoInscricaoEmpresa{Codigo = 0, Descricao = "Isento / Não Informado"},
                    new TipoInscricaoEmpresa{Codigo = 1, Descricao = "CPF"},
                    new TipoInscricaoEmpresa{Codigo = 2, Descricao = "CNPJ / CGC"},
                    new TipoInscricaoEmpresa{Codigo = 3, Descricao = "PIS / PASEP"},
                    new TipoInscricaoEmpresa{Codigo = 9, Descricao = "Outros"},
                };

                foreach (var ObjetoRegistro in registros)
                    dbContext.TipoInscricaoEmpresa.Add(ObjetoRegistro);

                dbContext.SaveChanges();
            }



            if (!dbContext.FormaLancamento.Any())
            {

                var registros = new FormaLancamento[]
                {
                    new FormaLancamento{Codigo = "01", Descricao = "Crédito em Conta Corrente / Salário"},
                    new FormaLancamento{Codigo = "02", Descricao = "Cheque Pagamento / Administrativo"},
                    new FormaLancamento{Codigo = "03", Descricao = "DOC / TED"},
                    new FormaLancamento{Codigo = "04", Descricao = "Cartão Salário"},
                    new FormaLancamento{Codigo = "05", Descricao = "Crédito em Conta Poupança"},
                    new FormaLancamento{Codigo = "10", Descricao = "OP à Disposição"},
                    new FormaLancamento{Codigo = "11", Descricao = "Pagamento de Contas e Tributos com Código de Barras"},
                    new FormaLancamento{Codigo = "16", Descricao = "Tributo - DARF Normal"},
                    new FormaLancamento{Codigo = "17", Descricao = "Tributo - GPS"},
                    new FormaLancamento{Codigo = "18", Descricao = "Tributo - DARF Simples"},
                    new FormaLancamento{Codigo = "19", Descricao = "Tributo IPTU - Prefeituras"},
                    new FormaLancamento{Codigo = "20", Descricao = "Pagamento com Autenticação"},
                    new FormaLancamento{Codigo = "21", Descricao = "Tributo - DARJ"},
                    new FormaLancamento{Codigo = "22", Descricao = "Tributo - GARE-SP ICMS"},
                    new FormaLancamento{Codigo = "23", Descricao = "Tributo - GARE-SP DR"},
                    new FormaLancamento{Codigo = "24", Descricao = "Tributo - GARE-SP ITCMD"},
                    new FormaLancamento{Codigo = "25", Descricao = "Tributo - IPVA"},
                    new FormaLancamento{Codigo = "26", Descricao = "Tributo - Licenciamento"},
                    new FormaLancamento{Codigo = "27", Descricao = "Tributo - DPVAT"},
                    new FormaLancamento{Codigo = "30", Descricao = "Liquidação de Títulos do Próprio Banco"},
                    new FormaLancamento{Codigo = "31", Descricao = "Pagamento de Títulos de Outros Bancos"},
                    new FormaLancamento{Codigo = "40", Descricao = "Extrato de Conta Corrente"},
                    new FormaLancamento{Codigo = "41", Descricao = "TED – Outra Titularidade"},
                    new FormaLancamento{Codigo = "43", Descricao = "TED – Mesma Titularidade"},
                    new FormaLancamento{Codigo = "44", Descricao = "TED para Transferência de Conta Investimento"},
                    new FormaLancamento{Codigo = "45", Descricao = "PIX Transferência"},
                    new FormaLancamento{Codigo = "47", Descricao = "PIX QR-CODE"},
                    new FormaLancamento{Codigo = "50", Descricao = "Débito em Conta Corrente"},
                    new FormaLancamento{Codigo = "70", Descricao = "Extrato para Gestão de Caixa"},
                    new FormaLancamento{Codigo = "71", Descricao = "Depósito Judicial em Conta Corrente"},
                    new FormaLancamento{Codigo = "72", Descricao = "Depósito Judicial em Poupança"},
                    new FormaLancamento{Codigo = "73", Descricao = "Extrato de Conta Investimento"},
                    new FormaLancamento{Codigo = "80", Descricao = "Pagamento de Tributos Municipais ISS – LCP 157 – próprio Banco"},
                    new FormaLancamento{Codigo = "81", Descricao = "Pagamento de Tributos Municipais ISS – LCP 157 – outros Bancos"}

                };

                foreach (var ObjetoRegistro in registros)
                    dbContext.FormaLancamento.Add(ObjetoRegistro);

                dbContext.SaveChanges();
            }

            if (!dbContext.TipoServico.Any())
            {

                var registros = new TipoServico[]
                {
                    new TipoServico{Codigo = "01", Descricao = "Cobrança"},
                    new TipoServico{Codigo = "03", Descricao = "Boleto de Pagamento Eletrônico"},
                    new TipoServico{Codigo = "04", Descricao = "Conciliação Bancária"},
                    new TipoServico{Codigo = "05", Descricao = "Débitos"},
                    new TipoServico{Codigo = "06", Descricao = "Custódia de Cheques"},
                    new TipoServico{Codigo = "07", Descricao = "Gestão de Caixa"},
                    new TipoServico{Codigo = "08", Descricao = "Consulta / Informação Margem"},
                    new TipoServico{Codigo = "09", Descricao = "Averbação da Consignação / Retenção"},
                    new TipoServico{Codigo = "10", Descricao = "Pagamento de Dividendos"},
                    new TipoServico{Codigo = "11", Descricao = "Manutenção da Consignação"},
                    new TipoServico{Codigo = "12", Descricao = "Consignação de Parcelas"},
                    new TipoServico{Codigo = "13", Descricao = "Glosa de Consignação (INSS)"},
                    new TipoServico{Codigo = "14", Descricao = "Consulta de Tributos a Pagar"},
                    new TipoServico{Codigo = "20", Descricao = "Pagamento Fornecedor"},
                    new TipoServico{Codigo = "22", Descricao = "Pagamento de Contas, Tributos e Impostos"},
                    new TipoServico{Codigo = "23", Descricao = "Interoperabilidade entre Contas de Instituições de Pagamentos"},
                    new TipoServico{Codigo = "25", Descricao = "Compror"},
                    new TipoServico{Codigo = "26", Descricao = "Compror Rotativo"},
                    new TipoServico{Codigo = "29", Descricao = "Alegação do Pagador"},
                    new TipoServico{Codigo = "30", Descricao = "Pagamento de Salários"},
                    new TipoServico{Codigo = "32", Descricao = "Pagamento de Honorários"},
                    new TipoServico{Codigo = "33", Descricao = "Pagamento de Bolsa Auxílio"},
                    new TipoServico{Codigo = "34", Descricao = "Pagamento de Prebenda"},
                    new TipoServico{Codigo = "40", Descricao = "Vendor"},
                    new TipoServico{Codigo = "41", Descricao = "Vendor a Termo"},
                    new TipoServico{Codigo = "50", Descricao = "Pagamento Sinistros Segurados"},
                    new TipoServico{Codigo = "60", Descricao = "Pagamento Despesas Viajante em Trânsito"},
                    new TipoServico{Codigo = "70", Descricao = "Pagamento Autorizado"},
                    new TipoServico{Codigo = "75", Descricao = "Pagamento Credenciados"},
                    new TipoServico{Codigo = "77", Descricao = "Pagamento de Remuneração"},
                    new TipoServico{Codigo = "80", Descricao = "Pagamento de Representantes / Vendedores Autorizados"},
                    new TipoServico{Codigo = "90", Descricao = "Pagamento Benefícios"},
                    new TipoServico{Codigo = "98", Descricao = "Pagamento Diversos"}
                };

                foreach (var ObjetoRegistro in registros)
                    dbContext.TipoServico.Add(ObjetoRegistro);

                dbContext.SaveChanges();
            }




            if (!dbContext.TipoOperacao.Any())
            {

                var registros = new TipoOperacao[]
                {
                    new TipoOperacao{Codigo = "C", Descricao = "Lançamento de Crédito"},
                    new TipoOperacao{Codigo = "D", Descricao = "Lançamento de Débito"},
                    new TipoOperacao{Codigo = "E", Descricao = "Extrato para Conciliação"},
                    new TipoOperacao{Codigo = "G", Descricao = "Extrato para Gestão de Caixa"},
                    new TipoOperacao{Codigo = "I", Descricao = "Informação de Títulos Capturados do Próprio Banco"},
                    new TipoOperacao{Codigo = "R", Descricao = "Arquivo Remessa"},
                    new TipoOperacao{Codigo = "T", Descricao = "Arquivo Retorno"},

                };

                foreach (var ObjetoRegistro in registros)
                    dbContext.TipoOperacao.Add(ObjetoRegistro);

                dbContext.SaveChanges();
            }


            if (!dbContext.UF.Any())
            {

                var registros = new UF[]
                {
                    new UF{Sigla = "RO", Descricao = "Rondônia", CodigoFiscal = "11"},
                    new UF{Sigla = "AC", Descricao = "Acre", CodigoFiscal = "12"},
                    new UF{Sigla = "AM", Descricao = "Amazonas", CodigoFiscal = "13"},
                    new UF{Sigla = "RR", Descricao = "Roraima", CodigoFiscal = "14"},
                    new UF{Sigla = "PA", Descricao = "Pará", CodigoFiscal = "15"},
                    new UF{Sigla = "AP", Descricao = "Amapá", CodigoFiscal = "16"},
                    new UF{Sigla = "TO", Descricao = "Tocantins", CodigoFiscal = "17"},
                    new UF{Sigla = "MA", Descricao = "Maranhão", CodigoFiscal = "21"},
                    new UF{Sigla = "PI", Descricao = "Piauí", CodigoFiscal = "22"},
                    new UF{Sigla = "CE", Descricao = "Ceará", CodigoFiscal = "23"},
                    new UF{Sigla = "RN", Descricao = "Rio Grande do Norte", CodigoFiscal = "24"},
                    new UF{Sigla = "PB", Descricao = "Paraíba", CodigoFiscal = "25"},
                    new UF{Sigla = "PE", Descricao = "Pernambuco", CodigoFiscal = "26"},
                    new UF{Sigla = "AL", Descricao = "Alagoas", CodigoFiscal = "27"},
                    new UF{Sigla = "SE", Descricao = "Sergipe", CodigoFiscal = "28"},
                    new UF{Sigla = "BA", Descricao = "Bahia", CodigoFiscal = "29"},
                    new UF{Sigla = "MG", Descricao = "Minas Gerais", CodigoFiscal = "31"},
                    new UF{Sigla = "ES", Descricao = "Espírito Santo", CodigoFiscal = "32"},
                    new UF{Sigla = "RJ", Descricao = "Rio de Janeiro", CodigoFiscal = "33"},
                    new UF{Sigla = "SP", Descricao = "São Paulo", CodigoFiscal = "35"},
                    new UF{Sigla = "PR", Descricao = "Paraná", CodigoFiscal = "41"},
                    new UF{Sigla = "SC", Descricao = "Santa Catarina", CodigoFiscal = "42"},
                    new UF{Sigla = "RS", Descricao = "Rio Grande do Sul", CodigoFiscal = "43"},
                    new UF{Sigla = "MS", Descricao = "Mato Grosso do Sul", CodigoFiscal = "50"},
                    new UF{Sigla = "MT", Descricao = "Mato Grosso", CodigoFiscal = "51"},
                    new UF{Sigla = "GO", Descricao = "Goiás", CodigoFiscal = "52"},
                    new UF{Sigla = "DF", Descricao = "Distrito Federal", CodigoFiscal = "53"}

                };

                foreach (var ObjetoRegistro in registros)
                    dbContext.UF.Add(ObjetoRegistro);

                dbContext.SaveChanges();
            }
        }
    }
}