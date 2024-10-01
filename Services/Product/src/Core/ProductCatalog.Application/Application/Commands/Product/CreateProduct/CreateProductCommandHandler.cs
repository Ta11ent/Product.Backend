using MediatR;
using ProductCatalog.Application.Common.Abstractions;
using ProductCatalog.Application.Common.Exceptions;


namespace ProductCatalog.Application.Application.Commands.Product.CreateProduct
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Guid>
    {
        private readonly IProductRepository _productRepository;
        private readonly ISubCategoryRepository _subCategoryRepository;
        public CreateProductCommandHandler(IProductRepository productRepository, ISubCategoryRepository subCategoryRepository)
        {
            _productRepository = productRepository ?? throw new ArgumentNullException(nameof(IProductRepository));
            _subCategoryRepository = subCategoryRepository ?? throw new ArgumentNullException(nameof(ISubCategoryRepository));
        }
        public async Task<Guid> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var subCategory = await _subCategoryRepository.GetSubCategoryByIdAsync(request.CategoryId, request.SubCategoryId, cancellationToken);
            if (subCategory == null)
                throw new NotFoundExceptions(nameof(subCategory), request.SubCategoryId);

            var product = new Domain.Product().Create(request.Name, request.Description, request.ManufacturerId);
            var productSale = new Domain.ProductSale().Create(request.SubCategoryId, product.ProductId);
            var cost = new Domain.Cost().Create(productSale.ProductSaleId, request.Price, request.CurrencyId);

            await _productRepository.CreateProductAsync(product, productSale, cost, cancellationToken);
            await _productRepository.SaveChangesAsync(cancellationToken);   

            return productSale.ProductSaleId;
        }
    }
}
