using ShoppingCart.Domain;
using Microsoft.EntityFrameworkCore;

namespace ShoppingCart.Application.Common.Abstractions
{
    public interface IOrderDbContext : IDisposable
    {
        DbSet<Order> Orders { get; set; }
        DbSet<ProductRange> ProductRanges { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
