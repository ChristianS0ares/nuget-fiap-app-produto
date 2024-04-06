using nuget_fiap_app_produto_common.Interfaces.Repository;
using nuget_fiap_app_produto_common.Interfaces.Services;
using nuget_fiap_app_produto_common.Models;
using nuget_fiap_app_produto_repository.Interface;

namespace nuget_fiap_app_produto.Services
{
    public class CategoriaService : ICategoriaService
    {

        private readonly ICategoriaRepository _categoriaRespository;
        private readonly IProdutoService _produtoService;
        private readonly IUnitOfWork _unitOfWork;

        public CategoriaService(ICategoriaRepository categoriaRespository, 
                                IProdutoService produtoService, 
                                IUnitOfWork unitOfWork)
        {
            _categoriaRespository = categoriaRespository;
            _produtoService = produtoService;
            _unitOfWork = unitOfWork;
        }

        public async Task<int> Create(Categoria categoria)
        {
            try
            {
                _unitOfWork.BeginTransaction();
                int categoriaId = await _categoriaRespository.Create(categoria);

                if (categoriaId > 0)
                {
                    _unitOfWork.Commit();
                    return categoriaId;
                }
                else
                {
                    _unitOfWork.Rollback();
                    return 0;
                }
            }
            catch (Exception ex)
            {
                _unitOfWork.Rollback();
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> Delete(int id)
        {
            if (!await CanDeleteCategoria(id))
            {
                throw new Exception("Não é possível excluir a categoria devido a restrições de chave estrangeira.");
            }

            try
            {
                _unitOfWork.BeginTransaction();
                var result = await _categoriaRespository.Delete(id);

                if (result)
                {
                    _unitOfWork.Commit();
                    return true;
                }
                else
                {
                    _unitOfWork.Rollback();
                    return false;
                }
            }
            catch (Exception ex)
            {
                _unitOfWork.Rollback();
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<Categoria>> GetAll()
        {
            try
            {
                return await _categoriaRespository.GetAll();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Categoria> GetById(int id)
        {
            try
            {
                return await _categoriaRespository.GetById(id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> Update(Categoria categoria, int id)
        {
            try
            {
                var existingCategoria = await _categoriaRespository.GetById(id);

                if (existingCategoria == null)
                {
                    return false; // Categoria não encontrada, portanto, não foi atualizada.
                }

                // Realize as validações necessárias no objeto 'categoria' e manipule exceções, se necessário.

                // Atualize as propriedades da categoria existente com base nos dados de 'categoria'.
                existingCategoria.Nome = categoria.Nome;

                _unitOfWork.BeginTransaction();
                bool updated = await _categoriaRespository.Update(existingCategoria);

                if (updated)
                {
                    _unitOfWork.Commit();
                    return true;
                }
                else
                {
                    _unitOfWork.Rollback();
                    return false;
                }
            }
            catch (Exception ex)
            {
                _unitOfWork.Rollback();
                throw new Exception(ex.Message);
            }
        }

        private async Task<bool> CanDeleteCategoria(int categoriaId)
        {
            // Verifique se há produtos associados a esta categoria
            var produtos = await _produtoService.GetByIdCategoria(categoriaId);

            if (produtos != null && produtos.Any())
            {
                // Se houver produtos associados, não permita a exclusão da categoria
                return false;
            }

            // Se não houver produtos associados, a categoria pode ser excluída
            return true;
        }
       
    }
}
