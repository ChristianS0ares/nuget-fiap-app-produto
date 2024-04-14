using Xunit;
using nuget_fiap_app_produto_common.Models;
using nuget_fiap_app_produto_repository;
using nuget_fiap_app_produto_repository.DB;

namespace nuget_fiap_app_produto_test.Repository
{
    public class ProdutoRepositoryIT
    {
        public ProdutoRepositoryIT()
        {

        }

        [Fact]
        public async Task devePermitirRegistrarProduto()
        {
            var session = new RepositoryDB();
            var repository = new ProdutoRepository(session);

            var novoProduto = new Produto
            {
                Id = 0,
                Nome = "Produto Teste",
                Preco = 99.99M,
                Descricao = "Descrição do produto teste",
                UrlImagem = "url_da_imagem",
                Categoria = new Categoria { Id = 1, Nome = "Lanche" } // Assumindo que a categoria com ID 1 existe
            };

            var idNovoProduto = await repository.Create(novoProduto);

            var produtoInserido = await repository.GetById(idNovoProduto);
            Assert.NotNull(produtoInserido);
            Assert.Equal("Produto Teste", produtoInserido.Nome);
        }

        [Fact]
        public async Task deveRecuperarTodosOsProdutos()
        {
            var session = new RepositoryDB();
            var repository = new ProdutoRepository(session);

            var produtos = await repository.GetAll();
            Assert.NotNull(produtos);
            Assert.True(produtos.Any()); // Presume que a tabela não está vazia
        }

        [Fact]
        public async Task deveRecuperarProdutoPorId()
        {
            var session = new RepositoryDB();
            var repository = new ProdutoRepository(session);

            // Presume-se que o produto com ID 1 existe
            var produto = await repository.GetById(1);
            Assert.NotNull(produto);
            Assert.Equal(1, produto.Id);
        }

        [Fact]
        public async Task devePermitirAtualizarProduto()
        {
            var session = new RepositoryDB();
            var repository = new ProdutoRepository(session);

            // Presume-se que o produto com ID 1 existe
            var produtoParaAtualizar = new Produto
            {
                Id = 1,
                Nome = "Produto Atualizado",
                Preco = 120.50M,
                Descricao = "Descrição atualizada",
                UrlImagem = "nova_url_imagem",
                Categoria = new Categoria { Id = 1, Nome = "Lanche" }
            };

            var sucesso = await repository.Update(produtoParaAtualizar);

            var produtoAtualizado = await repository.GetById(1);
            Assert.True(sucesso);
            Assert.NotNull(produtoAtualizado);
            Assert.Equal("Produto Atualizado", produtoAtualizado.Nome);
        }

        [Fact]
        public async Task devePermitirExcluirProduto()
        {
            var session = new RepositoryDB();
            var repository = new ProdutoRepository(session);

            // Inserir um novo produto para deletar
            var novoProduto = new Produto
            {
                Id = 0,
                Nome = "Produto Para Deletar",
                Preco = 10.00M,
                Descricao = "Deletar este produto",
                UrlImagem = "url_deletar",
                Categoria = new Categoria { Id = 1, Nome = "Lanche" }
            };
            var idProduto = await repository.Create(novoProduto);

            var sucesso = await repository.Delete(idProduto);
            var produtoDeletado = await repository.GetById(idProduto);

            Assert.True(sucesso);
            Assert.Null(produtoDeletado);
        }
    }
}
