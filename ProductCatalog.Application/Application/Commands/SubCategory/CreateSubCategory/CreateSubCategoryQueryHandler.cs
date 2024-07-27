using MediatR;
using ProductCatalog.Application.Common.Interfaces;

namespace ProductCatalog.Application.Application.Commands.SubCategory.CreateSubCategory
{
    public class CreateSubCategoryQueryHandler : IRequestHandler<CreateSubCategoryCommand, Guid>
    {
        private readonly IProductDbContext _dbContext;
        public CreateSubCategoryQueryHandler(IProductDbContext dbContext) =>
            _dbContext = dbContext ?? throw new ArgumentNullException("PRoductDbContext");
        public async Task<Guid> Handle(CreateSubCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = new Domain.SubCategory
            {
                CategoryId = request.CategoryId,
                SubCategoryId = Guid.NewGuid(),
                Name = request.Name,
                Description = request.Description
            };

            await _dbContext.SubCategories.AddAsync(category);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return category.SubCategoryId;
        }
    }
}
