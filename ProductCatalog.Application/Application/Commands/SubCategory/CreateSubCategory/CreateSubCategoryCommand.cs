using MediatR;

namespace ProductCatalog.Application.Application.Commands.SubCategory.CreateSubCategory
{
    public class CreateSubCategoryCommand : IRequest<Guid>
    {
        public Guid CategoryId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }
}
