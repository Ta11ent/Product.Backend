using MediatR;
using ProductCatalog.Application.Common.Abstractions;
using ProductCatalog.Application.Common.Exceptions;
using ProductCatalog.Application.Common.Predicate;


namespace ProductCatalog.Application.Application.Commands.Product.UpdateProduct
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand>
    {
        private readonly IProductRepository _productRepository;
        public UpdateProductCommandHandler(IProductRepository productRepository) =>
            _productRepository = productRepository ?? throw new ArgumentNullException(nameof(IProductRepository));
        public async Task Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var predicate = PredicateBuilder
               .True<Domain.ProductSale>()
               .And(x => x.SubCategory.CategoryId == request.CategoryId,
                       request.CategoryId)
                   .And(x => x.SubCategoryId == request.SubCategoryId,
                       request.SubCategoryId)
                   .And(x => x.ProductSaleId == request.ProductId,
                       request.ProductId);

            var productSale = await _productRepository.GetProductByIdAsync(request.ProductId, predicate, cancellationToken);
            if (productSale == null)
                throw new NotFoundExceptions(nameof(productSale), request.ProductId);

            productSale.Product.Update(request.Name, request.Description, request.ManufacturerId);
            productSale.Update(request.SubCategoryId, request.Available ?? default);

            var cost = productSale.Costs.OrderByDescending(y => y.DatePrice).FirstOrDefault();
            if (cost!.Price != request.Price)
                await _productRepository.CreateProductCostAsync(
                    new Domain.Cost().Create(
                        productSale.ProductSaleId, 
                        request.Price,
                        cost.CurrencyId),
                    cancellationToken);

            await _productRepository.SaveChangesAsync(cancellationToken);
        }
    }
}
