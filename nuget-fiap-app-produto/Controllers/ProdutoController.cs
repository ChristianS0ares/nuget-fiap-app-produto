﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using nuget_fiap_app_produto_common.Interfaces.Services;
using nuget_fiap_app_produto_common.Models;
using Swashbuckle.AspNetCore.Annotations;

namespace nuget_fiap_app_produto.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProdutoController : Controller
    {
        private readonly IProdutoService _produtoService;

        public ProdutoController(IProdutoService produtoService)
        {
            _produtoService = produtoService;
        }

        /// <summary>
        /// Obtém uma lista de todos os produtos.
        /// </summary>
        /// <returns>Uma lista de produtos.</returns>
        [HttpGet(Name = "Produtos")]
        [SwaggerOperation(Summary = "Listagem de todos os produtos", Description = "Recupera uma lista de todos os produtos.")]
        [SwaggerResponse(StatusCodes.Status200OK, "A lista de produtos foi recuperada com sucesso.", typeof(IEnumerable<Produto>))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Nenhum produto encontrado.")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Erro interno do servidor.")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var produtos = await _produtoService.GetAll();
                return Ok(produtos); // Retorna 200 OK com os produtos recuperados.
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message); // Retorna 500 Internal Server Error em caso de erro interno do servidor.
            }
        }


        /// <summary>
        /// Obtém uma lista de produtos com base no identificador da categoria.
        /// </summary>
        /// <param name="idCategoria">O identificador da categoria dos produtos desejada.</param>
        /// <returns>Uma lista de produtos filtrados pela categoria especificada.</returns>
        [HttpGet("Categoria/{idCategoria}", Name = "ProdutosPorCategoria")]
        [SwaggerOperation(Summary = "Listagem de produtos por categoria", Description = "Recupera uma lista de produtos filtrados pela categoria especificada.")]
        [SwaggerResponse(StatusCodes.Status200OK, "A lista de produtos foi recuperada com sucesso.", typeof(IEnumerable<Produto>))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Nenhum produto encontrado.")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Erro interno do servidor.")]
        public async Task<IActionResult> GetByIdCategoria(int idCategoria)
        {
            try
            {
                var produtos = await _produtoService.GetByIdCategoria(idCategoria);
                return Ok(produtos); // Retorna 200 OK com os produtos recuperados.
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message); // Retorna 500 Internal Server Error em caso de erro interno do servidor.
            }
        }


        /// <summary>
        /// Cria um novo produto.
        /// </summary>
        /// <param name="produto">Os dados do novo produto a ser criado.</param>
        /// <returns>
        /// 201 Created juntamente com o URL do novo recurso se a criação for bem-sucedida.
        /// 500 Internal Server Error em caso de erro inesperado no servidor.
        /// </returns>
        [HttpPost(Name = "Produtos")]
        [SwaggerOperation(Summary = "Criação de um novo produto", Description = "Cria um novo produto com base nos dados fornecidos.")]
        [SwaggerResponse(StatusCodes.Status201Created, "O produto foi criado com sucesso.")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Erro interno do servidor.")]
        public async Task<IActionResult> Post(Produto produto)
        {
            try
            {
                int produtoId = await _produtoService.Create(produto);
                return CreatedAtRoute("ProdutoPorId", new { id = produtoId }, null);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// Exclui um produto com base no ID especificado.
        /// </summary>
        /// <param name="id">O ID do produto a ser excluído.</param>
        /// <returns>
        /// 204 No Content se o produto for excluído com sucesso.
        /// 404 Not Found se o produto não for encontrado.
        /// 500 Internal Server Error em caso de erro interno do servidor.
        /// </returns>
        [HttpDelete("{id}", Name = "ProdutoPorId")]
        [SwaggerOperation(Summary = "Exclusão de produto por ID", Description = "Exclui um produto com base no ID especificado.")]
        [SwaggerResponse(StatusCodes.Status204NoContent, "O produto foi excluído com sucesso.")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Produto não encontrado.")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Erro interno do servidor.")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                bool result = await _produtoService.Delete(id);

                if (!result)
                {
                    return NotFound(); // Retorna 404 Not Found se o produto não for encontrado.
                }
                return NoContent(); // Retorna 204 No Content se o produto for excluído com sucesso.
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message); // Retorna 500 Internal Server Error em caso de erro interno do servidor.
            }
        }

        /// <summary>
        /// Obtém um produto com base no ID especificado.
        /// </summary>
        /// <param name="id">O ID do produto desejado.</param>
        /// <returns>
        /// 200 OK com o produto recuperado.
        /// 404 Not Found se o produto não for encontrado.
        /// 500 Internal Server Error em caso de erro interno do servidor.
        /// </returns>
        [HttpGet("{id}", Name = "ProdutoPorId")]
        [SwaggerOperation(Summary = "Obtenção de produto por ID", Description = "Obtém um produto com base no ID especificado.")]
        [SwaggerResponse(StatusCodes.Status200OK, "O produto foi recuperado com sucesso.", typeof(Produto))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Produto não encontrado.")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Erro interno do servidor.")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var produto = await _produtoService.GetById(id);

                if (produto == null)
                {
                    return NotFound(); // Retorna 404 Not Found se o produto não for encontrado.
                }
                return Ok(produto); // Retorna 200 OK com o produto recuperado.
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message); // Retorna 500 Internal Server Error em caso de erro interno do servidor.
            }
        }

        /// <summary>
        /// Atualiza um produto com base no ID especificado.
        /// </summary>
        /// <param name="id">O ID do produto a ser atualizado.</param>
        /// <param name="produto">Os dados do produto a serem atualizados.</param>
        /// <returns>
        /// Retorna um código de status HTTP que indica o resultado da operação:
        /// - 200 OK se a atualização for bem-sucedida.
        /// - 404 Not Found se o produto não for encontrado com o ID especificado.
        /// - 500 Internal Server Error em caso de erro interno do servidor.
        /// </returns>
        [HttpPut("{id}", Name = "ProdutoPorId")]
        [SwaggerOperation(Summary = "Atualização de um produto por ID", Description = "Atualiza um produto com base no ID especificado.")]
        [SwaggerResponse(StatusCodes.Status200OK, "O produto foi atualizado com sucesso.")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Produto não encontrado.")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Erro interno do servidor.")]
        public async Task<IActionResult> Put(int id, Produto produto)
        {
            try
            {
                
                bool updated = await _produtoService.Update(produto, id);

                if (!updated)
                {
                    return NotFound(); // Retorna 404 Not Found se o produto não for encontrado.
                }
                return Ok(); // Retorna 200 OK se a atualização for bem-sucedida.
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message); // Retorna 500 Internal Server Error em caso de erro interno do servidor.
            }
        }
    }
}
