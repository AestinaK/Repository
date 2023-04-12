using Microsoft.EntityFrameworkCore;

namespace Repo.Core
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext()
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // You don't actually ever need to call this
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql(
                "Server=localhost;Port=5432;Database=School;User Id=postgres;Password=Postme123");
            }
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) :
            base(options) { 
        
        }
        public virtual DbSet<Product> Products { get; set; }
    }
}
