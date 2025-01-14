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
        public int? Stock { get; set; }

        [Required]
        public int? CategoryId { get; set; }

        [Required]
        public string? CategoryName { get; set; }
    }
}
