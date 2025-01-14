using AutoMapper;
using Azure.Core;
using PT_SalasDario.Data;
using PT_SalasDario.Repository;
using PT_SalasDario.Services.Models.ExternalModels;
using PT_SalasDario.Services.Requests;
using PT_SalasDario.Services.Response;
using System.Net.Http.Json;

namespace PT_SalasDario.Services
{
    public class CharacterService : ICharacterService
    {
        private readonly HttpClient _httpClient; //It can be implemented in the Program.cs
        private const string BASEURL = "https://rickandmortyapi.com/api";
        private readonly ICharacterRepository _characterRepository;
        private readonly IMapper _mapper;

        public CharacterService(HttpClient httpClient, ICharacterRepository characterRepository, IMapper mapper)
        {
            _httpClient = httpClient;
            _characterRepository = characterRepository;
            _mapper = mapper;
        }

        //We can use this inside a Job background like hingfire
        public async Task<int> ImportCharactersAsync()
        {
            var characters = new List<CharaterResponseDTO>();
            string url = $"{BASEURL}/character";

            while (!string.IsNullOrEmpty(url))
            {
                //TODO: Should be a model
                var response = await _httpClient.GetFromJsonAsync<ApiResponse<CharaterResponseDTO>>(url);
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

            await _characterRepository.CreateCharacters(_mapper.Map<IEnumerable<Character>>(characters));

            return characters.Count;
        }

        public async Task<IEnumerable<CharaterResponseDTO>> GetAllCharactersAsync(int? pageNumber, int? pageSize)
        {
            IEnumerable<Character> characters;

            if (pageNumber == null || pageSize == null)
                characters = await _characterRepository.GetAllCharacters();
            else
                characters = await _characterRepository.GetAllCharactersPaginated(pageNumber, pageSize);

            return _mapper.Map<IEnumerable<CharaterResponseDTO>>(characters);
        }
    }
}
