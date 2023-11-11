using MediatR;
using Microsoft.EntityFrameworkCore;
using ProductCatalog.Application.Common.Exceptions;
using ProductCatalog.Application.Common.Interfaces;

namespace ProductCatalog.Application.Application.Commands.Product.UpdateProduct
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand>
    {
        private readonly IProductDbContext _dbContect;
        public UpdateProductCommandHandler(IProductDbContext dbContect) =>
            _dbContect = dbContect ?? throw new ArgumentNullException(nameof(dbContect));
        public async Task Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var product = await
                _dbContect.Products
                .FirstOrDefaultAsync(c => c.ProductId == request.ProductId);

            if (product == null)
                throw new NotFoundExceptions(nameof(product), cancellationToken);

            product.Name = request.Name;
            product.Description = request.Description;

            await _dbContect.SaveChangesAsync(cancellationToken);
        }
    }
}
