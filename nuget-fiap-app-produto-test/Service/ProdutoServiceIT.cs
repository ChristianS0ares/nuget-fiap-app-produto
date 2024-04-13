using Xunit;
using FluentAssertions;
using nuget_fiap_app_produto.Services;
using nuget_fiap_app_produto_common.Models;
using nuget_fiap_app_produto_repository;
using nuget_fiap_app_produto_repository.DB;
using nuget_fiap_app_produto_repository.Interface;

namespace nuget_fiap_app_produto_test.Service
{
    public class ProdutoServiceIT
    {
        private ProdutoService _produtoService;
        private RepositoryDB _repositoryDB;
        private IUnitOfWork _unitOfWork;

        public ProdutoServiceIT()
        {
            _repositoryDB = new RepositoryDB(); 
            _unitOfWork = new UnitOfWork(_repositoryDB);
            _produtoService = new ProdutoService(new ProdutoRepository(_repositoryDB), _unitOfWork);
        }

        [Fact]
        public async Task devePermitirCriarProduto()
        {
            var novoProduto = new Produto
            {
                Id = 0,
                Nome = "Produto Teste",
                Preco = 100.50M,
                Descricao = "Descrição do Produto Teste",
                UrlImagem = "url_para_imagem",
                Categoria = new Categoria { Id = 1, Nome = "Lanche" } 
            };
            var produtoId = await _produtoService.Create(novoProduto);

            produtoId.Should().BeGreaterThan(0);

            var produtoCriado = await _produtoService.GetById(produtoId);

            produtoCriado.Should().NotBeNull();
            produtoCriado.Nome.Should().Be("Produto Teste");
        }

        [Fact]
        public async Task devePermitirExcluirProduto()
        {
            var produtoTeste = new Produto
            {
                Id = 0,
                Nome = "Produto para Excluir",
                Preco = 10.00M,
                Descricao = "Deletar este produto",
                UrlImagem = "url_deletar",
                Categoria = new Categoria { Id = 1, Nome = "Lanche" }
            };
            var produtoId = await _produtoService.Create(produtoTeste);

            var resultadoExclusao = await _produtoService.Delete(produtoId);

            resultadoExclusao.Should().BeTrue("O produto não foi excluído como esperado.");

            var produtoExcluido = await _produtoService.GetById(produtoId);

            produtoExcluido.Should().BeNull();
        }

        [Fact]
        public async Task deveRecuperarTodosOsProdutos()
        {
            await _produtoService.Create(new Produto { Id = 0, Nome = "Produto Teste 1", Preco = 200, Descricao = "Produto 1", UrlImagem = "url1", Categoria = new Categoria { Id = 1, Nome = "Lanche" } });
            await _produtoService.Create(new Produto { Id = 0, Nome = "Produto Teste 2", Preco = 300, Descricao = "Produto 2", UrlImagem = "url2", Categoria = new Categoria { Id = 1, Nome = "Lanche" } });

            var produtos = await _produtoService.GetAll();

            produtos.Should().NotBeNull();
            produtos.Should().HaveCountGreaterOrEqualTo(2);
            produtos.Select(p => p.Nome).Should().Contain(new[] { "Produto Teste 1", "Produto Teste 2" });
        }

        [Fact]
        public async Task deveRecuperarProdutoPorId()
        {
            var produtoTeste = new Produto { Id = 0, Nome = "Produto Para Recuperar", Preco = 100.50M, Descricao = "Teste de Recuperação", UrlImagem = "url_teste", Categoria = new Categoria { Id = 1, Nome = "Lanche" } };
            var produtoId = await _produtoService.Create(produtoTeste);

            var produto = await _produtoService.GetById(produtoId);

            produto.Should().NotBeNull();
            produto.Nome.Should().Be("Produto Para Recuperar");
        }

        [Fact]
        public async Task devePermitirAtualizarProduto()
        {
            var produtoTeste = new Produto { Id = 0, Nome = "Produto Antes da Atualização", Preco = 100.50M, Descricao = "Descrição Antes", UrlImagem = "url_antes", Categoria = new Categoria { Id = 1, Nome = "Lanche" } };
            var produtoId = await _produtoService.Create(produtoTeste);

            var produtoParaAtualizar = new Produto { Id = produtoId, Nome = "Produto Após Atualização", Preco = 150.75M, Descricao = "Descrição Após", UrlImagem = "url_após", Categoria = new Categoria { Id = 1, Nome = "Lanche" } };
            var resultadoAtualizacao = await _produtoService.Update(produtoParaAtualizar, produtoId);

            var produtoAtualizado = await _produtoService.GetById(produtoId);

            resultadoAtualizacao.Should().BeTrue();
            produtoAtualizado.Should().NotBeNull();
            produtoAtualizado.Nome.Should().Be("Produto Após Atualização");
        }
    }
}
