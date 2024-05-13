using nuget_fiap_app_produto_common.Interfaces.Repository;
using nuget_fiap_app_produto_common.Models;

namespace nuget_fiap_app_produto_test.Repository
{
    public class ProdutoRepositoryMock : IProdutoRepository
    {
        private List<Produto> dados;

        public ProdutoRepositoryMock()
        {
            dados = new List<Produto>() {
                new Produto() { Id = 1, Nome = "Hambúrguer", Preco = 9.99M, Descricao = "Hambúrguer delicioso", UrlImagem = "https://example.com/imagem1.jpg", Categoria = new Categoria() { Id = 1, Nome = "Lanche" }},
                new Produto() { Id = 2, Nome = "Batata Frita", Preco = 4.99M, Descricao = "Batata frita crocante", UrlImagem = "https://example.com/imagem2.jpg", Categoria = new Categoria() { Id = 2, Nome = "Acompanhamento" }},
                new Produto() { Id = 3, Nome = "Coca-Cola", Preco = 2.99M, Descricao = "Refrigerante refrescante", UrlImagem = "https://example.com/imagem3.jpg", Categoria = new Categoria() { Id = 3, Nome = "Bebida" }},
                new Produto() { Id = 4, Nome = "Sundae de Chocolate", Preco = 3.99M, Descricao = "Sobremesa de chocolate", UrlImagem = "https://example.com/imagem4.jpg", Categoria = new Categoria() { Id = 4, Nome = "Sobremesa" }}
            };
        }

        public async Task<int> Create(Produto produto)
        {
            if (produto == null)
                throw new ArgumentNullException(nameof(produto));

            var nextId = dados.Any() ? dados.Max(p => p.Id) + 1 : 1;
            produto.Id = nextId;
            dados.Add(produto);
            return await Task.FromResult(nextId);
        }

        public async Task<bool> Delete(int id)
        {
            var produto = dados.FirstOrDefault(p => p.Id == id);
            if (produto == null)
                return await Task.FromResult(false);

            dados.Remove(produto);
            return await Task.FromResult(true);
        }

        public async Task<IEnumerable<Produto>> GetAll()
        {
            return await Task.FromResult(dados);
        }

        public async Task<Produto> GetById(int id)
        {
            var produto = dados.FirstOrDefault(p => p.Id == id);
            return await Task.FromResult(produto);
        }

        public async Task<IEnumerable<Produto>> GetByIdCategoria(int idCategoria)
        {
            var produtos = dados.Where(p => p.Categoria.Id == idCategoria);
            return await Task.FromResult(produtos);
        }

        public async Task<bool> Update(Produto produto)
        {
            if (produto == null)
                throw new ArgumentNullException(nameof(produto));

            var existingProduto = dados.FirstOrDefault(p => p.Id == produto.Id);
            if (existingProduto == null)
                return await Task.FromResult(false);

            existingProduto.Nome = produto.Nome;
            existingProduto.Preco = produto.Preco;
            existingProduto.Descricao = produto.Descricao;
            existingProduto.UrlImagem = produto.UrlImagem;
            existingProduto.Categoria = produto.Categoria;

            return await Task.FromResult(true);
        }
    }
}
