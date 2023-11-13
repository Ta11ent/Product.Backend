using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ProductCatalog.Application.Common.Interfaces;

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
            var products = await
                _dbContext.Products
                .Skip((request.Page - 1) * request.PageSize)
                .Take(request.PageSize)
                .ProjectTo<ProductListDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return new ProductListResponse(products, request);
        }
    }
}
