using ProductCatalog.Application.Common.Pagination;
using ProductCatalog.Application.Common.Response;

namespace ProductCatalog.Application.Application.Queries.Currency.GetCurrencyList
{
    public class CurrencyListResponse : PageResponse<List<CurrencyListDto>>
    {
        public CurrencyListResponse(List<CurrencyListDto> data, IPagination pagination)
            : base(data, pagination) { }
    }
}
