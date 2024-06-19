using MediatR;

namespace ShoppingCart.Application.Application.Queries.ProductRange.GetProductRangeDetails
{
    public class GerProductRangeQuery : IRequest<ProductRangeDetailsResponse>
    {
        public Guid Id { get; set; }
    }
}
