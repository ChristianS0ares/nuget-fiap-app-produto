using FluentAssertions;
using Moq;
using nuget_fiap_app_produto.Services;
using nuget_fiap_app_produto_common.Models;
using nuget_fiap_app_produto_test.Repository;
using nuget_fiap_app_produto_test.Repository.DB;
using Xunit;

namespace nuget_fiap_app_produto_test.Service
{
    public class ProdutoServiceTest
    {

        [Fact]
        [Trait("Category", "Unit")]
        public async Task devePermitirConsultarTodosOsProdutos()
        {
            // Arrange
            var resultadoEsperadoDeLinhas = 4;
            var mockProdutoRepository = new ProdutoRepositoryMock();
            var mockUnitOfWork = new UnitOfWorkMock();
            var produtoService = new ProdutoService(mockProdutoRepository, mockUnitOfWork);

            // Act
            var resultado = await produtoService.GetAll();

            // Assert
            resultado.Count().Should().Be(resultadoEsperadoDeLinhas);
        }

        [Fact]
        [Trait("Category", "Unit")]
        public async Task devePermitirCriarUmProduto()
        {
            // Arrange
            var novoProduto = new Produto() { Id = 5, Nome = "Pizza", Preco = 14.99M, Descricao = "Pizza deliciosa", UrlImagem = "https://example.com/imagem2.jpg", Categoria = new Categoria() { Id = 1, Nome = "Lanche" } };
            var mockProdutoRepository = new ProdutoRepositoryMock();
            var mockUnitOfWork = new UnitOfWorkMock();
            var produtoService = new ProdutoService(mockProdutoRepository, mockUnitOfWork);

            // Act
            var resultado = await produtoService.Create(novoProduto);

            // Assert
            resultado.Should().Be(5);
        }

        [Fact]
        [Trait("Category", "Unit")]
        public async Task devePermitirApagarUmProduto()
        {
            // Arrange
            var id = 1;
            var mockProdutoRepository = new ProdutoRepositoryMock();
            var mockUnitOfWork = new UnitOfWorkMock();
            var produtoService = new ProdutoService(mockProdutoRepository, mockUnitOfWork);

            // Act
            var resultado = await produtoService.Delete(1);

            // Assert
            resultado.Should().Be(true);

        }

        [Fact]
        [Trait("Category", "Unit")]
        public async Task devePermitirListarProdutoPelaCategoria()
        {
            // Arrange
            var idCategoria = 2;
            var mockProdutoRepository = new ProdutoRepositoryMock();
            var mockUnitOfWork = new UnitOfWorkMock();
            var produtoService = new ProdutoService(mockProdutoRepository, mockUnitOfWork);

            // Act
            var resultado = await produtoService.GetByIdCategoria(idCategoria);

            // Assert
            resultado.Count().Should().Be(1);
        }

        [Fact]
        [Trait("Category", "Unit")]
        public async Task devePermitirAtualizarUmProduto()
        {
            // Arrange
            var id = 1;
            var produto = new Produto() { Id = 0, Nome = "Pizza", Preco = 14.99M, Descricao = "Pizza deliciosa", UrlImagem = "https://example.com/imagem2.jpg", Categoria = new Categoria() { Id = 1, Nome = "Lanche" } };
            var mockProdutoRepository = new ProdutoRepositoryMock();
            var mockUnitOfWork = new UnitOfWorkMock();
            var produtoService = new ProdutoService(mockProdutoRepository, mockUnitOfWork);

            // Act
            var resultado = await produtoService.Update(produto, 1);

            // Assert
            resultado.Should().Be(true);
        }
    }
}
