using ProductCatalog.Domain;

namespace ProductCatalog.Application.Common.Abstractions
{
    public interface IROERepository
    {
        Task<ROE> GetROEByIdAsync(Guid currencyId, Guid roeId, CancellationToken cancellationToken);
        Task CreateROEAsync(ROE roe, CancellationToken cancellationToken);
        void DeleteROE(ROE roe);
        Task SaveChangesAsync(CancellationToken cancellationToken);
    }
}
