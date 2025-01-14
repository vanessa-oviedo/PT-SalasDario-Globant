using Microsoft.AspNetCore.Mvc;
using PT_SalasDario.API.Infra;
using PT_SalasDario.Services;
using PT_SalasDario.Services.Requests;
using PT_SalasDario.Services.Response;

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
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResult))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorResult))]
        public async Task<IActionResult> ImportCharactersToDB()
        {
            try
            {
                var characters = await _characterService.ImportCharactersAsync();
                return Ok($"# Characters Added: {characters}");
            }
            catch (Exception ex)
            {
                return BadRequest(new ErrorResult { StatusCode = 500, Errors = [ex.Message.ToString()] });
            }
        }

        //TODO: use the request from the API
        [HttpGet(Name = "GetAllCharacters")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<CharaterResponseDTO>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResult))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorResult))]
        public async Task<IActionResult> Get([FromQuery] GetCharactersRequest request)
        {
            try
            {
                //TODO: return a DTO instead of the entity
                var characters = await _characterService.GetAllCharactersAsync(request);
                return Ok(characters);
            }
            catch (Exception ex)
            {
                return BadRequest(new ErrorResult { StatusCode = 500, Errors = [ex.Message.ToString()] });
            }
        }
    }
}
