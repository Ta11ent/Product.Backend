using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ShoppingCart.Application.Common.Abstractions;
using ShoppingCart.Application.Common.Exceptions;

namespace ShoppingCart.Application.Application.Queries.ProductRange.GetProductRangeDetails
{
    public class GetProductRangeDetailsHandler : IRequestHandler<GerProductRangeQuery, ProductRangeDetailsResponse>
    {
        private readonly IOrderDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IProductService _productService;
        public GetProductRangeDetailsHandler(IOrderDbContext dbContext, IMapper mapper, IProductService productService)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException("OrderDbContext");
            _mapper = mapper;
            _productService = productService;
        }

        public async Task<ProductRangeDetailsResponse> Handle(GerProductRangeQuery request, CancellationToken cancellationToken)
        {
            var productRange =
               await _dbContext.ProductRanges
               .ProjectTo<ProductRangeDetailsDto>(_mapper.ConfigurationProvider)
               .FirstOrDefaultAsync(x => x.ProductRangeId == request.Id);

            if (productRange is null)
                throw new NotFoundException(nameof(productRange), request.Id);
            else
            {
                var productDetails =
                    await _productService.GetProductByIdAsync(productRange.ProductId);

                productRange.Name = productDetails.Name;
                productRange.Description = productDetails.Description;
                productRange.Available = productDetails.Available;

            }

            return new ProductRangeDetailsResponse(productRange);
        }
    }
}
