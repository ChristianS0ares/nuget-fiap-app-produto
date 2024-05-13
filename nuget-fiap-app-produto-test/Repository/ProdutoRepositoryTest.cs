using FluentAssertions;
using Moq;
using nuget_fiap_app_produto_common.Interfaces.Repository;
using nuget_fiap_app_produto_common.Models;
using Xunit;

namespace nuget_fiap_app_produto_test.Repository
{
    public class ProdutoRepositoryTest
    {
        [Fact]
        [Trait("Category", "Unit")]
        public async Task devePermitirConsultarTodosOsProdutos()
        {
            // Arrange
            var dados = new List<Produto>() {
                new Produto() { Id = 1, Nome = "Hambúrguer", Preco = 9.99M, Descricao = "Hambúrguer delicioso", UrlImagem = "https://example.com/imagem1.jpg", Categoria = new Categoria() { Id = 1, Nome = "Lanche" }},
                new Produto() { Id = 2, Nome = "Batata Frita", Preco = 4.99M, Descricao = "Batata frita crocante", UrlImagem = "https://example.com/imagem2.jpg", Categoria = new Categoria() { Id = 2, Nome = "Acompanhamento" }},
                new Produto() { Id = 3, Nome = "Coca-Cola", Preco = 2.99M, Descricao = "Refrigerante refrescante", UrlImagem = "https://example.com/imagem3.jpg", Categoria = new Categoria() { Id = 3, Nome = "Bebida" }},
                new Produto() { Id = 4, Nome = "Sundae de Chocolate", Preco = 3.99M, Descricao = "Sobremesa de chocolate", UrlImagem = "https://example.com/imagem4.jpg", Categoria = new Categoria() { Id = 4, Nome = "Sobremesa" }}
            };
            var repositoryMock = new Mock<IProdutoRepository>();
            repositoryMock.Setup(m => m.GetAll()).ReturnsAsync(dados);

            // Act
            var resultado = await repositoryMock.Object.GetAll();

            // Assert
            resultado.Should().BeEquivalentTo(dados);
            repositoryMock.Verify(m => m.GetAll(), Times.Once);

        }

        [Fact]
        [Trait("Category", "Unit")]
        public async Task devePermitirConsultarUmProduto()
        {
            // Arrange
            var produtoEsperado = new Produto() { Id = 1, Nome = "Hambúrguer", Preco = 9.99M, Descricao = "Hambúrguer delicioso", UrlImagem = "https://example.com/imagem1.jpg", Categoria = new Categoria() { Id = 1, Nome = "Lanche" } };
            var repositoryMock = new Mock<IProdutoRepository>();
            repositoryMock.Setup(m => m.GetById(produtoEsperado.Id)).ReturnsAsync(produtoEsperado);

            // Act
            var resultado = await repositoryMock.Object.GetById(produtoEsperado.Id);

            // Assert
            resultado.Should().BeEquivalentTo(produtoEsperado);
            repositoryMock.Verify(m => m.GetById(produtoEsperado.Id), Times.Once);

        }

        [Fact]
        [Trait("Category", "Unit")]
        public async Task devePermitirConsultarTodosOsProdutosPelaCategoria()
        {
            // Arrange
            int idCategoria = 1; // Exemplo de ID de categoria
            var produtosEsperados = new List<Produto>() {
                new Produto() { Id = 1, Nome = "Hambúrguer", Preco = 9.99M, Descricao = "Hambúrguer delicioso", UrlImagem = "https://example.com/imagem1.jpg", Categoria = new Categoria() { Id = 1, Nome = "Lanche" }},
                new Produto() { Id = 2, Nome = "Pizza", Preco = 14.99M, Descricao = "Pizza deliciosa", UrlImagem = "https://example.com/imagem2.jpg", Categoria = new Categoria() { Id = 1, Nome = "Lanche" }},
            };
            var repositoryMock = new Mock<IProdutoRepository>();
            repositoryMock.Setup(m => m.GetByIdCategoria(idCategoria)).ReturnsAsync(produtosEsperados);

            // Act
            var resultado = await repositoryMock.Object.GetByIdCategoria(idCategoria);

            // Assert
            resultado.Should().BeEquivalentTo(produtosEsperados);
            repositoryMock.Verify(m => m.GetByIdCategoria(idCategoria), Times.Once);

        }

        [Fact]
        [Trait("Category", "Unit")]
        public async Task devePermitirRegistrarUmProduto()
        {
            // Arrange
            var produtoNovo = new Produto() { Id = 1, Nome = "Hambúrguer", Preco = 9.99M, Descricao = "Hambúrguer delicioso", UrlImagem = "https://example.com/imagem1.jpg", Categoria = new Categoria() { Id = 1, Nome = "Lanche" } };
            int idEsperado = 9; // Supondo que o novo produto receba o ID 
            var repositoryMock = new Mock<IProdutoRepository>();
            repositoryMock.Setup(m => m.Create(produtoNovo)).ReturnsAsync(idEsperado);

            // Act
            var idResultado = await repositoryMock.Object.Create(produtoNovo);

            // Assert
            idResultado.Should().Be(idEsperado);
            repositoryMock.Verify(m => m.Create(produtoNovo), Times.Once);

        }

        [Fact]
        [Trait("Category", "Unit")]
        public async Task devePermitirApagarUmProduto()
        {
            // Arrange
            int idParaApagar = 2; // Exemplo de ID de produto para apagar
            var repositoryMock = new Mock<IProdutoRepository>();
            repositoryMock.Setup(m => m.Delete(idParaApagar)).ReturnsAsync(true);

            // Act
            var resultado = await repositoryMock.Object.Delete(idParaApagar);

            // Assert
            resultado.Should().BeTrue();
            repositoryMock.Verify(m => m.Delete(idParaApagar), Times.Once);
        }

        [Fact]
        [Trait("Category", "Unit")]
        public async Task devePermitirAtualizarUmProduto()
        {
            // Arrange
            var produtoAtualizado = new Produto() { Id = 1, Nome = "Hambúrguer", Preco = 9.99M, Descricao = "Hambúrguer delicioso", UrlImagem = "https://example.com/imagem1.jpg", Categoria = new Categoria() { Id = 1, Nome = "Lanche" } };
            var repositoryMock = new Mock<IProdutoRepository>();
            repositoryMock.Setup(m => m.Update(produtoAtualizado)).ReturnsAsync(true);

            // Act
            var resultado = await repositoryMock.Object.Update(produtoAtualizado);

            // Assert
            resultado.Should().BeTrue();
            repositoryMock.Verify(m => m.Update(produtoAtualizado), Times.Once);

        }
    }
}
