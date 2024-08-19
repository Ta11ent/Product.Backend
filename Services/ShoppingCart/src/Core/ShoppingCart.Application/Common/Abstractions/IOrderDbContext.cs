using ShoppingCart.Domain;
using Microsoft.EntityFrameworkCore;

namespace ShoppingCart.Application.Common.Abstractions
{
    public interface IOrderDbContext : IDisposable
    {
        DbSet<Order> Orders { get; set; }
        DbSet<OrderItem> OrderItems { get; set; }
        DbSet<Status> Statuses { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
