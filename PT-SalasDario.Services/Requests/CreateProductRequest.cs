using System.ComponentModel.DataAnnotations;

namespace PT_SalasDario.Services.Requests
{
    public class CreateProductRequest
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public double Price { get; set; }

        public int Stock { get; set; }

        public int CategoryId { get; set; }

        public string CategoryName { get; set; }
    }
}
