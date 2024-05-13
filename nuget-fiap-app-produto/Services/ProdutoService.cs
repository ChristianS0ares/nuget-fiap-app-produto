using nuget_fiap_app_produto_common.Interfaces.Repository;
using nuget_fiap_app_produto_common.Interfaces.Services;
using nuget_fiap_app_produto_common.Models;
using nuget_fiap_app_produto_repository.Interface;

namespace nuget_fiap_app_produto.Services
{
    public class ProdutoService : IProdutoService
    {
        private readonly IProdutoRepository _produtoRepository;
        private readonly IUnitOfWork _unitOfWork;
        
        public ProdutoService(IProdutoRepository produtoRepository, IUnitOfWork unitOfWork)
        {
            _produtoRepository = produtoRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<int> Create(Produto produto)
        {
            try
            {
                int produtoId = await _produtoRepository.Create(produto);
                return produtoId;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> Delete(int id)
        {
            try
            {
                var result = await _produtoRepository.Delete(id);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<Produto>> GetAll()
        {
            return await _produtoRepository.GetAll();
        }

        public async Task<Produto> GetById(int id)
        {
            try
            {
                return await _produtoRepository.GetById(id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<Produto>> GetByIdCategoria(int idCategoria)
        {
            return await _produtoRepository.GetByIdCategoria(idCategoria);
        }

        public async  Task<bool> Update(Produto produto, int id)
        {

            try
            {
                var existingProduct = await _produtoRepository.GetById(id);

                if (existingProduct == null)
                {
                    return false; // Produto não encontrado, portanto, não foi atualizado.
                }

                // Realize as validações necessárias no objeto 'produto' e manipule exceções, se necessário.

                // Atualize as propriedades do produto existente com base nos dados de 'produto'.
                existingProduct.Nome = produto.Nome;
                existingProduct.Preco = produto.Preco;
                existingProduct.Descricao = produto.Descricao;
                existingProduct.UrlImagem = produto.UrlImagem;
                existingProduct.Categoria = produto.Categoria;

                bool updated = await _produtoRepository.Update(existingProduct);
                return updated;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}

