using MediatR;
using ProductCatalog.Application.Common.Abstractions;

namespace ProductCatalog.Application.Application.Commands.Category.CreateCategory
{
    public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, Guid>
    {
        private ICategoryRepository _repository;
        public CreateCategoryCommandHandler(ICategoryRepository repository) =>
            _repository = repository ?? throw new ArgumentNullException(nameof(ICategoryRepository));
        public async Task<Guid> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = new Domain.Category
            {
                CategoryId = Guid.NewGuid(),
                Name = request.Name,
                Description = request.Description
            };

            await _repository.CreateCategoryAsync(category, cancellationToken);
            await _repository.SaveChangesAsync(cancellationToken);

            return category.CategoryId;
        }
    }
}
