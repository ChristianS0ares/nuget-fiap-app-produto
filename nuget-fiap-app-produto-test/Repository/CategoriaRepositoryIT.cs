using Microsoft.Extensions.Options;
using nuget_fiap_app_produto_common.Models;
using nuget_fiap_app_produto_repository;
using nuget_fiap_app_produto_repository.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace nuget_fiap_app_produto_test.Repository
{
    public class CategoriaRepositoryIT
    {
        public CategoriaRepositoryIT()
        {
            
        }

        [Fact]
        public async Task devePermitirRegistrarCategoria()
        {
            var session = new RepositoryDB();
            var repository = new CategoriaRepository(session);

            var novaCategoria = new Categoria { Nome = "Teste" };
            var idNovaCategoria = await repository.Create(novaCategoria);

            var categoriaInserida = await repository.GetById(idNovaCategoria);
            Assert.NotNull(categoriaInserida);
            Assert.Equal("Teste", categoriaInserida.Nome);
        }

        [Fact]
        public async Task deveRecuperarTodasAsCategorias()
        {
            var session = new RepositoryDB();
            var repository = new CategoriaRepository(session);

            var categorias = await repository.GetAll();
            Assert.NotNull(categorias);
            Assert.True(categorias.Any()); // Presume que a tabela não está vazia
        }

        [Fact]
        public async Task deveRecuperarCategoriaPorId()
        {
            var session = new RepositoryDB();
            var repository = new CategoriaRepository(session);

            // Presume-se que a categoria com ID 1 existe
            var categoria = await repository.GetById(1);
            Assert.NotNull(categoria);
            Assert.Equal(1, categoria.Id);
        }

        [Fact]
        public async Task devePermitirAtualizarCategoria()
        {
            var session = new RepositoryDB();
            var repository = new CategoriaRepository(session);

            // Presume-se que a categoria com ID 1 existe
            var categoriaParaAtualizar = new Categoria { Id = 1, Nome = "Atualizado" };
            var sucesso = await repository.Update(categoriaParaAtualizar);

            var categoriaAtualizada = await repository.GetById(1);
            Assert.True(sucesso);
            Assert.NotNull(categoriaAtualizada);
            Assert.Equal("Atualizado", categoriaAtualizada.Nome);
        }

        [Fact]
        public async Task devePermitirExcluirCategoria()
        {
            var session = new RepositoryDB();
            var repository = new CategoriaRepository(session);

            // Inserir uma nova categoria para deletar
            var novaCategoria = new Categoria { Nome = "Para Deletar" };
            var idCategoria = await repository.Create(novaCategoria);

            var sucesso = await repository.Delete(idCategoria);
            var categoriaDeletada = await repository.GetById(idCategoria);

            Assert.True(sucesso);
            Assert.Null(categoriaDeletada);
        }


    }
}
