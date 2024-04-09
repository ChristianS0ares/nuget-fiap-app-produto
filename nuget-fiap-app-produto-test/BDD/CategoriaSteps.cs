using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using nuget_fiap_app_produto_common.Models;
using System.Net.Http.Json;
using TechTalk.SpecFlow;

namespace nuget_fiap_app_produto_test.BDD
{
    [Binding]
    public class CategoriaSteps
    {
        private readonly HttpClient _client;
        private HttpResponseMessage _response;
        private dynamic _categoriaCriada; 
        private readonly string _baseUrl = "/categoria";

        public CategoriaSteps(WebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();
        }

        [Given(@"que eu adicionei uma categoria com o nome ""(.*)""")]
        public async Task DadoQueEuAdicioneiUmaCategoriaComONome(string nome)
        {
            var novaCategoria = new { Nome = nome };
            _response = await _client.PostAsJsonAsync(_baseUrl, novaCategoria);
            _response.EnsureSuccessStatusCode();

            _categoriaCriada = await _response.Content.ReadFromJsonAsync<dynamic>();
            _categoriaCriada.Should().NotBeNull();
            _categoriaCriada.nome.Should().Be(nome);
        }

        [When(@"eu solicito a lista de categorias")]
        public async Task QuandoEuSolicitoAListaDeCategorias()
        {
            _response = await _client.GetAsync(_baseUrl);
        }

        [When(@"eu adiciono uma categoria com o nome ""(.*)""")]
        public async Task QuandoEuAdicionoUmaCategoriaComONome(string nome)
        {
            var novaCategoria = new { Nome = nome };
            _response = await _client.PostAsJsonAsync(_baseUrl, novaCategoria);
        }

        [When(@"eu solicito a categoria pelo seu ID")]
        public async Task QuandoEuSolicitoACategoriaPeloSeuID()
        {
            _response = await _client.GetAsync($"{_baseUrl}/{_categoriaCriada.id}");
        }

        [When(@"eu excluo a categoria ""(.*)""")]
        public async Task QuandoEuExcluoACategoria(string nome)
        {
            _response = await _client.DeleteAsync($"{_baseUrl}/{_categoriaCriada.id}");
        }

        [Then(@"eu devo receber uma lista contendo ""(.*)""")]
        public async Task EntaoEuDevoReceberUmaListaContendo(string nome)
        {
            _response.EnsureSuccessStatusCode();
            var categorias = await _response.Content.ReadFromJsonAsync<List<Categoria>>();
            categorias.Should().Contain(categoria => categoria.Nome == nome);
        }

        [Then(@"a categoria ""(.*)"" deve ser adicionada com sucesso")]
        public void EntaoACategoriaDeveSerAdicionadaComSucesso(string nome)
        {
            _response.StatusCode.Should().Be(System.Net.HttpStatusCode.Created);
            _categoriaCriada.nome.Should().Be(nome);
        }

        [Then(@"eu devo receber a categoria ""(.*)""")]
        public async Task EntaoEuDevoReceberACategoria(string nome)
        {
            _response.EnsureSuccessStatusCode();
            var categoria = await _response.Content.ReadFromJsonAsync<dynamic>();
            categoria.nome.Should().Be(nome);
        }

        [Then(@"a categoria ""(.*)"" não deve mais existir")]
        public void EntaoACategoriaNaoDeveMaisExistir(string nome)
        {
            _response.StatusCode.Should().Be(System.Net.HttpStatusCode.NoContent);
        }

        [Then(@"eu atualizo a categoria ""(.*)"" para ter o nome ""(.*)""")]
        public async Task EeuAtualizoACategoriaParaTerONome(string nomeOriginal, string novoNome)
        {
            var categoriaAtualizada = new { Nome = novoNome };
            _response = await _client.PutAsJsonAsync($"{_baseUrl}/{_categoriaCriada.id}", categoriaAtualizada);
            _response.EnsureSuccessStatusCode();

            // Verificando se a atualização foi bem-sucedida
            var responseAtualizacao = await _client.GetAsync($"{_baseUrl}/{_categoriaCriada.id}");
            responseAtualizacao.EnsureSuccessStatusCode();
            var categoriaAtualizadaVerificada = await responseAtualizacao.Content.ReadFromJsonAsync<dynamic>();
            categoriaAtualizadaVerificada.nome.Should().Be(novoNome);
        }
    }
}
