using MediatR;

namespace ProductCatalog.Application.Application.Queries.ROE.GetROEDetails
{
    public class GetROEDetailsQuery : IRequest<ROEDetailsResponse>
    {
        public Guid CurrencyId { get; set; }
        public Guid ROEId { get; set; }
    }
}
