using ChoppingCart.Domain;
using Microsoft.EntityFrameworkCore;

namespace ShoppingCart.Application.Abstractions
{
    public interface IOrderDbContext : IDisposable
    {
        public DbSet<Order> Orders { get; set; }
        public DbSet<ProductRange> ProductRanges { get; set; }
        public Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
