using System.ComponentModel.DataAnnotations;

namespace PT_SalasDario.Services.Requests
{
    public class GetCharactersRequest
    {
       
        public int? PageNumber { get; set; }

        [Required]
        public int? PageSize { get; set; }
    }
}
