using PT_SalasDario.Data;

namespace PT_SalasDario.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly MyDbContext _dbContext;

        public CategoryRepository(MyDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task CreateCategory(Category category)
        {
            var existingCategory = await _dbContext.Categories.FindAsync(category.Id);

            if (existingCategory == null)
            {
                _dbContext.Categories.Add(category);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
