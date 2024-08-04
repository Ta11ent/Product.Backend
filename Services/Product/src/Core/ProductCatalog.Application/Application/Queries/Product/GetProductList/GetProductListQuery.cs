using MediatR;
using ProductCatalog.Application.Common.Pagination;

namespace ProductCatalog.Application.Application.Queries.Product.GetProductList
{
    public class GetProductListQuery : Pagination, IRequest<ProductListResponse> 
    { 
        public Guid CategoryId { get; set; }
        public Guid SubCategoryId { get; set; }
        public Guid[]? ProductIds { get; set; }
        public bool? Available { get; set; }
        public string? CurrencyCode { get; set; }
    }
}
