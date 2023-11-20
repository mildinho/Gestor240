using Dominio.DTO;
using Dominio.Entidades;
using Dominio.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moq;

namespace TDD;

public class UnitTest1 : IClassFixture<DependencySetupFixture>
{
    private ServiceProvider _serviceProvider;
    private readonly Mock<IUnitOfWork> _uow;



    public UnitTest1(DependencySetupFixture fixture)
    {
        _serviceProvider = fixture.ServiceProvider;
        _uow = new Mock<IUnitOfWork>();
        //_uow = (Mock<IUnitOfWork>)_serviceProvider.GetRequiredService<IUnitOfWork>();  



    }

    [Fact]
    public void Get_Todos_Banco()
    {
        Banco banco = new();
        Assert.Equal(0, banco.Id);
        

        var objeto01 = _uow.Setup(x => x.Banco.PesquisarPorIdAsync(1));
        Assert.NotNull(objeto01);
        

    }


}