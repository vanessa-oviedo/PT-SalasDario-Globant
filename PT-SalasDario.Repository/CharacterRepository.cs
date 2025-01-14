using Microsoft.EntityFrameworkCore;
using PT_SalasDario.Data;

namespace PT_SalasDario.Repository
{
    public class CharacterRepository: ICharacterRepository
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
    }

}
