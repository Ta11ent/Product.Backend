using MediatR;
using ProductCatalog.Application.Common.Abstractions;
using ProductCatalog.Application.Common.Exceptions;

namespace ProductCatalog.Application.Application.Commands.SubCategory.DeleteSubCategory
{
    public class DeleteSubCategoryCommandHandler : IRequestHandler<DeleteSubCategoryCommand>
    {
        private readonly ISubCategoryRepository _repository;
        public DeleteSubCategoryCommandHandler(ISubCategoryRepository repository) =>
            _repository = repository ?? throw new ArgumentNullException(nameof(ISubCategoryRepository));
        public async Task Handle(DeleteSubCategoryCommand request, CancellationToken cancellationToken)
        {
            var subCategory = await _repository.GetSubCategoryByIdAsync(request.CategoryId, request.SubCategoryId, cancellationToken);
            if (subCategory == null)
                throw new NotFoundExceptions(nameof(subCategory), request.SubCategoryId);

            _repository.DeleteSubCategory(subCategory);

            await _repository.SaveChangesAsync(cancellationToken);
        }
    }
}
