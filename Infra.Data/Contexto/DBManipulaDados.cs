using Dominio.Entidades;
using System;

namespace Infra.Data.Contexto
{
    public class DBManipulaDados
    {
        public static void Cadastrar(DBContexto dbContext)
        {
            ArgumentNullException.ThrowIfNull(dbContext, nameof(dbContext));

            dbContext.Database.EnsureCreated();

            if (!dbContext.Login.Any())
            {
                var registros = new Login[]
                {
                    //fer123
                    new Login{
                        Email="mildinho@gmail.com",
                        Password = "NjhQI/aFYQikKjc478qUVQ==",
                        Nome = "Casagrande",
                        SexoMasculino = true,
                        DataNascimento = DateTime.Now,
                    }

                };

                foreach (var ObjetoRegistro in registros)
                    dbContext.Login.Add(ObjetoRegistro);

                dbContext.SaveChanges();
            }


            if (!dbContext.TipoPix.Any())
            {
                var registros = new TipoPix[]
                {
                    new TipoPix{Codigo = "01", Descricao = "Tipo Telefone"},
                    new TipoPix{Codigo = "02", Descricao = "Tipo Email"},
                    new TipoPix{Codigo = "03", Descricao = "Tipo CPF/CNPJ"},
                    new TipoPix{Codigo = "04", Descricao = "Chave Aleatória"},
                    new TipoPix{Codigo = "05", Descricao = "Dados Bancários"}
                };

                foreach (var ObjetoRegistro in registros)
                    dbContext.TipoPix.Add(ObjetoRegistro);

                dbContext.SaveChanges();
            }




            if (!dbContext.Banco.Any())
            {
                var registros = new Banco[]
                {
                    new Banco{Codigo = 1, Nome = "BANCO DO BRASIL", ISPB = "0"},
                    new Banco{Codigo = 237, Nome = "BANCO BRADESCO S/A", ISPB = "60746948"},
                    new Banco{Codigo = 341, Nome = "ITAÚ", ISPB = "60701190"}

                };

                foreach (var ObjetoRegistro in registros)
                    dbContext.Banco.Add(ObjetoRegistro);

                dbContext.SaveChanges();
            }


            if (!dbContext.TipoInscricaoEmpresa.Any())
            {
                var registros = new TipoInscricaoEmpresa[]
                {
                    new TipoInscricaoEmpresa{Codigo = 0, Descricao = "Isento / Não Informado"},
                    new TipoInscricaoEmpresa{Codigo = 1, Descricao = "CPF"},
                    new TipoInscricaoEmpresa{Codigo = 2, Descricao = "CNPJ"},
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
                    new TipoServico{Codigo = "01", Descricao = "Cobrança", Remessa_P = true, Remessa_Q = true},
                    new TipoServico{Codigo = "05", Descricao = "Débitos", Remessa_A = true},
                    new TipoServico{Codigo = "06", Descricao = "Custódia de Cheques", Remessa_D = true},
                    new TipoServico{Codigo = "08", Descricao = "Consulta / Informação Margem", Remessa_H = true},
                    new TipoServico{Codigo = "09", Descricao = "Averbação da Consignação / Retenção", Remessa_H = true},
                    new TipoServico{Codigo = "10", Descricao = "Pagamento de Dividendos", Remessa_A = true},
                    new TipoServico{Codigo = "11", Descricao = "Manutenção da Consignação", Remessa_H = true},
                    new TipoServico{Codigo = "12", Descricao = "Consignação de Parcelas", Remessa_H = true},
                    new TipoServico{Codigo = "13", Descricao = "Glosa de Consignação (INSS)", Remessa_H = true},
                    new TipoServico{Codigo = "20", Descricao = "Pagamento Fornecedor", Remessa_A = true},
                    /*
                     * Quando for Pagamento de Contas, Tributos e Impostos:
                     *  -Gerar W COM Código de Barras
                     *  -Gerar B SEM Código de Barras
                     */
                    new TipoServico{Codigo = "22", Descricao = "Pagamento de Contas, Tributos e Impostos", Remessa_W = true},
                    new TipoServico{Codigo = "23", Descricao = "Interoperabilidade entre Contas de Instituições de Pagamentos", Remessa_A = true},
                    new TipoServico{Codigo = "25", Descricao = "Compror", Remessa_A = true, Remessa_I = true},
                    new TipoServico{Codigo = "26", Descricao = "Compror Rotativo", Remessa_A = true, Remessa_I = true},
                    new TipoServico{Codigo = "29", Descricao = "Alegação do Pagador", Remessa_Y = true},
                    new TipoServico{Codigo = "30", Descricao = "Pagamento de Salários", Remessa_A = true},
                    new TipoServico{Codigo = "32", Descricao = "Pagamento de Honorários", Remessa_A = true},
                    new TipoServico{Codigo = "33", Descricao = "Pagamento de Bolsa Auxílio", Remessa_A = true},
                    new TipoServico{Codigo = "34", Descricao = "Pagamento de Prebenda", Remessa_A = true},
                    new TipoServico{Codigo = "40", Descricao = "Vendor", Remessa_K = true, Remessa_L = true},
                    new TipoServico{Codigo = "41", Descricao = "Vendor a Termo", Remessa_K = true, Remessa_L = true},
                    new TipoServico{Codigo = "50", Descricao = "Pagamento Sinistros Segurados",Remessa_A = true},
                    new TipoServico{Codigo = "60", Descricao = "Pagamento Despesas Viajante em Trânsito",Remessa_A = true},
                    new TipoServico{Codigo = "70", Descricao = "Pagamento Autorizado",Remessa_A = true},
                    new TipoServico{Codigo = "75", Descricao = "Pagamento Credenciados",Remessa_A = true},
                    new TipoServico{Codigo = "77", Descricao = "Pagamento de Remuneração", Remessa_A = true},
                    new TipoServico{Codigo = "80", Descricao = "Pagamento de Representantes / Vendedores Autorizados", Remessa_A = true},
                    new TipoServico{Codigo = "90", Descricao = "Pagamento Benefícios", Remessa_A = true},
                    new TipoServico{Codigo = "98", Descricao = "Pagamento Diversos", Remessa_A = true},


                    /*
                     * Boleto de Pagamento Eletrônico
                     * Conciliação Bancária nao tem Remessa
                     * Gestão de Caixa nao tem Remessa
                     * Consulta de Tributos a Pagar
                     */
                    new TipoServico{Codigo = "03", Descricao = "Boleto de Pagamento Eletrônico"},
                    new TipoServico{Codigo = "04", Descricao = "Conciliação Bancária"},
                    new TipoServico{Codigo = "07", Descricao = "Gestão de Caixa"},
                    new TipoServico{Codigo = "14", Descricao = "Consulta de Tributos a Pagar"},
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


            if (!dbContext.Municipio.Any())
            {

                var registros = new Municipio[]
                {
                    new Municipio{Nome = "CAMPINAS", UFId = 20, CodigoFiscal = "1"},
                    new Municipio{Nome = "SUMARÉ", UFId = 20, CodigoFiscal = "2"},
                    new Municipio{Nome = "HORTOLANDIA", UFId = 20, CodigoFiscal = "3"},
                    new Municipio{Nome = "GUAXUPE", UFId = 17, CodigoFiscal = "4"},
                    new Municipio{Nome = "GUARANESIA", UFId = 17, CodigoFiscal = "5"},
                    new Municipio{Nome = "RIO DE JANEIRO", UFId = 19, CodigoFiscal = "5"},


                };

                foreach (var ObjetoRegistro in registros)
                    dbContext.Municipio.Add(ObjetoRegistro);

                dbContext.SaveChanges();
            }

            if (!dbContext.Agencia.Any())
            {
                var registros = new Agencia[]
                {
                    new Agencia{BancoId = 1,
                    NumeroAgencia = 3360,
                    DigitoAgencia = "X",
                    },
                    new Agencia{BancoId = 2,
                    NumeroAgencia = 311,
                    DigitoAgencia = "5",
                    },

                };

                foreach (var ObjetoRegistro in registros)
                    dbContext.Agencia.Add(ObjetoRegistro);

                dbContext.SaveChanges();
            }


            if (!dbContext.Beneficiario.Any())
            {
                var registros = new Beneficiario[]
                {
                    new Beneficiario{
                        CNPJ_CPF = "08897417000291",
                        Nome = "FW DISTRIBUIDORA LTDA",
                        Fantasia = "FURACAO - CAMPINAS",
                        Endereco = "RUA ALTINO ARANTES",
                        Numero = "1250",
                        Bairro = "Jd. Lago II",
                        Cidade = "Campinas",
                        CEP = "13051110",
                        UFId = 20,
                        TipoInscricaoEmpresaId = 2,
                        MunicipioId = 1,

                    },
                    new Beneficiario{
                        CNPJ_CPF = "08897417000534",
                        Nome = "FW DISTRIBUIDORA LTDA",
                        Fantasia = "FURACAO - RJ",
                        Endereco = "Avenida Brasil",
                        Numero = "15127",
                        Bairro = "Viagario Geral",
                        Cidade = "Rio de Janeiro",
                        CEP = "21241051",
                        UFId = 19,
                        TipoInscricaoEmpresaId = 3,
                        MunicipioId = 6,
                    },

                };

                foreach (var ObjetoRegistro in registros)
                    dbContext.Beneficiario.Add(ObjetoRegistro);

                dbContext.SaveChanges();
            }


            if (!dbContext.Pagador.Any())
            {
                var registros = new Pagador[]
                {
                    new Pagador{
                        CNPJ_CPF = "98514946668",
                        Nome = "FERNANDO CASAGRANDE",
                        Fantasia = "LIMPA FOSSA HI-TECH",
                        Endereco = "RUA DAS DALIAS",
                        Numero = "1227",
                        Bairro = "JD. DAS BANDEIRAS",
                        Cidade = "CAMPINAS",
                        CEP = "13050088",
                        UFId = 20,
                        TipoInscricaoEmpresaId = 1,
                        MunicipioId = 1,

                    }

                };

                foreach (var ObjetoRegistro in registros)
                    dbContext.Pagador.Add(ObjetoRegistro);

                dbContext.SaveChanges();
            }


            if (!dbContext.Conta.Any())
            {
                var registros = new Conta[]
                {
                    new Conta{
                        AgenciaId = 1,
                        NumeroConta = 73552,
                        DigitoConta = "3",
                        BeneficiarioID = 2,
                        NumeroConvenio = "360593"
                    },

                new Conta
                {
                    AgenciaId = 2,
                    NumeroConta = 21013021,
                    DigitoConta = "0",
                    BeneficiarioID = 1,
                    NumeroConvenio = "360593"
                }

                };

                foreach (var ObjetoRegistro in registros)
                    dbContext.Conta.Add(ObjetoRegistro);

                dbContext.SaveChanges();
            }

            if (!dbContext.TipoContaCorrente.Any())
            {
                var registros = new TipoContaCorrente[]
                {
                    new TipoContaCorrente{
                    Id = 1,
                    Descricao = "Sucata"
                    },

                new TipoContaCorrente{
                    Id = 2,
                    Descricao = "IPTU"
                    },

                new TipoContaCorrente{
                    Id = 3,
                    Descricao = "Imposto de Renda"
                    },

                };

                foreach (var ObjetoRegistro in registros)
                    dbContext.TipoContaCorrente.Add(ObjetoRegistro);

                dbContext.SaveChanges();
            }


            if (!dbContext.Financas.Any())
            {
                Random diasPagamento = new();
                Random formaLancamento = new();
                Random tipoServico = new();

                List<Financas> registros = new();
                for (int i = 0; i < 500; i++)
                {
                    DateTime? dataPagamento = null;
                    DateTime dataVencimento = DateTime.Now.AddDays(i);

                    if ((i % 5) == 0)
                    {
                        dataPagamento = dataVencimento.AddDays(diasPagamento.Next(-10, 10));
                    }


                    if (dataPagamento == null)
                    {
                        registros.Add(
                            new Financas
                            {
                                BeneficiarioID = 1,
                                PagadorID = 1,
                                BancoID = 1,
                                FormaLancamentoID = formaLancamento.Next(1, 34),
                                TipoServicoID = tipoServico.Next(1, 33),
                                Documento = "DOCUMENTO =>" + i.ToString(),
                                Parcela = "A",
                                Emissao = DateTime.Today,
                                Vencimento = dataVencimento,
                                ValorPrincipal = i * Math.PI,
                                Abatimento = i / 3,
                                MoraDia = i
                            }
                        );

                    }
                    else
                    {
                        registros.Add(
                            new Financas
                            {
                                BeneficiarioID = 1,
                                PagadorID = 1,
                                BancoID = 1,
                                FormaLancamentoID = 31,
                                TipoServicoID = 2,
                                Documento = "DOCUMENTO =>" + i.ToString(),
                                Parcela = "A",
                                Emissao = DateTime.Today,
                                Vencimento = dataVencimento,
                                Pagamento = (DateTime)dataPagamento,
                                ValorPrincipal = i * Math.PI,
                                Abatimento = i / 3,
                                MoraDia = i
                            }
                        );
                    }

                }

                foreach (var ObjetoRegistro in registros)
                    dbContext.Financas.Add(ObjetoRegistro);

                dbContext.SaveChanges();
            }



        }
    }
}
