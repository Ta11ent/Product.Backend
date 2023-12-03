using ShoppingCart.Application.Common.Response;

namespace ShoppingCart.Application.Common.Models.ProductRange
{
    public class ProductRangeDetailsResponse : Response<ProductRangeDetailsDto>
    {
        public ProductRangeDetailsResponse(ProductRangeDetailsDto data)
            : base(data) { }
    }
}
