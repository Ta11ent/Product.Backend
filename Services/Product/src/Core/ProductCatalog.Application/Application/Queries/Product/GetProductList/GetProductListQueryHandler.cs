using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ProductCatalog.Application.Common.Abstractions;
using ProductCatalog.Application.Common.Interfaces;
using ProductCatalog.Application.Common.Predicate;


namespace ProductCatalog.Application.Application.Queries.Product.GetProductList
{
    public class GetProductListQueryHandler : IRequestHandler<GetProductListQuery, ProductListResponse>
    {
        private readonly IProductDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ICurrencyService _currencyService;

        public GetProductListQueryHandler(IProductDbContext dbContext, IMapper mapper, ICurrencyService currency)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _mapper = mapper;
            _currencyService = currency;
        }

        public async Task<ProductListResponse> Handle(GetProductListQuery request, CancellationToken cancellationToken)
        {
            var predicate = PredicateBuilder.True<Domain.ProductSale>();

            var products = await
                _dbContext.ProductSale
                .Include(x => x.Costs.OrderByDescending(y => y.DatePrice).Take(1))
                    .ThenInclude(x => x.Currency)
                        .ThenInclude(x => x.ROEs
                            .OrderByDescending(x => x.DateFrom).Take(1))
                .Where(predicate
                    .And(x => x.SubCategory.CategoryId == request.CategoryId,
                        request.CategoryId)
                    .And(x => x.SubCategoryId == request.SubCategoryId,
                        request.SubCategoryId)
                    .And(x => x.Available == request.Available,
                        request.Available)
                    .And(x => request.ProductIds!.Contains(x.ProductSaleId),
                        request.ProductIds!.Any() ? request.ProductIds : null))
                .Skip((request.Page - 1) * request.PageSize)
                .Take(request.PageSize)
                .OrderBy(x => x.Product.Name)
                .ProjectTo<ProductListDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            if (!String.IsNullOrEmpty(request.CurrencyCode))
            {
                var currency = await _currencyService.GetCurrentROEofCurrency(request.CurrencyCode, cancellationToken);

                if (currency != null) { 

                    foreach(var product in products)
                    {
                        product.Ccy = currency.Code;
                        product.Price = Math.Round((product.Price / product.Rate) * currency.Rate, 4);
                        product.Rate = currency.Rate;
                    }
                }

            }

            return new ProductListResponse(products, request);
        }
    }
}
