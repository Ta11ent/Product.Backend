using ShoppingCart.Domain;
using Microsoft.EntityFrameworkCore;

namespace ShoppingCart.Application.Common.Abstractions
{
    public interface IOrderDbContext : IDisposable
    {
        public DbSet<Order> Orders { get; set; }
        public DbSet<ProductRange> ProductRanges { get; set; }
        public Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
