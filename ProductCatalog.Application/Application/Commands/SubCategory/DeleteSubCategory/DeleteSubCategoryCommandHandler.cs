using MediatR;
using Microsoft.EntityFrameworkCore;
using ProductCatalog.Application.Common.Exceptions;
using ProductCatalog.Application.Common.Interfaces;

namespace ProductCatalog.Application.Application.Commands.SubCategory.DeleteSubCategory
{
    public class DeleteSubCategoryCommandHandler : IRequestHandler<DeleteSubCategoryCommand>
    {
        private readonly IProductDbContext _dbContext;
        public DeleteSubCategoryCommandHandler(IProductDbContext dbContext) =>
            _dbContext = dbContext ?? throw new ArgumentNullException("ProductDbContext");
        public async Task Handle(DeleteSubCategoryCommand request, CancellationToken cancellationToken)
        {
            var subCategory = 
                await _dbContext.SubCategories
                    .FirstOrDefaultAsync(x => x.CategoryId == request.CategoryId
                        && x.SubCategoryId == request.SubCategoryId);
            if (subCategory == null)
                throw new NotFoundExceptions(nameof(subCategory), request.SubCategoryId);

            _dbContext.SubCategories.Remove(subCategory);

            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
