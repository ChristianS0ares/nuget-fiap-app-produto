using Xunit;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using System.Net;
using System.Text;
using nuget_fiap_app_produto_common.Models;

namespace nuget_fiap_app_produto_test.Controller
{
    public class ProdutoControllerIT : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;

        public ProdutoControllerIT(WebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        [Trait("Category", "Integration")]
        public async Task devePermitirBuscarTodosOsProdutos()
        {
            var response = await _client.GetAsync("/Produto");
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        [Trait("Category", "Integration")]
        public async Task devePermitirCriarUmNovoProduto()
        {
            var novoProduto = new Produto
            {
                Id = 0,
                Nome = "Novo Produto",
                Preco = 10.99M,
                Descricao = "Descrição do novo produto",
                UrlImagem = "url_da_imagem",
                Categoria = new Categoria { Id = 1 , Nome = "Lanche" } 
            };
            var content = new StringContent(JsonConvert.SerializeObject(novoProduto), Encoding.UTF8, "application/json");

            var response = await _client.PostAsync("/Produto", content);

            response.StatusCode.Should().Be(HttpStatusCode.Created);
            response.Headers.Location.Should().NotBeNull();
        }

        [Fact]
        [Trait("Category", "Integration")]
        public async Task deveRetornarNotFoundAoAtualizarUmIdInexistente()
        {
            var produtoAtualizado = new Produto
            {
                Id = 0,
                Nome = "Produto Atualizado",
                Preco = 20.50M,
                Descricao = "Descrição atualizada",
                UrlImagem = "url_atualizada",
                Categoria = new Categoria { Id = 1, Nome = "Lanche" }
            };
            var content = new StringContent(JsonConvert.SerializeObject(produtoAtualizado), Encoding.UTF8, "application/json");

            var response = await _client.PutAsync("/Produto/999", content);

            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        [Trait("Category", "Integration")]
        public async Task devePermitirExcluirUmProduto()
        {
            // Assumindo que o produto com ID 2 existe
            var response = await _client.DeleteAsync("/Produto/2");

            response.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }

        [Fact]
        [Trait("Category", "Integration")]
        public async Task deveRetornarNotFoundAoExcluirUmProdutoInexistente()
        {
            var response = await _client.DeleteAsync("/Produto/9999");

            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        [Trait("Category", "Integration")]
        public async Task devePermitirAtualizarProduto()
        {
            var produtoAtualizado = new Produto
            {
                Id = 0,
                Nome = "Produto Atualizado",
                Preco = 50.99M,
                Descricao = "Descrição atualizada",
                UrlImagem = "url_atualizada",
                Categoria = new Categoria { Id = 1, Nome = "Lanche" } 
            };
            var content = new StringContent(JsonConvert.SerializeObject(produtoAtualizado), Encoding.UTF8, "application/json");

            var response = await _client.PutAsync("/Produto/1", content); 

            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        [Trait("Category", "Integration")]
        public async Task deveObterProdutoPorId()
        {
            var response = await _client.GetAsync("/Produto/1"); 

            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var produto = JsonConvert.DeserializeObject<Produto>(await response.Content.ReadAsStringAsync());
            produto.Should().NotBeNull();
            produto.Nome.Should().NotBeNull();
        }
    }
}
