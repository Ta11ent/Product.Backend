using MediatR;
using ProductCatalog.Application.Common.Pagination;

namespace ProductCatalog.Application.Application.Queries.Category.GetCategoryList
{
    public class GetCategoryListQuery : Pagination, IRequest<GetCategoryListResponse> { }
}
