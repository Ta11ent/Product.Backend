using MediatR;
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
            var product = await
                _dbContext.Products
                .FindAsync(new object[] { request.ProductId }, cancellationToken);
            
            if (product == null) 
                throw new NotFoundExceptions(nameof(product), request.ProductId);

            _dbContext.Products.Remove(product);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
