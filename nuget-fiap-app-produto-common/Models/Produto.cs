
namespace nuget_fiap_app_produto_common.Models
{
    public class Produto
    {
        public required int Id { get; set; }
        public required string Nome { get; set; }
        public required decimal Preco { get; set; }
        public required string Descricao { get; set; }
        public required string UrlImagem { get; set; }
        public required Categoria Categoria { get; set; }
    }
}
