using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PT_SalasDario.Services.Requests
{
    public class GetCharactersRequest
    {
       
        public int? PageNumber { get; set; }

        [Required]
        public int? PageSize { get; set; }
    }
}
