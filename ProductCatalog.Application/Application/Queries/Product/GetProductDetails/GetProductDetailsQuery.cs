using MediatR;

namespace ProductCatalog.Application.Application.Queries.Product.GetProductDetails
{
    public class GetProductDetailsQuery : IRequest<ProductDetailsResponse>
    {
        public Guid ProductId { get; set; }
    }
}
