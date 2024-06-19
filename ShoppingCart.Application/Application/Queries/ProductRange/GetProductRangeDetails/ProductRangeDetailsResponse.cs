using ShoppingCart.Application.Common.Response;

namespace ShoppingCart.Application.Application.Queries.ProductRange.GetProductRangeDetails
{
    public class ProductRangeDetailsResponse : Response<ProductRangeDetailsDto>
    {
        public ProductRangeDetailsResponse(ProductRangeDetailsDto data) : base(data) { }
    }
}
