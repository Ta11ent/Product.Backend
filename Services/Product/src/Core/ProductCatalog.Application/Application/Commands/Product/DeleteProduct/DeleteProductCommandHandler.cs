using MediatR;
using ProductCatalog.Application.Common.Abstractions;
using ProductCatalog.Application.Common.Exceptions;
using ProductCatalog.Application.Common.Predicate;

namespace ProductCatalog.Application.Application.Commands.Product.DeleteProduct
{
    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand>
    {
        private readonly IProductRepository _productRepository;
        public DeleteProductCommandHandler(IProductRepository productRepository) =>
            _productRepository = productRepository ?? throw new ArgumentNullException(nameof(IProductRepository));

        public async Task Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var predicate = PredicateBuilder
               .True<Domain.ProductSale>()
               .And(x => x.SubCategory.CategoryId == request.CategoryId,
                       request.CategoryId)
                   .And(x => x.SubCategoryId == request.SubCategoryId,
                       request.SubCategoryId)
                   .And(x => x.ProductSaleId == request.ProductId,
                       request.ProductId);

            var product = await _productRepository.GetProductByIdAsync(request.ProductId, predicate, cancellationToken);
            if (product == null)
                throw new NotFoundExceptions(nameof(product), request.ProductId);

            _productRepository.DeleteProduct(product);

            await _productRepository.SaveChangesAsync(cancellationToken);
        }
    }
}
