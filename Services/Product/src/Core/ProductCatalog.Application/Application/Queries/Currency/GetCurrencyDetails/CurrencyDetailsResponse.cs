using ProductCatalog.Application.Common.Response;

namespace ProductCatalog.Application.Application.Queries.Currency.GetCurreencyDetails
{
    public class CurrencyDetailsResponse : Response<CurrencyDetailsDto>
    {
        public CurrencyDetailsResponse(CurrencyDetailsDto data) : base(data) { }
    }
}
