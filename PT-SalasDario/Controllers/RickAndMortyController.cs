using Microsoft.AspNetCore.Mvc;
using PT_SalasDario.Services;

namespace PT_SalasDario.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RickAndMortyController : ControllerBase
    {
        private readonly ICharacterService _characterService;

        public RickAndMortyController(ICharacterService characterService)
        {
            _characterService = characterService;
        }

        [HttpPost(Name = "ImportCharactersToDB")]
        public async Task<IActionResult> ImportCharactersToDB()
        {
            var characters = await _characterService.ImportCharactersAsync();
            return Ok(characters);
        }
    }
}
