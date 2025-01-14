using System.ComponentModel.DataAnnotations;

namespace PT_SalasDario.API.Requests
{
    public class GetCharatersRequest
    {
        public class GetCharactersRequest
        {
            [Range(1, int.MaxValue, ErrorMessage = "PageNumber must be greater than or equal to 1.")]
            public int? PageNumber { get; set; }

            [Range(1, int.MaxValue, ErrorMessage = "PageSize must be greater than or equal to 1.")]
            public int? PageSize { get; set; }
        }
    }
}
