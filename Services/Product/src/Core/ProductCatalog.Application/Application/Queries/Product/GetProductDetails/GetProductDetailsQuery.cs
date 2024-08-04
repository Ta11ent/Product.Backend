using MediatR;

namespace ProductCatalog.Application.Application.Queries.Product.GetProductDetails
{
    public class GetProductDetailsQuery : IRequest<ProductDetailsResponse>
    {
        public Guid CategoryId { get; set; }
        public Guid SubCategoryId { get; set; }
        public Guid ProductId { get; set; }
        public string? CurrencyCode { get; set; }
    }
}
