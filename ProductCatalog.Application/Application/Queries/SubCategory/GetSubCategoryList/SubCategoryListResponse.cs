using ProductCatalog.Application.Common.Pagination;
using ProductCatalog.Application.Common.Response;

namespace ProductCatalog.Application.Application.Queries.SubCategory.GetSubCategoryList
{
    public class SubCategoryListResponse : PageResponse<List<SubCategoryListDto>>
    {
        public SubCategoryListResponse(List<SubCategoryListDto> data, IPagination pagination)
            : base(data, pagination) { }
    }
}
