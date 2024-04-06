using Moq;
using Xunit;
using FluentAssertions;
using nuget_fiap_app_produto_common.Interfaces.Repository;
using nuget_fiap_app_produto_common.Models;

namespace nuget_fiap_app_produto_test.Repository
{
    public class CategoriaRepositoryTest
    {
        [Fact]
        public async Task devePermitirConsultarTodasAsCategorias()
        {
            //Arrange 
            var dados = new List<Categoria>() {
                new Categoria() { Id = 1, Nome = "Lanche" },
                new Categoria() { Id = 2, Nome = "Acompanhamento" },
                new Categoria() { Id = 3, Nome = "Bebida" },
                new Categoria() { Id = 4, Nome = "Sobremesa" }
            };
            var repositoryMock = new Mock<ICategoriaRepository>();
            repositoryMock.Setup(m => m.GetAll()).ReturnsAsync(dados);

            //Act
            var resultado = await (repositoryMock.Object).GetAll();

            // Assert
            repositoryMock.Verify(m => m.GetAll(), Times.Once); // Verifica se o método foi chamado uma vez
            resultado.Should().BeEquivalentTo(dados);
        }

        [Fact]
        public async Task devePermitirRegistrarCategoria()
        {
            //Arrange 
            var categoria = new Categoria() { Nome = "Categoria Teste" };
            var novoCodigo = 5;
            var repositoryMock = new Mock<ICategoriaRepository>();
            repositoryMock.Setup(m => m.Create(categoria)).ReturnsAsync(novoCodigo);

            //Act
            var resultado = await (repositoryMock.Object).Create(categoria);

            // Assert
            repositoryMock.Verify(m => m.Create(categoria), Times.Once); // Verifica se o método foi chamado uma vez
            resultado.Should().Be(novoCodigo);
        }

        [Fact]
        public async Task devePermitirConsultarUmaCategoria()
        {
            //Arrange 
            var dados = new Categoria() { Id = 2, Nome = "Acompanhamento" };
            var codigoBuscado = 2;
            var repositoryMock = new Mock<ICategoriaRepository>();
            repositoryMock.Setup(m => m.GetById(codigoBuscado)).ReturnsAsync(dados);

            //Act
            var resultado = await (repositoryMock.Object).GetById(codigoBuscado);

            // Assert
            repositoryMock.Verify(m => m.GetById(codigoBuscado), Times.Once); // Verifica se o método foi chamado uma vez
            resultado.Should().BeEquivalentTo(dados);
        }

        [Fact]
        public async Task devePermitirApagarUmaCategoria()
        {
            //Arrange 
            var codigoApagado = 3;
            var repositoryMock = new Mock<ICategoriaRepository>();
            repositoryMock.Setup(m => m.Delete(codigoApagado)).ReturnsAsync(true);

            //Act
            var resultado = await (repositoryMock.Object).Delete(codigoApagado);

            // Assert
            repositoryMock.Verify(m => m.Delete(codigoApagado), Times.Once); // Verifica se o método foi chamado uma vez
            resultado.Should().Be(true);
        }

        [Fact]
        public async Task devePermitirAtualizarUmaCategoria()
        {
            //Arrange 
            var categoria = new Categoria() { Nome = "Categoria Teste" };
            var repositoryMock = new Mock<ICategoriaRepository>();
            repositoryMock.Setup(m => m.Update(categoria)).ReturnsAsync(true);

            //Act
            var resultado = await (repositoryMock.Object).Update(categoria);

            // Assert
            repositoryMock.Verify(m => m.Update(categoria), Times.Once); // Verifica se o método foi chamado uma vez
            resultado.Should().Be(true);
        }
    }
}
