using AutoMapper;
using MediatR;
using ProductCatalog.Application.Common.Abstractions;
using ProductCatalog.Application.Common.Exceptions;
using ProductCatalog.Application.Common.Models;
using ProductCatalog.Application.Common.Predicate;

namespace ProductCatalog.Application.Application.Queries.Product.GetProductDetails
{
    public class GetProductDetailsQueryHandler : IRequestHandler<GetProductDetailsQuery, ProductDetailsResponse>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        private readonly ICurrencyRepository _currencyRepository;

        public GetProductDetailsQueryHandler(
            IProductRepository productRepository, 
            IMapper mapper, 
            ICurrencyRepository currencyRepository)
        {
            _productRepository = productRepository ?? throw new ArgumentNullException(nameof(IProductRepository));
            _mapper = mapper;
            _currencyRepository = currencyRepository;
        }

        public async Task<ProductDetailsResponse> Handle(GetProductDetailsQuery request, CancellationToken cancellationToken)
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

            var product = _mapper.Map<ProductDetailsDto>(productSale);

            if (!String.IsNullOrEmpty(request.CurrencyCode)) {
                var currencydto = await _currencyRepository.GetCurrencyWithActiveROEAsync(request.CurrencyCode, cancellationToken);
                var currency = _mapper.Map<CurrencyDto>(currencydto);

                if (currency != null)
                {
                    product.Ccy = currency.Code;
                    product.Price = Math.Round((product.Price / product.Rate) * currency.Rate, 4);
                    product.Rate = currency.Rate;
                }
            }

            return new ProductDetailsResponse(product);
        }
    }
}
