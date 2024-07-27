using MediatR;
using Microsoft.EntityFrameworkCore;
using ProductCatalog.Application.Common.Exceptions;
using ProductCatalog.Application.Common.Interfaces;

namespace ProductCatalog.Application.Application.Commands.SubCategory.UpdateSubCategory
{
    public class UpdateSubCategoryCommandHandler : IRequestHandler<UpdateSubCategoryCommand>
    {
        private readonly IProductDbContext _dbContext;
        public UpdateSubCategoryCommandHandler(IProductDbContext dbContext) =>
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));

        public async Task Handle(UpdateSubCategoryCommand request, CancellationToken cancellationToken)
        {
            var subCategory = await
                _dbContext.SubCategories
                .FirstOrDefaultAsync(x => x.CategoryId == request.CategoryId
                    && x.SubCategoryId == request.SubCategoryId);

            if (subCategory == null)
                throw new NotFoundExceptions(nameof(subCategory), request.SubCategoryId);

            subCategory.Name = request.Name;
            subCategory.Description = request.Description;

            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
