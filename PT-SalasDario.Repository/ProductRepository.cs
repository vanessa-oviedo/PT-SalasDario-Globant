using Microsoft.EntityFrameworkCore;
using PT_SalasDario.Data;

namespace PT_SalasDario.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly MyDbContext _dbContext;

        public ProductRepository(MyDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task CreateProduct(Product product)
        {
            await _dbContext.AddAsync(product);
            await _dbContext.SaveChangesAsync();
        }

        public IQueryable<Product> GetProducts()
        {
            return _dbContext.Products;
        }

        public async Task<Product> GetProduct(int id)
        {
            var product = await _dbContext.Products.Where(c => c.Id == id)
                            .Include(i=> i.Category)
                            .FirstOrDefaultAsync();
            return product;
        }
    }
}
