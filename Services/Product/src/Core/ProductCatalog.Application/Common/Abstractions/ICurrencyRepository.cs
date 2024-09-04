using ProductCatalog.Application.Common.Pagination;
using ProductCatalog.Domain;

namespace ProductCatalog.Application.Common.Abstractions
{
    public interface ICurrencyRepository
    {
        Task<Currency> GetCurrencyByIdAsync(Guid currencyId, CancellationToken cancellationToken);
        Task<Currency> GetCurrencyWithActiveROEAsync(string code, CancellationToken cancellationToken);
        Task<IEnumerable<Currency>> GetAllCurrenciesAsync(IPagination pagination, CancellationToken cancellationToken);
        Task CreateCurrencyAsync(Currency currency, CancellationToken cancellationToken);
        void DeleteCurrency(Currency currency);
        Task SaveChangesAsync(CancellationToken cancellationToken);
    }
}
