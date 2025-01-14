using PT_SalasDario.Data;
using PT_SalasDario.Repository;
using PT_SalasDario.Services.Models.ExternalModels;
using PT_SalasDario.Services.Requests;
using System.Net.Http.Json;

namespace PT_SalasDario.Services
{
    public class CharacterService : ICharacterService
    {
        private readonly HttpClient _httpClient; //It can be implemented in the Program.cs
        private const string BASEURL = "https://rickandmortyapi.com/api";
        private readonly ICharacterRepository _characterRepository;

        public CharacterService(HttpClient httpClient, ICharacterRepository characterRepository)
        {
            _httpClient = httpClient;
            _characterRepository = characterRepository;
        }

        public async Task<int> ImportCharactersAsync()
        {
            var characters = new List<Character>();
            string url = $"{BASEURL}/character";

            while (!string.IsNullOrEmpty(url))
            {
                //TODO: Should be a model
                var response = await _httpClient.GetFromJsonAsync<ApiResponse<Character>>(url);
                if (response?.Results != null)
                {
                    characters.AddRange(response.Results);
                    url = response.Info?.Next;
                }
                else
                {
                    url = null;
                }
            }

            var charactersEnumerable = characters;
            await _characterRepository.CreateCharacters(charactersEnumerable);

            return characters.Count;
        }

        public async Task<IEnumerable<Character>> GetAllCharactersAsync(int? pageNumber, int? pageSize)
        {
            IEnumerable<Character> characters;

            if (pageNumber == null || pageSize == null)
                characters = await _characterRepository.GetAllCharacters();
            else
                characters = await _characterRepository.GetAllCharactersPaginated(pageNumber, pageSize);

            return characters;
        }
    }
}
