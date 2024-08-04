using MediatR;
using ProductCatalog.Application.Common.Pagination;

namespace ProductCatalog.Application.Application.Queries.SubCategory.GetSubCategoryList
{
    public class GetSubCategoryListQuery : Pagination, IRequest<SubCategoryListResponse>
    {
        public Guid CategoryId { get;set; }
    }
}
