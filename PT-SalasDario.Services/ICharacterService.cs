using PT_SalasDario.Data;
using PT_SalasDario.Services.Requests;
using PT_SalasDario.Services.Response;

namespace PT_SalasDario.Services
{
    public interface ICharacterService
    {
        Task<int> ImportCharacters();

        Task<IEnumerable<CharaterResponseDTO>> GetAllCharactersAsync(int? pageNumber, int? pageSize);
    }
}
