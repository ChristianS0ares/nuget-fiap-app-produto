using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using nuget_fiap_app_produto_common.Models;
using System.Net.Http.Json;
using TechTalk.SpecFlow;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace nuget_fiap_app_produto_test.BDD
{
    [Binding]
    public class ProdutoSteps
    {
        private readonly HttpClient _client;
        private HttpResponseMessage _response;
        private Produto _produtoCriado;
        private readonly string _baseUrl = "/produto";

        public ProdutoSteps(WebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();
        }

        [Given(@"que eu adicionei um produto com o nome ""(.*)"" e preço ""(.*)""")]
        public async Task DadoQueEuAdicioneiUmProdutoComONomeEPreco(string nome, decimal preco)
        {
            var novoProduto = new Produto (){ Id =0, Nome = nome, Preco = preco, Descricao = "", Categoria = new Categoria() {Id = 1, Nome = "Lanche" }, UrlImagem = "" };
            _response = await _client.PostAsJsonAsync(_baseUrl, novoProduto);
            _response.EnsureSuccessStatusCode();

            var locationHeader = _response.Headers.Location.ToString();
            if (string.IsNullOrEmpty(locationHeader))
                throw new InvalidOperationException("Location header is missing in the POST response.");

            var produtoId = locationHeader.Split('/').Last();
            _response = await _client.GetAsync($"{_baseUrl}/{produtoId}");
            _response.EnsureSuccessStatusCode();

            _produtoCriado = await _response.Content.ReadFromJsonAsync<Produto>();
            _produtoCriado.Should().NotBeNull();
            _produtoCriado.Nome.Should().Be(nome);
        }

        [Given(@"que eu adicionei um novo produto com o nome ""(.*)""")]
        public async Task DadoQueEuAdicioneiUmProdutoComONome(string nome)
        {
            var novoProduto = new Produto() { Id = 0, Nome = nome, Preco = 0, Descricao = "", Categoria = new Categoria() { Id = 1, Nome = "Lanche" }, UrlImagem = "" };
            _response = await _client.PostAsJsonAsync(_baseUrl, novoProduto);
            _response.EnsureSuccessStatusCode();

            var locationHeader = _response.Headers.Location.ToString();
            if (string.IsNullOrEmpty(locationHeader))
                throw new InvalidOperationException("Location header is missing in the POST response.");

            var produtoId = locationHeader.Split('/').Last();
            _response = await _client.GetAsync($"{_baseUrl}/{produtoId}");
            _response.EnsureSuccessStatusCode();

            _produtoCriado = await _response.Content.ReadFromJsonAsync<Produto>();
            _produtoCriado.Should().NotBeNull();
            _produtoCriado.Nome.Should().Be(nome);
        }

        [Given(@"que eu adicionei o produto com o nome ""(.*)""")]
        public async Task DadoQueEuAdicioneiOProdutoComONome(string nome)
        {
            var novoProduto = new Produto() { Id = 0, Nome = nome, Preco = 0, Descricao = "", Categoria = new Categoria() { Id = 1, Nome = "Lanche" }, UrlImagem = "" };
            _response = await _client.PostAsJsonAsync(_baseUrl, novoProduto);
            _response.EnsureSuccessStatusCode();

            var locationHeader = _response.Headers.Location.ToString();
            if (string.IsNullOrEmpty(locationHeader))
                throw new InvalidOperationException("Location header is missing in the POST response.");

            var produtoId = locationHeader.Split('/').Last();
            _response = await _client.GetAsync($"{_baseUrl}/{produtoId}");
            _response.EnsureSuccessStatusCode();

            _produtoCriado = await _response.Content.ReadFromJsonAsync<Produto>();
            _produtoCriado.Should().NotBeNull();
            _produtoCriado.Nome.Should().Be(nome);
        }

        [When(@"eu solicito a lista de produtos")]
        public async Task QuandoEuSolicitoAListaDeProdutos()
        {
            _response = await _client.GetAsync(_baseUrl);
        }

        [When(@"que eu adiciono um produto com o nome ""(.*)"" e preço ""(.*)""")]
        public async Task QuandoEuAdicionoUmProdutoComONomeEPreco(string nome, decimal preco)
        {
            var novoProduto = new Produto() { Id = 0, Nome = nome, Preco = preco, Descricao = "", Categoria = new Categoria() { Id = 1, Nome = "Lanche" }, UrlImagem = "" };
            _response = await _client.PostAsJsonAsync(_baseUrl, novoProduto);
        }

        [When(@"eu solicito o produto pelo seu ID")]
        public async Task QuandoEuSolicitoOProdutoPeloSeuID()
        {
            _response = await _client.GetAsync($"{_baseUrl}/{_produtoCriado.Id}");
        }

        [When(@"eu atualizo o produto ""(.*)"" para ter o nome ""(.*)"" e preço ""(.*)""")]
        public async Task QuandoEuAtualizoOProdutoParaTerONomeEPreco(string nomeOriginal, string novoNome, decimal novoPreco)
        {
            var produtoAtualizado = new Produto() { Id = 0, Nome = novoNome, Preco = novoPreco, Descricao = "", Categoria = new Categoria() { Id = 1, Nome = "Lanche" }, UrlImagem = "" };
            _response = await _client.PutAsJsonAsync($"{_baseUrl}/{_produtoCriado.Id}", produtoAtualizado);
            _response.EnsureSuccessStatusCode();

            var responseAtualizacao = await _client.GetAsync($"{_baseUrl}/{_produtoCriado.Id}");
            responseAtualizacao.EnsureSuccessStatusCode();
            var produtoAtualizadoVerificado = await responseAtualizacao.Content.ReadFromJsonAsync<Produto>();
            produtoAtualizadoVerificado.Nome.Should().Be(novoNome);
        }

        [When(@"eu excluo o produto ""(.*)""")]
        public async Task QuandoEuExcluoOProduto(string nome)
        {
            _response = await _client.DeleteAsync($"{_baseUrl}/{_produtoCriado.Id}");
        }

        [Then(@"eu devo receber uma lista contendo o produto ""(.*)""")]
        public async Task EntaoEuDevoReceberUmaListaContendo(string nome)
        {
            _response.EnsureSuccessStatusCode();
            var produtos = await _response.Content.ReadFromJsonAsync<List<Produto>>();
            produtos.Should().Contain(produto => produto.Nome == nome);
        }

        [Then(@"o produto ""(.*)"" deve ser adicionado com sucesso")]
        public void EntaoOProdutoDeveSerAdicionadoComSucesso(string nome)
        {
            _response.StatusCode.Should().Be(System.Net.HttpStatusCode.Created);
        }

        [Then(@"eu devo receber o produto ""(.*)""")]
        public async Task EntaoEuDevoReceberOProduto(string nome)
        {
            _response.EnsureSuccessStatusCode();
            var produto = await _response.Content.ReadFromJsonAsync<Produto>();
            produto.Nome.Should().Be(nome);
        }

        [Then(@"eu devo receber o produto com o nome ""(.*)""")]
        public async Task EntaoEuDevoReceberOProdutoComONome(string nome)
        {
            _response.EnsureSuccessStatusCode();
            var produto = await _response.Content.ReadFromJsonAsync<Produto>();
            produto.Nome.Should().Be(nome);
        }

        [Then(@"o produto ""(.*)"" não deve mais existir")]
        public void EntaoOProdutoNaoDeveMaisExistir(string nome)
        {
            _response.StatusCode.Should().Be(System.Net.HttpStatusCode.NoContent);
        }
    }
}
