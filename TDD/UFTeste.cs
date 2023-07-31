using API.Controllers;
using Dominio.Entidades;
using Dominio.Interfaces;
using Moq;

namespace TDD
{
    public class UFTeste
    {

        private readonly Mock<IUnitOfWork> _UOW_;

        public UFTeste()
        {
            _UOW_ = new Mock<IUnitOfWork>();
        }



        [Fact]
        public void Inserir_Registro()
        {
            UF _uf = new UF { Sigla = "SP", Descricao = "São Paulo"};


            //_UOW_.Setup(x => x.UF.InserirAsync(_uf)).Returns(_uf);

            var UFController = new UFController(_UOW_.Object);

            var UFResultado = UFController.Post(_uf);
            //assert
            Assert.NotNull(UFResultado);
        }
    }
}