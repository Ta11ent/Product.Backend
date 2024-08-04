using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ProductCatalog.Application.Common.Exceptions;
using ProductCatalog.Application.Common.Interfaces;

namespace ProductCatalog.Application.Application.Queries.Product.GetProductDetails
{
    public class GetProductDetailsQueryHandler : IRequestHandler<GetProductDetailsQuery, ProductDetailsResponse>
    {
        private readonly IProductDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetProductDetailsQueryHandler(IProductDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _mapper = mapper;
        }

        public async Task<ProductDetailsResponse> Handle(GetProductDetailsQuery request, CancellationToken cancellationToken)
        {
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
                .Where(x => x.SubCategory.CategoryId == request.CategoryId 
                    && x.SubCategoryId == request.SubCategoryId
                    && x.ProductSaleId == request.ProductId)
                .ProjectTo<ProductDetailsDto>(_mapper.ConfigurationProvider)
                .AsNoTracking()
                .FirstOrDefaultAsync(cancellationToken);

            if(!String.IsNullOrEmpty(request.CurrencyCode)) { 
            var roe = 
                await _dbContext.ROE
                    .Where(x => x.Currency.Code == request.CurrencyCode)
                    .OrderByDescending(x => x.DateFrom)
                    .FirstOrDefaultAsync(cancellationToken);

                product.Ccy = request.CurrencyCode;
                product.Price = (product.Price / product.Rate) * roe.Rate;
                product.Rate = roe.Rate;
            }

            if (product == null)
                throw new NotFoundExceptions(nameof(product), request.ProductId);

            return new ProductDetailsResponse(product);
        }
    }
}
