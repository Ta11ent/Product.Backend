using MediatR;
using ProductCatalog.Application.Common.Abstractions;

namespace ProductCatalog.Application.Application.Commands.SubCategory.CreateSubCategory
{
    public class CreateSubCategoryQueryHandler : IRequestHandler<CreateSubCategoryCommand, Guid>
    {
        private readonly ISubCategoryRepository _repository;
        public CreateSubCategoryQueryHandler(ISubCategoryRepository repository) =>
            _repository = repository ?? throw new ArgumentNullException(nameof(ISubCategoryRepository));
        public async Task<Guid> Handle(CreateSubCategoryCommand request, CancellationToken cancellationToken)
        {
            var subcategory = new Domain.SubCategory
            {
                CategoryId = request.CategoryId,
                SubCategoryId = Guid.NewGuid(),
                Name = request.Name,
                Description = request.Description
            };

            await _repository.CreateSubCategoryAsync(subcategory, cancellationToken);
            await _repository.SaveChangesAsync(cancellationToken);

            return subcategory.SubCategoryId;
        }
    }
}
