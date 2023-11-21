using ShoppingCart.Domain;
using Microsoft.EntityFrameworkCore;
using ShoppingCart.Application.Common.Abstractions;
using ShoppingCart.Persistence.EntityTypeConfiguration;

namespace ShoppingCart.Persistence
{
    public class OrderDbContext : DbContext, IOrderDbContext
    {
        public DbSet<Order> Orders { get; set; }
        public DbSet<ProductRange> ProductRanges { get; set; }
        public OrderDbContext(DbContextOptions<OrderDbContext> options) 
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new OrderConfiguration());
            modelBuilder.ApplyConfiguration(new ProductRangeConfiguration());
        }

    }
}
