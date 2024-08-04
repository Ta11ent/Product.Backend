using MediatR;

namespace ProductCatalog.Application.Application.Queries.Category.GetCategoryDetails
{
    public class GetCategoryDetailsQuery : IRequest<CategoryDetailsResponse>
    {
        public Guid CategoryId { get; set; }
    }
}
