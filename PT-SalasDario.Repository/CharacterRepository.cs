using Microsoft.EntityFrameworkCore;
using PT_SalasDario.Data;

namespace PT_SalasDario.Repository
{
    public class CharacterRepository : ICharacterRepository
    {
        private readonly MyDbContext _dbContext;

        public CharacterRepository(MyDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task CreateCharacters(IEnumerable<Character> characters)
        {
            await _dbContext.Characters.ExecuteDeleteAsync();
            await _dbContext.Characters.AddRangeAsync(characters);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Character>> GetAllCharactersPaginated(int? pageNumber, int? pageSize)
        {
            int skip = (pageNumber ?? 0 - 1) * pageSize ?? 0;

            IEnumerable<Character> characters = _dbContext.Characters.Skip(skip).Take(pageSize ?? 0).ToList();

            return characters;
        }

        public async Task<IEnumerable<Character>> GetAllCharacters()
        {

            IEnumerable<Character> characters = _dbContext.Characters.ToList();

            return characters;
        }
    }

}
