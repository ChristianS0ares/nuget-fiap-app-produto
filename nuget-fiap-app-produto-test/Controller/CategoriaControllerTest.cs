using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;
using FluentAssertions;
using nuget_fiap_app_produto_common.Interfaces.Services;
using nuget_fiap_app_produto_common.Models;
using API.Controllers; // Substitua pela namespace correta do seu CategoriaController

namespace nuget_fiap_app_produto_test.Controller
{
    public class CategoriaControllerTest
    {
        private readonly Mock<ICategoriaService> _categoriaServiceMock;
        private readonly CategoriaController _controller;

        public CategoriaControllerTest()
        {
            _categoriaServiceMock = new Mock<ICategoriaService>();
            _controller = new CategoriaController(_categoriaServiceMock.Object);
        }

        [Fact]
        public async Task GetAll_DeveRetornar200OK_QuandoCategoriasExistirem()
        {
            // Arrange
            var categorias = new List<Categoria>
            {
                new Categoria { Id = 1, Nome = "Lanche" },
                new Categoria { Id = 2, Nome = "Bebida" }
            };

            _categoriaServiceMock.Setup(service => service.GetAll()).ReturnsAsync(categorias);

            // Act
            var result = await _controller.GetAll();

            // Assert
            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
            var returnedCategorias = okResult.Value.Should().BeAssignableTo<IEnumerable<Categoria>>().Subject;

            returnedCategorias.Should().HaveCount(categorias.Count);
        }

        [Fact]
        public async Task GetById_DeveRetornar200OK_QuandoCategoriaExistir()
        {
            // Arrange
            var categoria = new Categoria { Id = 1, Nome = "Lanche" };
            _categoriaServiceMock.Setup(service => service.GetById(1)).ReturnsAsync(categoria);

            // Act
            var result = await _controller.GetById(1);

            // Assert
            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
            var returnedCategoria = okResult.Value.Should().BeEquivalentTo(categoria);
        }

        [Fact]
        public async Task Post_DeveRetornar201Created_QuandoCategoriaForCriada()
        {
            // Arrange
            var categoria = new Categoria { Nome = "Sobremesa" };
            _categoriaServiceMock.Setup(service => service.Create(categoria)).ReturnsAsync(3); // Suponha que o ID 3 seja atribuído

            // Act
            var result = await _controller.Post(categoria);

            // Assert
            var createdAtRouteResult = result.Should().BeOfType<CreatedAtRouteResult>().Subject;
            createdAtRouteResult.RouteName.Should().Be("CategoriaPorId");
            createdAtRouteResult.RouteValues["id"].Should().Be(3);
        }

        [Fact]
        public async Task Put_ShouldReturn200OK_WhenCategoriaIsUpdated()
        {
            // Arrange
            var categoria = new Categoria { Id = 1, Nome = "Lanche Atualizado" };
            _categoriaServiceMock.Setup(service => service.Update(categoria, categoria.Id)).ReturnsAsync(true);

            // Act
            var result = await _controller.Put(categoria.Id, categoria);

            // Assert
            result.Should().BeOfType<OkResult>();
        }

        [Fact]
        public async Task Delete_DeveRetornar204NoContent_QuandoCategoriaForDeletada()
        {
            // Arrange
            _categoriaServiceMock.Setup(service => service.Delete(1)).ReturnsAsync(true);

            // Act
            var result = await _controller.Delete(1);

            // Assert
            result.Should().BeOfType<NoContentResult>();
        }
    }
}
