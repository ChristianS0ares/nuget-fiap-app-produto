using nuget_fiap_app_produto.Services;
using nuget_fiap_app_produto_common.Interfaces.Services;
using nuget_fiap_app_produto_common.Models;
using nuget_fiap_app_produto_repository.DB;
using nuget_fiap_app_produto_repository.Interface;
using nuget_fiap_app_produto_repository;
using Xunit;
using FluentAssertions;

namespace nuget_fiap_app_produto_test.Service
{
    public class CategoriaServiceIT
    {
        private CategoriaService _categoriaService;
        private RepositoryDB _repositoryDB; 
        private IProdutoService _produtoService; 
        private IUnitOfWork _unitOfWork; 

        public CategoriaServiceIT()
        {
            _repositoryDB = new RepositoryDB();
            _unitOfWork = new UnitOfWork(_repositoryDB);
            _produtoService = new ProdutoService(new ProdutoRepository(_repositoryDB), _unitOfWork);
            _categoriaService = new CategoriaService(new CategoriaRepository(_repositoryDB), _produtoService, _unitOfWork);
        }

        [Fact]
        [Trait("Category", "Integration")]
        public async Task devePermitirCriarCategoria()
        {
            var novaCategoria = new Categoria { Nome = "Categoria Teste" };
            var categoriaId = await _categoriaService.Create(novaCategoria);

            categoriaId.Should().BeGreaterThan(0);

            var categoriaCriada = await _categoriaService.GetById(categoriaId);

            categoriaCriada.Should().NotBeNull();
            categoriaCriada.Nome.Should().Be("Categoria Teste");
        }

        [Fact]
        [Trait("Category", "Integration")]
        public async Task devePermitirExcluirCategoria()
        {
            var categoriaTeste = new Categoria { Nome = "Categoria Para Excluir" };
            var categoriaId = await _categoriaService.Create(categoriaTeste);

            categoriaId.Should().BeGreaterThan(0, "Falha ao inserir categoria de teste.");

            var resultadoExclusao = await _categoriaService.Delete(categoriaId);

            resultadoExclusao.Should().BeTrue("A categoria não foi excluída como esperado.");

            var categoriaExcluida = await _categoriaService.GetById(categoriaId);

            categoriaExcluida.Should().BeNull();
        }

        [Fact]
        [Trait("Category", "Integration")]
        public async Task deveRecuperarTodasCategorias()
        {
            await _categoriaService.Create(new Categoria { Nome = "Categoria Teste 1" });
            await _categoriaService.Create(new Categoria { Nome = "Categoria Teste 2" });

            var categorias = await _categoriaService.GetAll();

            categorias.Should().NotBeNull();
            categorias.Should().HaveCountGreaterOrEqualTo(2);
            categorias.Select(c => c.Nome).Should().Contain(new[] { "Categoria Teste 1", "Categoria Teste 2" });
        }

        [Fact]
        [Trait("Category", "Integration")]
        public async Task deveRecuperarCategoriaPorId()
        {
            var categoriaTeste = new Categoria { Nome = "Categoria Para Recuperar" };
            var categoriaId = await _categoriaService.Create(categoriaTeste);

            var categoria = await _categoriaService.GetById(categoriaId);

            categoria.Should().NotBeNull();
            categoria.Nome.Should().Be("Categoria Para Recuperar");
        }

        [Fact]
        [Trait("Category", "Integration")]
        public async Task devePermitirAtualizarCategoria()
        {
            var categoriaTeste = new Categoria { Nome = "Categoria Antes da Atualização" };
            var categoriaId = await _categoriaService.Create(categoriaTeste);

            var categoriaAtualizada = new Categoria { Nome = "Categoria Após Atualização" };
            var resultadoAtualizacao = await _categoriaService.Update(categoriaAtualizada, categoriaId);

            var categoria = await _categoriaService.GetById(categoriaId);

            resultadoAtualizacao.Should().BeTrue();
            categoria.Should().NotBeNull();
            categoria.Nome.Should().Be("Categoria Após Atualização");
        }

    }
}
