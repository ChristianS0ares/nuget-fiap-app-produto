using nuget_fiap_app_produto_common.Models;

namespace nuget_fiap_app_produto_common.Interfaces.Services
{
    public interface ICategoriaService
    {
        public Task<IEnumerable<Categoria>> GetAll();
        public Task<Categoria> GetById(int id);
        public Task<int> Create(Categoria categoria);
        public Task<bool> Update(Categoria categoria, int id);
        public Task<bool> Delete(int id);
    }
}
