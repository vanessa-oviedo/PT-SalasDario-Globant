using Microsoft.AspNetCore.Mvc;
using PT_SalasDario.API.Infra;
using PT_SalasDario.API.Requests;
using PT_SalasDario.Services;
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
                // Consider not waiting for the importation to complete before returning a status code.
                // A fire-and-forget approach might be more suitable if real-time feedback is not required.
                var characters = await _characterService.ImportCharacters();
                return Ok($"# Characters Added: {characters}");
            }
            catch (Exception ex)
            {
                return BadRequest(new ErrorResult { StatusCode = 500, Errors = [ex.Message.ToString()] });
            }
        }

        [HttpGet(Name = "GetAllCharacters")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<CharaterResponseDTO>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResult))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorResult))]
        public async Task<IActionResult> Get([FromQuery] GetCharactersRequest request)
        {
            try
            {
                var characters = await _characterService.GetAllCharactersAsync(request.PageNumber, request.PageSize);
                return Ok(characters);
            }
            catch (Exception ex)
            {
                return BadRequest(new ErrorResult { StatusCode = 500, Errors = [ex.Message.ToString()] });
            }
        }
    }
}
