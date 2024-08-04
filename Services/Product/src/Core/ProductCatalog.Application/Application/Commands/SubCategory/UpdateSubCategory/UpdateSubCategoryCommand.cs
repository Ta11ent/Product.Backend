using MediatR;

namespace ProductCatalog.Application.Application.Commands.SubCategory.UpdateSubCategory
{
    public class UpdateSubCategoryCommand : IRequest
    {
        public Guid CategoryId { get; set; }
        public Guid SubCategoryId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty ;
    }
}
