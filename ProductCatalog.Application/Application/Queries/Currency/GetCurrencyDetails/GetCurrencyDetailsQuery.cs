using MediatR;

namespace ProductCatalog.Application.Application.Queries.Currency.GetCurreencyDetails
{
    public class GetCurrencyDetailsQuery : IRequest<CurrencyDetailsResponse>
    {
        public Guid CurrencyId { get; set; }
    }
}
