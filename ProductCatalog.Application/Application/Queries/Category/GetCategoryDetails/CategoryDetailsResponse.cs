using ProductCatalog.Application.Common.Response;

namespace ProductCatalog.Application.Application.Queries.Category.GetCategoryDetails
{
    public class CategoryDetailsResponse : Response<CategoryDetailsDto>
    {
        public CategoryDetailsResponse(CategoryDetailsDto detailsDto)
            : base(detailsDto) { }
    }
}
