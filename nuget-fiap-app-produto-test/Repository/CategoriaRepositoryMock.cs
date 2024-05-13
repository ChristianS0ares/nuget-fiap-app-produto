using nuget_fiap_app_produto_common.Interfaces.Repository;
using nuget_fiap_app_produto_common.Models;


namespace nuget_fiap_app_produto_test.Repository
{
    public class CategoriaRepositoryMock : ICategoriaRepository
    {
        private List<Categoria> dados;

        public CategoriaRepositoryMock()
        {
            dados = new List<Categoria>() {
                new Categoria() { Id = 1, Nome = "Lanche" },
                new Categoria() { Id = 2, Nome = "Acompanhamento" },
                new Categoria() { Id = 3, Nome = "Bebida" },
                new Categoria() { Id = 4, Nome = "Sobremesa" }
            };
        }

        public async Task<int> Create(Categoria categoria)
        {
            if (categoria == null)
            {
                throw new ArgumentNullException(nameof(categoria));
            }

            var nextId = dados.Any() ? dados.Max(c => c.Id) + 1 : 1;
            categoria.Id = nextId;
            dados.Add(categoria);

            return await Task.FromResult(nextId); // Retorna o Id da nova categoria criada
        }

        public async Task<bool> Delete(int id)
        {
            var categoria = dados.FirstOrDefault(c => c.Id == id);
            if (categoria == null)
            {
                return await Task.FromResult(false);
            }

            dados.Remove(categoria);
            return await Task.FromResult(true);
        }

        public async Task<IEnumerable<Categoria>> GetAll()
        {
            return await Task.FromResult(dados);
        }

        public async Task<Categoria> GetById(int id)
        {
            var categoria = dados.FirstOrDefault(c => c.Id == id);
            return await Task.FromResult(categoria);
        }

        public async Task<bool> Update(Categoria categoria)
        {
            if (categoria == null)
            {
                throw new ArgumentNullException(nameof(categoria));
            }

            var existingCategoria = dados.FirstOrDefault(c => c.Id == categoria.Id);
            if (existingCategoria == null)
            {
                return await Task.FromResult(false);
            }

            // Atualiza os dados da categoria existente
            existingCategoria.Nome = categoria.Nome;

            return await Task.FromResult(true);
        }
    }
}
