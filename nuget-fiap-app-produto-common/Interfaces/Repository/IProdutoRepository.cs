using nuget_fiap_app_produto_common.Models;

namespace nuget_fiap_app_produto_common.Interfaces.Repository
{
    public interface IProdutoRepository
    {
        public Task<IEnumerable<Produto>> GetAll();
        public Task<IEnumerable<Produto>> GetByIdCategoria(int idCategoria);
        public Task<int> Create(Produto produto);
        public Task<bool> Delete(int id);
        public Task<Produto> GetById(int id);
        public Task<bool> Update(Produto produto);
    }
}
