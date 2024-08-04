using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ProductCatalog.Application.Common.Interfaces;
using ProductCatalog.Application.Common.Predicate;


namespace ProductCatalog.Application.Application.Queries.Product.GetProductList
{
    public class GetProductListQueryHandler : IRequestHandler<GetProductListQuery, ProductListResponse>
    {
        private readonly IProductDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetProductListQueryHandler(IProductDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _mapper = mapper;
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
                var roe =
                 await _dbContext.ROE
                   .Where(x => x.Currency.Code == request.CurrencyCode)
                   .OrderByDescending(x => x.DateFrom)
                   .FirstOrDefaultAsync(cancellationToken);

                foreach(var product in products)
                {
                    product.Ccy = request.CurrencyCode;
                    product.Price = (product.Price / product.Rate) * roe.Rate;
                    product.Rate = roe.Rate;
                }

            }

            return new ProductListResponse(products, request);
        }
    }
}
