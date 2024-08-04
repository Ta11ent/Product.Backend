using MediatR;
using ProductCatalog.Application.Common.Exceptions;
using ProductCatalog.Application.Common.Interfaces;

namespace ProductCatalog.Application.Application.Commands.Category.DeleteCategory
{
    public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand>
    {
        private readonly IProductDbContext _dbContext;
        public DeleteCategoryCommandHandler(IProductDbContext dbContext) =>
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        public async Task Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = await
                _dbContext.Categories
                .FindAsync(new object[] { request.CategoryId }, cancellationToken);

            if (category == null) 
                throw new NotFoundExceptions(nameof(category), request.CategoryId);

            _dbContext.Categories.Remove(category);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
