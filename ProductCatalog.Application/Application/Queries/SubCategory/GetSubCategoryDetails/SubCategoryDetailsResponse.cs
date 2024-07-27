using ProductCatalog.Application.Common.Response;

namespace ProductCatalog.Application.Application.Queries.SubCategory.GetSubCategoryDetails
{
    public class SubCategoryDetailsResponse : Response<SubCategoryDetailsDto>
    {
        public SubCategoryDetailsResponse(SubCategoryDetailsDto data) : base(data) { }
    }
}
