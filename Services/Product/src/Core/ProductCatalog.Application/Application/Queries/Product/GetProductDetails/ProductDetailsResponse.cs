using ProductCatalog.Application.Common.Response;

namespace ProductCatalog.Application.Application.Queries.Product.GetProductDetails
{
    public class ProductDetailsResponse : Response<ProductDetailsDto>
    {
        public ProductDetailsResponse(ProductDetailsDto productDetails) 
            : base(productDetails) { }
    }
}
