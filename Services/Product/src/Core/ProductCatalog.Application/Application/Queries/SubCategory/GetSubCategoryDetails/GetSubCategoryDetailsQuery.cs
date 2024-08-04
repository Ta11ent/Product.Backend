using MediatR;

namespace ProductCatalog.Application.Application.Queries.SubCategory.GetSubCategoryDetails
{
    public class GetSubCategoryDetailsQuery : IRequest<SubCategoryDetailsResponse>
    {
        public Guid CategoryId { get; set; }
        public Guid SubCategoryId { get; set; }
    }
}
