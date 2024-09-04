using Microsoft.EntityFrameworkCore;
using ProductCatalog.Application.Common.Abstractions;
using ProductCatalog.Application.Common.Interfaces;
using ProductCatalog.Application.Common.Pagination;
using ProductCatalog.Domain;

namespace ProductCatalog.Infrastructure.Repositories
{
    public class CurrencyRepository : BaseRepository, ICurrencyRepository
    {
        public CurrencyRepository(IProductDbContext dbContext) : base(dbContext) { }
        public async Task CreateCurrencyAsync(Currency currency, CancellationToken cancellationToken)
            => await _dbContext.Currency.AddAsync(currency);
        public void DeleteCurrency(Currency currency) => _dbContext.Currency.Remove(currency);

        public async Task<IEnumerable<Currency>> GetAllCurrenciesAsync(IPagination pagination, CancellationToken cancellationToken)
        {
            var data = await
                _dbContext.Currency
                  .Skip((pagination.Page - 1) * pagination.PageSize)
                  .Take(pagination.PageSize)
                  .ToListAsync(cancellationToken);
            return data;
        }
        public async Task<Currency> GetCurrencyWithActiveROEAsync(string code, CancellationToken cancellationToken)
        {
            var data = await _dbContext.Currency
                .Where(x => x.Code == code)
                .Include(x => x.ROEs.OrderByDescending(y => y.DateFrom).Take(1))
                .FirstOrDefaultAsync(cancellationToken);
            return data!;
        }
        public async Task<Currency> GetCurrencyByIdAsync(Guid currencyId, CancellationToken cancellationToken)
        {
            var data =
                await _dbContext.Currency
                    .Include(x => x.ROEs)
                    .FirstOrDefaultAsync(x => x.CurrencyId == currencyId, cancellationToken);

            return data!;
        }
    }
}
