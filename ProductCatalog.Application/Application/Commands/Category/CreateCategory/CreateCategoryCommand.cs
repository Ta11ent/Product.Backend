using MediatR;

namespace ProductCatalog.Application.Application.Commands.Category.CreateCategory
{
    public class CreateCategoryCommand : IRequest<Guid>
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
