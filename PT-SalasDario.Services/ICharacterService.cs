using PT_SalasDario.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PT_SalasDario.Services
{
    public interface ICharacterService
    {
        Task<List<Character>> ImportCharactersAsync();
    }
}
