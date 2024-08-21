using ProductCatalog.Application.Common.Abstractions;
using ProductCatalog.Application.Common.Pagination;
using ProductCatalog.Domain;

namespace ProductCatalog.Infrastructure.Repositories
{
    public class CashedCurrencyRepoitory : ICurrencyRepository
    {
        private readonly ICurrencyRepository _decorated;
        private readonly ICashService _cashService;
        public CashedCurrencyRepoitory(ICurrencyRepository decorated, ICashService cashService)
        {
            _decorated = decorated;
            _cashService = cashService;
        }

        public async Task CreateCurrencyAsync(Currency currency, CancellationToken cancellationToken)
        {
            await _decorated.CreateCurrencyAsync(currency, cancellationToken);
            await _cashService.CreateAsync<Currency>(
                currency.CurrencyId,
                currency,
                null,
                cancellationToken);
        }

        public async void DeleteCurrency(Currency currency)
        {
            _decorated.DeleteCurrency(currency);
            await _cashService.DeleteByIdAsync<Currency>(currency.CurrencyId, new CancellationTokenSource().Token);
        }

        public async Task<IEnumerable<Currency>> GetAllCurrenciesAsync(IPagination pagination, CancellationToken cancellationToken)
            => await _decorated.GetAllCurrenciesAsync(pagination, cancellationToken);

        public async Task<Currency> GetCurrencyByIdAsync(Guid currencyId, CancellationToken cancellationToken)
            => await _cashService.GetByIdAsync(
                currencyId,
                async() => await _decorated.GetCurrencyByIdAsync(currencyId, cancellationToken),
                null,
                cancellationToken);

        public Task SaveChangesAsync(CancellationToken cancellationToken)
            => _decorated.SaveChangesAsync(cancellationToken);
    }
}
