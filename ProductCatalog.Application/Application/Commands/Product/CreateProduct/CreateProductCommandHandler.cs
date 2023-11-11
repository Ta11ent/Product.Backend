using MediatR;
using ProductCatalog.Application.Common.Interfaces;

namespace ProductCatalog.Application.Application.Commands.Product.CreateProduct
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Guid>
    {
        private readonly IProductDbContext _dbContext;
        public CreateProductCommandHandler(IProductDbContext dbContext) =>
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        public async Task<Guid> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var product = new Domain.Product
            {
                ProductId = Guid.NewGuid(),
                Name = request.Name,
                Description = request.Description 
            };

            await _dbContext.Products.AddAsync(product);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return product.ProductId;
        }
    }
}
