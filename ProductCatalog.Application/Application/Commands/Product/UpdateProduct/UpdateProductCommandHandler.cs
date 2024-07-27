using MediatR;
using Microsoft.EntityFrameworkCore;
using ProductCatalog.Application.Common.Exceptions;
using ProductCatalog.Application.Common.Interfaces;
using System.Diagnostics;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace ProductCatalog.Application.Application.Commands.Product.UpdateProduct
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand>
    {
        private readonly IProductDbContext _dbContext;
        public UpdateProductCommandHandler(IProductDbContext dbContect) =>
            _dbContext = dbContect ?? throw new ArgumentNullException(nameof(dbContect));
        public async Task Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var product = await
                _dbContext.Products
                .Include(x => x.Costs)
                .FirstOrDefaultAsync(x => x.SubCategory.CategoryId == request.CategoryId
                    && x.SubCategoryId == request.SubCategoryId
                    && x.ProductId == request.ProductId, cancellationToken);

            if (product == null)
                throw new NotFoundExceptions(nameof(product), cancellationToken);

            product.Name = request.Name;
            product.Description = request.Description;
            product.SubCategoryId = request.SubCategoryId;
            product.Available = request.Available is null
                ? product.Available
                : request.Available.Value;

            var cost = product.Costs.OrderByDescending(y => y.DatePrice).FirstOrDefault();

            if (cost.Price != request.Price)
                await _dbContext.Costs.AddAsync(new Domain.Cost()
                {
                    PriceId = Guid.NewGuid(),
                    ProductId = request.ProductId,
                    Price = request.Price,
                    DatePrice = DateTime.Now,
                    CurrencyId = cost.CurrencyId
                });

            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
