﻿using PT_SalasDario.Data;
using PT_SalasDario.Repository;
using System.Net.Http.Json;

namespace PT_SalasDario.Services
{
    public class CharacterService: ICharacterService
    {
        private readonly HttpClient _httpClient; //It can be implemented in the Program.cs
        private const string BASEURL = "https://rickandmortyapi.com/api";
        private readonly ICharacterRepository _characterRepository;

        public CharacterService(HttpClient httpClient, ICharacterRepository characterRepository)
        {
            _httpClient = httpClient;
            _characterRepository = characterRepository;
        }

        public async Task<List<Character>> ImportCharactersAsync()
        {
            var characters = new List<Character>();
            string url = $"{BASEURL}/character";

            while (!string.IsNullOrEmpty(url))
            {
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

            return characters;
        }

        private class ApiResponse<T>
        {
            public Info Info { get; set; }
            public List<T> Results { get; set; }
        }

        private class Info
        {
            public string Next { get; set; }
        }
    }
}
