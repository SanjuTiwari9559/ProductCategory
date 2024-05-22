using Microsoft.EntityFrameworkCore;

namespace ProductCategory.Model
{
    public class AplicationDbContext:DbContext
    {
        public AplicationDbContext(DbContextOptions<AplicationDbContext> options):base(options) 
        {
            
        }
       public DbSet<Category> Categories1 { get; set; }
        public DbSet<Product> Products1 { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Product>().HasOne(p => p.Category).WithMany(p => p.Products).HasForeignKey(p => p.CategoryId);
        }
    }
}
