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
            var predicate = PredicateBuilder.True<Domain.Product>();

            var products = await
                _dbContext.Products
                .Include(x => x.Costs)
                .Where(predicate
                    .And(x => x.CategoryId == request.CategoryId,
                        request.CategoryId)
                    .And(x => x.Available == request.Available,
                        request.Available)
                    .And(x => request.ProductIds!.Contains(x.ProductId),
                        request.ProductIds!.Any() ? request.ProductIds : null))
                .Skip((request.Page - 1) * request.PageSize)
                .Take(request.PageSize)
                .OrderBy(x => x.Name)
                .ProjectTo<ProductListDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return new ProductListResponse(products, request);
        }
    }
}
