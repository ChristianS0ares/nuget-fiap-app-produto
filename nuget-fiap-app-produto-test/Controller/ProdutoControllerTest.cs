using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;
using FluentAssertions;
using System.Collections.Generic;
using System.Threading.Tasks;
using nuget_fiap_app_produto_common.Interfaces.Services;
using nuget_fiap_app_produto_common.Models;
using nuget_fiap_app_produto.Controllers;
using System;

namespace nuget_fiap_app_produto_test.Controllers
{
    public class ProdutoControllerTest
    {
        private readonly Mock<IProdutoService> _produtoServiceMock;
        private readonly ProdutoController _controller;

        public ProdutoControllerTest()
        {
            _produtoServiceMock = new Mock<IProdutoService>();
            _controller = new ProdutoController(_produtoServiceMock.Object);
        }

        [Fact]
        public async Task ConsultarTodos_DeveRetornar200OK_QuandoProdutosExistirem()
        {
            // Arrange
            var produtos = new List<Produto>
            {
                new Produto { Id = 1, Nome = "Produto 1", Preco = 10.00M, Descricao = "Decrição do Produto 1", UrlImagem = "", Categoria = new Categoria() { Id = 1, Nome = "Lanche"} },
                new Produto { Id = 2, Nome = "Produto 2", Preco = 12.00M, Descricao = "Decrição do Produto 2", UrlImagem = "", Categoria = new Categoria() { Id = 1, Nome = "Lanche"} }
            };
            _produtoServiceMock.Setup(service => service.GetAll()).ReturnsAsync(produtos);

            // Act
            var resultado = await _controller.GetAll();

            // Assert
            var okResult = resultado.Should().BeOfType<OkObjectResult>().Subject;
            var returnedProdutos = okResult.Value.Should().BeAssignableTo<IEnumerable<Produto>>().Subject;
            returnedProdutos.Should().HaveCount(produtos.Count);
        }

        [Fact]
        public async Task ConsultarPorIdCategoria_DeveRetornar200OK_QuandoProdutosDaCategoriaExistirem()
        {
            // Arrange
            int idCategoria = 1;
            var produtos = new List<Produto> { new Produto { Id = 1, Nome = "Produto 1", Preco = 10.00M, Descricao = "Decrição do Produto 1", UrlImagem = "", Categoria = new Categoria() { Id = 1, Nome = "Lanche" } } };
            _produtoServiceMock.Setup(service => service.GetByIdCategoria(idCategoria)).ReturnsAsync(produtos);

            // Act
            var resultado = await _controller.GetByIdCategoria(idCategoria);

            // Assert
            var okResult = resultado.Should().BeOfType<OkObjectResult>().Subject;
            okResult.Value.Should().BeEquivalentTo(produtos);
        }

        [Fact]
        public async Task Criar_DeveRetornar201Created_QuandoProdutoForCriado()
        {
            // Arrange
            var novoProduto = new Produto { Id = 1, Nome = "Produto 1", Preco = 10.00M, Descricao = "Decrição do Produto 1", UrlImagem = "", Categoria = new Categoria() { Id = 1, Nome = "Lanche" } };
            _produtoServiceMock.Setup(service => service.Create(novoProduto)).ReturnsAsync(1); // Suponha que o novo produto receba o ID 1

            // Act
            var resultado = await _controller.Post(novoProduto);

            // Assert
            var createdAtActionResult = resultado.Should().BeOfType<CreatedAtRouteResult>().Subject;
            createdAtActionResult.RouteName.Should().Be("ProdutoPorId");
            createdAtActionResult.RouteValues["id"].Should().Be(1);
        }

        [Fact]
        public async Task Deletar_DeveRetornar204NoContent_QuandoProdutoForDeletado()
        {
            // Arrange
            _produtoServiceMock.Setup(service => service.Delete(It.IsAny<int>())).ReturnsAsync(true);

            // Act
            var resultado = await _controller.Delete(1);

            // Assert
            resultado.Should().BeOfType<NoContentResult>();
        }

        [Fact]
        public async Task Atualizar_DeveRetornar200OK_QuandoProdutoForAtualizado()
        {
            // Arrange
            var produtoAtualizado = new Produto { Id = 1, Nome = "Produto 1", Preco = 10.00M, Descricao = "Decrição do Produto 1", UrlImagem = "", Categoria = new Categoria() { Id = 1, Nome = "Lanche" } };
            _produtoServiceMock.Setup(service => service.Update(produtoAtualizado, produtoAtualizado.Id)).ReturnsAsync(true);

            // Act
            var resultado = await _controller.Put(produtoAtualizado.Id, produtoAtualizado);

            // Assert
            resultado.Should().BeOfType<OkResult>();
        }

        [Fact]
        public async Task ConsultarPorId_DeveRetornar200OK_QuandoProdutoExistir()
        {
            // Arrange
            var produto = new Produto { Id = 1, Nome = "Produto 1", Preco = 10.00M, Descricao = "Decrição do Produto 1", UrlImagem = "", Categoria = new Categoria() { Id = 1, Nome = "Lanche" } };
            _produtoServiceMock.Setup(service => service.GetById(1)).ReturnsAsync(produto);

            // Act
            var resultado = await _controller.GetById(1);

            // Assert
            var okResult = resultado.Should().BeOfType<OkObjectResult>().Subject;
            okResult.Value.Should().BeEquivalentTo(produto);
        }
    }
}
