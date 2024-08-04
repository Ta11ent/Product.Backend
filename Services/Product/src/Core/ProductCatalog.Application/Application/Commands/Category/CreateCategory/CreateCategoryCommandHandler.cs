using MediatR;
using ProductCatalog.Application.Common.Interfaces;

namespace ProductCatalog.Application.Application.Commands.Category.CreateCategory
{
    public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, Guid>
    {
        private readonly IProductDbContext _dbContext;
        public CreateCategoryCommandHandler(IProductDbContext dbContext) =>
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        public async Task<Guid> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = new Domain.Category
            {
                CategoryId = Guid.NewGuid(),
                Name = request.Name,
                Description = request.Description
            };

            await _dbContext.Categories.AddAsync(category);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return category.CategoryId;
        }
    }
}
