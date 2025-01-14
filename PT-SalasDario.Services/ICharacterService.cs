using PT_SalasDario.Data;
using PT_SalasDario.Services.Requests;

namespace PT_SalasDario.Services
{
    public interface ICharacterService
    {
        Task<int> ImportCharactersAsync();

        Task<IEnumerable<Character>> GetAllCharactersAsync(GetCharactersRequest charactersRequest);
    }
}
