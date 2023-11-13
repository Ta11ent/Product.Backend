using ProductCatalog.Application.Common.Abstractions;
using ProductCatalog.Application.Common.Response;

namespace ProductCatalog.Application.Application.Queries.Product.GetProductList
{
    public class ProductListResponse : PageResponse<List<ProductListDto>>
    {
        public ProductListResponse(List<ProductListDto> products, IPagination pagination) 
            : base(products, pagination) { }
    }
}
