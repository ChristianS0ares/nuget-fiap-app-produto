using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Newtonsoft.Json;
using nuget_fiap_app_produto_common.Models;
using System.Net;
using System.Text;
using Xunit;

namespace nuget_fiap_app_produto_test.Controller
{
    public class CategoriaControllerIT : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;

        public CategoriaControllerIT(WebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        [Trait("Category", "Integration")]
        public async Task devePermitirBuscarTodos()
        {
            var response = await _client.GetAsync("/Categoria");
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        [Trait("Category", "Integration")]
        public async Task deveRetornarNaoEncontradoQuandoIdInexistente()
        {
            var response = await _client.GetAsync("/Categoria/999");

            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        [Trait("Category", "Integration")]
        public async Task devePermitirCriarUmaNovaCategoria()
        {
            var novaCategoria = new Categoria { Nome = "Nova Categoria" };
            var content = new StringContent(JsonConvert.SerializeObject(novaCategoria), Encoding.UTF8, "application/json");

            var response = await _client.PostAsync("/Categoria", content);

            response.StatusCode.Should().Be(HttpStatusCode.Created);
            // Verifica se a URL do recurso criado é retornada
            response.Headers.Location.Should().NotBeNull();
        }

        [Fact]
        [Trait("Category", "Integration")]
        public async Task deveRetornarNotFoundAoAtualizarUmIdInexistente()
        {
            var categoriaAtualizada = new Categoria { Nome = "Categoria Atualizada" };
            var content = new StringContent(JsonConvert.SerializeObject(categoriaAtualizada), Encoding.UTF8, "application/json");

            var response = await _client.PutAsync("/Categoria/999", content);

            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        [Trait("Category", "Integration")]
        public async Task devePermitirExcluirUmaCategoria()
        {
            // Assumindo que a categoria com ID 1 existe
            var response = await _client.DeleteAsync("/Categoria/5");

            response.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }

        [Fact]
        [Trait("Category", "Integration")]
        public async Task naoDevePermitirExcluirUmaCategoriaInexistente()
        {
            // Assumindo que a categoria com ID -1 não existe
            var response = await _client.DeleteAsync("/Categoria/-1");

            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }
    }
}
