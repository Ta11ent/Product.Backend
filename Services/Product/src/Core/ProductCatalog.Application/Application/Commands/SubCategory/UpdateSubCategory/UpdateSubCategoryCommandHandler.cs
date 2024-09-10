using MediatR;
using ProductCatalog.Application.Common.Abstractions;
using ProductCatalog.Application.Common.Exceptions;

namespace ProductCatalog.Application.Application.Commands.SubCategory.UpdateSubCategory
{
    public class UpdateSubCategoryCommandHandler : IRequestHandler<UpdateSubCategoryCommand>
    {
        private readonly ISubCategoryRepository _repository;
        public UpdateSubCategoryCommandHandler(ISubCategoryRepository repository) =>
            _repository = repository ?? throw new ArgumentNullException(nameof(ISubCategoryRepository));

        public async Task Handle(UpdateSubCategoryCommand request, CancellationToken cancellationToken)
        {
            var subCategory = await _repository.GetSubCategoryByIdAsync(request.CategoryId, request.SubCategoryId, cancellationToken);

            if (subCategory == null)
                throw new NotFoundExceptions(nameof(subCategory), request.SubCategoryId);

            subCategory.Name = request.Name;
            subCategory.Description = request.Description;

            await _repository.SaveChangesAsync(cancellationToken);
        }
    }
}
