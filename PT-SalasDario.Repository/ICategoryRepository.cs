using PT_SalasDario.Data;

namespace PT_SalasDario.Repository
{
    public interface ICategoryRepository
    {
        Task CreateCategory(Category category);
    }
}
