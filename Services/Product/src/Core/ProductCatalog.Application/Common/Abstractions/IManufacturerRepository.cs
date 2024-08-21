using ProductCatalog.Application.Common.Pagination;
using ProductCatalog.Domain;

namespace ProductCatalog.Application.Common.Abstractions
{
    public interface IManufacturerRepository
    {
        Task<Manufacturer> GetManufacturerByIdAsync(Guid manufacturerId, CancellationToken cancellationToken);
        Task<IEnumerable<Manufacturer>> GetAllManufacturersAsync(IPagination pagination, CancellationToken cancellationToken);
        Task CreateManufacturerAsync(Manufacturer manufacturer, CancellationToken cancellationToken);
        void DeleteManufacturer(Manufacturer manufacturer);
        Task SaveChangesAsync(CancellationToken cancellationToken);
    }
}
