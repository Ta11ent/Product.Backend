using Microsoft.EntityFrameworkCore;
using ProductCatalog.Domain;

namespace ProductCatalog.Application.Common.Interfaces
{
    public interface IProductDbContext : IDisposable
    {
        DbSet<Category> Categories { get; set; }
        DbSet<Product> Products { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
