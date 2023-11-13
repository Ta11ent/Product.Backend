using MediatR;
using ProductCatalog.Application.Common.Pagination;

namespace ProductCatalog.Application.Application.Queries.Product.GetProductList
{
    public class GetProductListQuery : Pagination, IRequest<ProductListResponse> { }
}
