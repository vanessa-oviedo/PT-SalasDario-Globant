using Microsoft.EntityFrameworkCore;

namespace PT_SalasDario.Data
{
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext> options)
            : base(options)
        {
        }

        public MyDbContext()
            : base()
        {
        }

        public DbSet<Product> Products { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Character> Characters { get; set; }
    }
}
