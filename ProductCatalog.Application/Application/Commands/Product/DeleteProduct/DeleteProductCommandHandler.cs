using MediatR;
using Microsoft.EntityFrameworkCore;
using ProductCatalog.Application.Common.Exceptions;
using ProductCatalog.Application.Common.Interfaces;

namespace ProductCatalog.Application.Application.Commands.Product.DeleteProduct
{
    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand>
    {
        private readonly IProductDbContext _dbContext;
        public DeleteProductCommandHandler(IProductDbContext dbContext)=>
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));

        public async Task Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var product = 
                await _dbContext.ProductSale
                    .FirstOrDefaultAsync(x => x.SubCategoryId == request.SubCategoryId
                        && x.ProductSaleId == request.ProductId, 
                        cancellationToken);
            if (product == null) 
                throw new NotFoundExceptions(nameof(product), request.ProductId);

            product.Available = false;
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
