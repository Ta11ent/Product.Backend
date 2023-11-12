using ProductCatalog.Application.Common.Abstractions;
using ProductCatalog.Application.Common.Response;

namespace ProductCatalog.Application.Application.Queries.Category.GetCategoryList
{
    public class GetCategoryListResponse : PageResponse<List<CategoryListDto>>
    {
        public GetCategoryListResponse(List<CategoryListDto> categoryList, IPagination pagination) 
            : base(categoryList, pagination) { }
    }
}
