using PT_SalasDario.Data;

namespace PT_SalasDario.Repository
{
    public interface ICharacterRepository
    {
        Task CreateCharacters(IEnumerable<Character> characters);
        Task<IEnumerable<Character>> GetAllCharactersPaginated(int? pageNumber, int? pageSize);
        Task<IEnumerable<Character>> GetAllCharacters();
    }
}
