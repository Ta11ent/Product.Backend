using Microsoft.EntityFrameworkCore;
using ProductCatalog.Domain;

namespace ProductCatalog.Application.Common.Interfaces
{
    public interface IProductDbContext : IDisposable
    {
        DbSet<Category> Categories { get; set; }
        DbSet<SubCategory> SubCategories { get; set; }
        DbSet<Product> Products { get; set; }
        DbSet<Cost> Costs { get; set; }
        DbSet<Manufacturer> Manufacturer { get; set; }
        DbSet<Currency> Currency { get; set; }
        DbSet<ProductSale> ProductSale { get; set; }
        DbSet<ROE> ROE { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
