using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ProductCatalog.Application.Common.Abstractions;
using ProductCatalog.Application.Common.Exceptions;
using ProductCatalog.Application.Common.Interfaces;
using ProductCatalog.Application.Common.Models;
using ProductCatalog.Application.Common.Predicate;

namespace ProductCatalog.Application.Application.Queries.Product.GetProductDetails
{
    public class GetProductDetailsQueryHandler : IRequestHandler<GetProductDetailsQuery, ProductDetailsResponse>
    {
        private readonly IProductDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ICurrencyRepository _currencyRepository;

        public GetProductDetailsQueryHandler(IProductDbContext dbContext, IMapper mapper, ICurrencyRepository currencyRepository)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _mapper = mapper;
            _currencyRepository = currencyRepository;
        }

        public async Task<ProductDetailsResponse> Handle(GetProductDetailsQuery request, CancellationToken cancellationToken)
        {
            var predicate = PredicateBuilder.True<Domain.ProductSale>();

            var product = await
                _dbContext.ProductSale
                .Include(x => x.SubCategory)
                    .ThenInclude(x => x.Category)
                .Include(x => x.Costs
                    .OrderByDescending(y => y.DatePrice).Take(1))
                    .ThenInclude(x => x.Currency)
                        .ThenInclude(x => x.ROEs
                            .OrderByDescending(x => x.DateFrom).Take(1))
                .Include(x => x.Product)
                    .ThenInclude(x => x.Manufacturer)
                .Where(predicate
                    .And(x => x.SubCategory.CategoryId == request.CategoryId,
                        request.CategoryId)
                    .And(x => x.SubCategoryId == request.SubCategoryId, 
                        request.SubCategoryId)
                    .And(x => x.ProductSaleId == request.ProductId))
                .ProjectTo<ProductDetailsDto>(_mapper.ConfigurationProvider)
                .AsNoTracking()
                .FirstOrDefaultAsync(cancellationToken);

            if (product == null)
                throw new NotFoundExceptions(nameof(product), request.ProductId);

            if (!String.IsNullOrEmpty(request.CurrencyCode)) {
                //var currency = await _currency.GetCurrentROEofCurrency(request.CurrencyCode, cancellationToken);
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
