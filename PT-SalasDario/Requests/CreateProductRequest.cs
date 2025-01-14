using System.ComponentModel.DataAnnotations;

namespace PT_SalasDario.API.Requests
{
    public class CreateProductRequest
    {
        [Required]
        public string? Name { get; set; }

        [Required]
        public string? Description { get; set; }

        [Required]
        public double? Price { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Stock must be a positive number.")]
        public int? Stock { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "CategoryId must be a positive number.")]
        public int? CategoryId { get; set; }

        [Required]
        public string? CategoryName { get; set; }
    }
}
