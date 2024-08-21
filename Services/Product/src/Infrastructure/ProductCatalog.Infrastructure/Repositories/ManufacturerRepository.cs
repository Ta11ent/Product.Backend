using Microsoft.EntityFrameworkCore;
using ProductCatalog.Application.Common.Abstractions;
using ProductCatalog.Application.Common.Interfaces;
using ProductCatalog.Application.Common.Pagination;
using ProductCatalog.Domain;

namespace ProductCatalog.Infrastructure.Repositories
{
    public class ManufacturerRepository : BaseRepository, IManufacturerRepository
    {
        public ManufacturerRepository(IProductDbContext dbContext) : base(dbContext) { }

        public async Task CreateManufacturerAsync(Manufacturer manufacturer, CancellationToken cancellationToken)
            => await _dbContext.Manufacturer.AddAsync(manufacturer, cancellationToken);
        public void DeleteManufacturer(Manufacturer manufacturer)
            => _dbContext.Manufacturer.Remove(manufacturer);
        public async Task<IEnumerable<Manufacturer>> GetAllManufacturersAsync(IPagination pagination, CancellationToken cancellationToken)
        {
            var data = await
                _dbContext.Manufacturer
                   .Skip((pagination.Page - 1) * pagination.PageSize)
                   .Take(pagination.PageSize)
                   .ToListAsync(cancellationToken);
            return data;
        }
        public async Task<Manufacturer> GetManufacturerByIdAsync(Guid manufacturerId, CancellationToken cancellationToken)
        {
            var data =
               await _dbContext.Manufacturer
                .Where(c => c.ManufacturerId == manufacturerId)
                .FirstOrDefaultAsync(cancellationToken);
            return data!;
        }
    }
}
