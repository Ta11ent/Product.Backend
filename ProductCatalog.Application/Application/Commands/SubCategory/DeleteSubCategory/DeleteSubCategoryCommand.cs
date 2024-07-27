using MediatR;

namespace ProductCatalog.Application.Application.Commands.SubCategory.DeleteSubCategory
{
    public class DeleteSubCategoryCommand : IRequest
    {
        public Guid CategoryId { get; set; }
        public Guid SubCategoryId { get; set; }
    }
}
