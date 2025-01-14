using PT_SalasDario.Data;
using PT_SalasDario.Services.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PT_SalasDario.Services
{
    public interface ICharacterService
    {
        Task<IEnumerable<Character>> ImportCharactersAsync();
        Task<IEnumerable<Character>> GetAllCharactersAsync(GetCharactersRequest charactersRequest);
    }
}
