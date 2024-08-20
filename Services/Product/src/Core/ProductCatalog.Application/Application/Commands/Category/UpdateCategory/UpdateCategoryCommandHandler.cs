using MediatR;
using ProductCatalog.Application.Common.Abstractions;
using ProductCatalog.Application.Common.Exceptions;

namespace ProductCatalog.Application.Application.Commands.Category.UpdateCategory
{
    public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand>
    {
        private readonly ICategoryRepository _repository;
        public UpdateCategoryCommandHandler(ICategoryRepository repository) =>
            _repository = repository ?? throw new ArgumentNullException(nameof(ICategoryRepository));
        public async Task Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = await _repository.GetCategoryByIdAsync(request.CategoryId, cancellationToken);
            if (category == null)
                throw new NotFoundExceptions(nameof(category), request.CategoryId);

            category.Name = request.Name;
            category.Description = request.Description;

            await _repository.SaveChangesAsync(cancellationToken);
        }
    }
}
