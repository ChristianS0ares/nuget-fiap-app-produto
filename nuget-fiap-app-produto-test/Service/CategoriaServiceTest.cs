using nuget_fiap_app_produto.Services;
using nuget_fiap_app_produto_test.Repository.DB;
using nuget_fiap_app_produto_test.Repository;
using Xunit;
using FluentAssertions;
using nuget_fiap_app_produto_common.Models;

namespace nuget_fiap_app_produto_test.Service
{
    public class CategoriaServiceTest
    {

        [Fact]
        [Trait("Category", "Unit")]
        public async Task devePermitirConsultarTodasAsCategorias()
        {
            // Arrange
            var resultadoEsperadoDeLinhas = 4;
            var mockProdutoRepository = new ProdutoRepositoryMock();
            var mockUnitOfWork = new UnitOfWorkMock();
            var produtoService = new ProdutoService(mockProdutoRepository, mockUnitOfWork);

            var mockCategoriaRepository = new CategoriaRepositoryMock();
            var categoriaService = new CategoriaService(mockCategoriaRepository, produtoService, mockUnitOfWork);

            // Act
            var resultado = await categoriaService.GetAll();

            // Assert
            resultado.Count().Should().Be(resultadoEsperadoDeLinhas);
        }

        [Fact]
        [Trait("Category", "Unit")]
        public async Task devePermitirConsultarUmaCategoria()
        {
            // Arrange
            var id = 4;
            var mockProdutoRepository = new ProdutoRepositoryMock();
            var mockUnitOfWork = new UnitOfWorkMock();
            var produtoService = new ProdutoService(mockProdutoRepository, mockUnitOfWork);

            var mockCategoriaRepository = new CategoriaRepositoryMock();
            var categoriaService = new CategoriaService(mockCategoriaRepository, produtoService, mockUnitOfWork);

            // Act
            var resultado = await categoriaService.GetById(4);

            // Assert
            resultado.Nome.Should().Be("Sobremesa");
        }

        [Fact]
        [Trait("Category", "Unit")]
        public async Task devePermitirCriarUmaCategoria()
        {
            // Arrange
            var novaCategoria = new Categoria() { Id = 5, Nome = "Teste" };
            var mockProdutoRepository = new ProdutoRepositoryMock();
            var mockUnitOfWork = new UnitOfWorkMock();
            var produtoService = new ProdutoService(mockProdutoRepository, mockUnitOfWork);

            var mockCategoriaRepository = new CategoriaRepositoryMock();
            var categoriaService = new CategoriaService(mockCategoriaRepository, produtoService, mockUnitOfWork);

            // Act
            var resultado = await categoriaService.Create(novaCategoria);

            // Assert
            resultado.Should().Be(5);
        }

        [Fact]
        [Trait("Category", "Unit")]
        public async Task devePermitirAtualizarUmaCategoria()
        {
            // Arrange
            var categoria = new Categoria() { Id = 1, Nome = "Teste" };
            var mockProdutoRepository = new ProdutoRepositoryMock();
            var mockUnitOfWork = new UnitOfWorkMock();
            var produtoService = new ProdutoService(mockProdutoRepository, mockUnitOfWork);

            var mockCategoriaRepository = new CategoriaRepositoryMock();
            var categoriaService = new CategoriaService(mockCategoriaRepository, produtoService, mockUnitOfWork);

            // Act
            var resultado = await categoriaService.Update(categoria, 1);

            // Assert
            resultado.Should().Be(true);
        }

        [Fact]
        [Trait("Category", "Unit")]
        public async Task deveRetornarExcecaoAoApagarUmaCategoriaVinculadaAUmProduto()
        {
            // Arrange
            var id = 1;
            var mockProdutoRepository = new ProdutoRepositoryMock();
            var mockUnitOfWork = new UnitOfWorkMock();
            var produtoService = new ProdutoService(mockProdutoRepository, mockUnitOfWork);

            var mockCategoriaRepository = new CategoriaRepositoryMock();
            var categoriaService = new CategoriaService(mockCategoriaRepository, produtoService, mockUnitOfWork);

            // Act & Assert
            await FluentActions.Invoking(() => categoriaService.Delete(id))
                .Should().ThrowAsync<Exception>()
                .WithMessage("Não é possível excluir a categoria devido a restrições de chave estrangeira."); // Verifica se a exceção correta é lançada com a mensagem esperada
        }


        [Fact]
        [Trait("Category", "Unit")]
        public async Task devePermitirApagarUmaCategoria()
        {
            // Arrange
            var novaCategoria = new Categoria() { Id = 5, Nome = "Teste" };
            var mockProdutoRepository = new ProdutoRepositoryMock();
            var mockUnitOfWork = new UnitOfWorkMock();
            var produtoService = new ProdutoService(mockProdutoRepository, mockUnitOfWork);

            var mockCategoriaRepository = new CategoriaRepositoryMock();
            var categoriaService = new CategoriaService(mockCategoriaRepository, produtoService, mockUnitOfWork);

            // Act
            var idNovaCategoria = await categoriaService.Create(novaCategoria);
            var resultado = await categoriaService.Delete(idNovaCategoria);

            // Assert
            resultado.Should().Be(true);
        }
    }
}
