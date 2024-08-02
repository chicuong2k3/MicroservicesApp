using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Catalog.Api.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) 
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Variant> Variants { get; set; }
        public DbSet<ProductAttribute> ProductAttributes { get; set; }
        public DbSet<VariantOption> VariantOptions { get; set; }
        public DbSet<Category> Categories { get; set; }
    }
}
