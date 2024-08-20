using MediatR;
using ProductCatalog.Application.Common.Abstractions;
using ProductCatalog.Application.Common.Exceptions;
using ProductCatalog.Application.Common.Interfaces;

namespace ProductCatalog.Application.Application.Commands.Category.DeleteCategory
{
    public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand>
    {
        private readonly ICategoryRepository _repository;
        public DeleteCategoryCommandHandler(ICategoryRepository repository) =>
            _repository = repository ?? throw new ArgumentNullException(nameof(ICategoryRepository));
        public async Task Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = await _repository.GetCategoryByIdAsync(request.CategoryId, cancellationToken);
            if (category == null) 
                throw new NotFoundExceptions(nameof(category), request.CategoryId);

            _repository.DeleteCategory(category);
            await _repository.SaveChangesAsync(cancellationToken);
        }
    }
}
