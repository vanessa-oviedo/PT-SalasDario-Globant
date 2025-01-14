using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PT_SalasDario.Data
{
    [Table("product")]
    public class Product
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public double Price { get; set; }

        public int Stock { get; set; }

        public int CategoryId { get; set; }


        [ForeignKey(nameof(CategoryId))]
        public Category Category { get; set; }
    }
}
