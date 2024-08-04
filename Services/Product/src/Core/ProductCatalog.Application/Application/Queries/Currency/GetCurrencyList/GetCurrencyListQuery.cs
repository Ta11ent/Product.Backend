using MediatR;
using ProductCatalog.Application.Common.Pagination;

namespace ProductCatalog.Application.Application.Queries.Currency.GetCurrencyList
{
    public class GetCurrencyListQuery : Pagination, IRequest<CurrencyListResponse>
    { }
}
