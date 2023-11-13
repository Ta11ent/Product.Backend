using ProductCatalog.Application.Common.Pagination;
using ProductCatalog.Application.Common.Response;

namespace ProductCatalog.Application.Application.Queries.Category.GetCategoryList
{
    public class CategoryListResponse : PageResponse<List<CategoryListDto>>
    {
        public CategoryListResponse(List<CategoryListDto> categoryList, IPagination pagination) 
            : base(categoryList, pagination) { }
    }
}
