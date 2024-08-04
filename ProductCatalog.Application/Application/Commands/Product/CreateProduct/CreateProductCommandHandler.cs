using MediatR;
using Microsoft.EntityFrameworkCore;
using ProductCatalog.Application.Common.Exceptions;
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
            var subCategory =
                await _dbContext.SubCategories
                    .FirstOrDefaultAsync(x => x.CategoryId == request.CategoryId
                        && x.SubCategoryId == request.SubCategoryId, cancellationToken);
            if (subCategory == null)
                throw new NotFoundExceptions(nameof(subCategory), request.SubCategoryId);


            var product = new Domain.Product()
            {
                ProductId = Guid.NewGuid(),
                Name = request.Name,
                Description = request.Description,
                ManufacturerId = request.ManufacturerId
            };

            var productSale = new Domain.ProductSale()
            {
                ProductSaleId = Guid.NewGuid(),
                //CategoryId = request.CategoryId,
                SubCategoryId = request.SubCategoryId,
                ProductId = product.ProductId,
                Available = true
            };

            var cost = new Domain.Cost()
            {
                CostId = Guid.NewGuid(),
                ProductSaleId = productSale.ProductSaleId,
                Price = request.Price,
                DatePrice = DateTime.Now,
                CurrencyId = request.CurrencyId,
            };


            await _dbContext.Products.AddAsync(product);
            await _dbContext.ProductSale.AddAsync(productSale);
            await _dbContext.Costs.AddAsync(cost);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return productSale.ProductSaleId;
        }
    }
}
