using Dominio.DTO;
using Dominio.Entidades;
using Dominio.Interfaces;
using Infra.Data.Contexto;
using Infra.Data.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moq;

namespace TDD;

public class BancoTeste : IClassFixture<DependencySetupFixture>
{
    private ServiceProvider _serviceProvider;


    public BancoTeste(DependencySetupFixture fixture)
    {
        _serviceProvider = fixture.ServiceProvider;
    }


    [Fact(DisplayName = "CRUD - INSERIR - BANCO")]
    public async Task Consegui_Inserir()
    {
       using (var contexto = _serviceProvider.GetService<DBContexto>())
        {
            BancoRepository _repositorio01 = new BancoRepository(contexto);
            Banco banco = new Banco
            {
                Codigo = 1,
                Nome = Faker.Name.First(),
                ISPB = Faker.Identification.UKNationalInsuranceNumber()
            };

            var Registro01 = await _repositorio01.InserirAsync(banco);


            Assert.NotNull(Registro01);

            Assert.Equal(banco.Nome, Registro01.Nome);
        }


    }


}