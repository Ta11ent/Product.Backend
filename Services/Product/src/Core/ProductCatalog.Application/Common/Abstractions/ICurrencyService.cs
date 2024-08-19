using ProductCatalog.Application.Common.Models;

namespace ProductCatalog.Application.Common.Abstractions
{
    public interface ICurrencyService
    {
        Task<CurrencyDto> GetCurrentROEofCurrency(string code, CancellationToken token);
    }
}
