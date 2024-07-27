using Microsoft.EntityFrameworkCore;
using ProductCatalog.Application.Common.Interfaces;
using ProductCatalog.Data.EntityTypeConfiguration;
using ProductCatalog.Domain;
using ProductCatalog.Persistence.EntityTypeConfiguration;

namespace ProductCatalog.Data
{
    public class ProductDbContext : DbContext, IProductDbContext
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<SubCategory> SubCategories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Cost> Costs { get; set; }
        public DbSet<Manufacturer> Manufacturer { get; set; }
        public DbSet<Currency> Currency { get; set; }

        public ProductDbContext(DbContextOptions<ProductDbContext> options) 
            : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CategoryConfiguration());
            modelBuilder.ApplyConfiguration(new SubCategoryConfiguration());
            modelBuilder.ApplyConfiguration(new ProductConfiguration());
            modelBuilder.ApplyConfiguration(new CostConfiguration());
            modelBuilder.ApplyConfiguration(new ManufacturerConfiguration());
            modelBuilder.ApplyConfiguration(new CurrencyConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}
