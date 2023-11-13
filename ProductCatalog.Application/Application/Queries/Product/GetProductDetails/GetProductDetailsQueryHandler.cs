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
                _dbContext.Products
                .Where(x => x.ProductId == request.ProductId)
                .ProjectTo<ProductDetailsDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(cancellationToken);

            if (product == null)
                throw new NotFoundExceptions(nameof(product), request.ProductId);

            return new ProductDetailsResponse(product);
        }
    }
}
