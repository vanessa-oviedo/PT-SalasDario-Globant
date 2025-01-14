using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PT_SalasDario.Data
{
    [Table("category")]
    public class Category
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
