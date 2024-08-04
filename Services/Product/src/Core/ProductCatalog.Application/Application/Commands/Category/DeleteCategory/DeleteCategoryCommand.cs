using MediatR;

namespace ProductCatalog.Application.Application.Commands.Category.DeleteCategory
{
    public class DeleteCategoryCommand : IRequest
    {
        public Guid CategoryId { get; set; }
    }
}
