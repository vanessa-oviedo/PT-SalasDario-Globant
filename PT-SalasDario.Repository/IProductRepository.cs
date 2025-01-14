using PT_SalasDario.Data;

namespace PT_SalasDario.Repository
{
    public interface IProductRepository
    {
        Task CreateProduct(Product product);

        IQueryable<Product> GetProducts();

        Task<Product> GetProduct(int id);
    }
}
