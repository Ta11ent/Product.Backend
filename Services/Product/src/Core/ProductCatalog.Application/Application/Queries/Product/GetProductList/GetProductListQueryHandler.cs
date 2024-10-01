using AutoMapper;
using MediatR;
using ProductCatalog.Application.Common.Abstractions;
using ProductCatalog.Application.Common.Models;
using ProductCatalog.Application.Common.Predicate;


namespace ProductCatalog.Application.Application.Queries.Product.GetProductList
{
    public class GetProductListQueryHandler : IRequestHandler<GetProductListQuery, ProductListResponse>
    {
        private readonly IProductRepository _productRepository;
        private readonly ICurrencyRepository _currencyRepository;
        private readonly IMapper _mapper;
       
        public GetProductListQueryHandler(
            IProductRepository productRepository, 
            ICurrencyRepository currencyRepository,
            IMapper mapper)
        {
            _productRepository = productRepository;
            _currencyRepository = currencyRepository;
            _mapper = mapper;
        }

        public async Task<ProductListResponse> Handle(GetProductListQuery request, CancellationToken cancellationToken)
        {
            var predicate = PredicateBuilder
                .True<Domain.ProductSale>()
                .And(x => x.SubCategory.CategoryId == request.CategoryId,
                        request.CategoryId)
                    .And(x => x.SubCategoryId == request.SubCategoryId,
                        request.SubCategoryId)
                    .And(x => x.Available == request.Available,
                        request.Available)
                    .And(x => request.ProductIds!.Contains(x.ProductSaleId),
                        request.ProductIds!.Any() ? request.ProductIds : null);

            var products = await _productRepository.GetAllProductsAsync(predicate, request, cancellationToken);
            var productsdto = _mapper.Map<List<ProductListDto>>(products);

            if (!String.IsNullOrEmpty(request.CurrencyCode))
            {
                var currencydto = await _currencyRepository.GetCurrencyWithActiveROEAsync(request.CurrencyCode, cancellationToken);
                var currency = _mapper.Map<CurrencyDto>(currencydto);

                if (currency != null) { 

                    foreach(var product in productsdto)
                    {
                        product.Ccy = currency.Code;
                        product.Price = Math.Round((product.Price / product.Rate) * currency.Rate, 4);
                        product.Rate = currency.Rate;
                    }
                }

            }

            return new ProductListResponse(productsdto, request);
        }
    }
}
