using ProductCatalog.Application.Common.Abstractions;
using ProductCatalog.Application.Common.Pagination;
using ProductCatalog.Domain;

namespace ProductCatalog.Infrastructure.Repositories
{
    public class CashedManufacturerRepository : IManufacturerRepository
    {
        private readonly IManufacturerRepository _decorated;
        private readonly ICashService _cashService;
        public CashedManufacturerRepository(IManufacturerRepository decorated, ICashService cashService)
        {
            _decorated = decorated;
            _cashService = cashService;
        }
        public async Task CreateManufacturerAsync(Manufacturer manufacturer, CancellationToken cancellationToken)
        {
            await _decorated.CreateManufacturerAsync(manufacturer, cancellationToken);
            await _cashService.CreateAsync<Manufacturer>(
                manufacturer.ManufacturerId,
                manufacturer,
                null,
                cancellationToken);
        }
        public async void DeleteManufacturer(Manufacturer manufacturer)
        {
            _decorated.DeleteManufacturer(manufacturer);
            await _cashService.DeleteByIdAsync<Manufacturer>(manufacturer.ManufacturerId, new CancellationTokenSource().Token);
        }
        public async Task<IEnumerable<Manufacturer>> GetAllManufacturersAsync(IPagination pagination, CancellationToken cancellationToken)
            => await _decorated.GetAllManufacturersAsync(pagination, cancellationToken);
        public async Task<Manufacturer> GetManufacturerByIdAsync(Guid manufacturerId, CancellationToken cancellationToken)
            => await _cashService.GetByIdAsync(
                manufacturerId,
                async() => await _decorated.GetManufacturerByIdAsync(manufacturerId, cancellationToken),
                null,
                cancellationToken);
        public async Task SaveChangesAsync(CancellationToken cancellationToken) => await _decorated.SaveChangesAsync(cancellationToken);
    }
}
