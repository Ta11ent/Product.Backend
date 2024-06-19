using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ShoppingCart.Application.Application.Queries.ProductRange.GetProductRangeDetails;
using ShoppingCart.Application.Common.Abstractions;
using ShoppingCart.Application.Common.Helpers;
using ShoppingCart.Application.Common.Models.Product;
using ShoppingCart.Application.Common.Models.User;

namespace ShoppingCart.Application.Application.Queries.Order.GetOrderDetails
{
    public class GetOrderDetailsQueryHandler : IRequestHandler<GetOrderDetailsQuery, OrderDetailsResponse>
    {
        private readonly IOrderDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IProductService _productService;
        private readonly IUserService _userService;
        public GetOrderDetailsQueryHandler(
            IOrderDbContext dbContext,
            IMapper mapper,
            IProductService productService,
            IUserService userService)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException("OrderDbContext");
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _productService = productService;
            _userService = userService;
        }

        public async Task<OrderDetailsResponse> Handle(GetOrderDetailsQuery request, CancellationToken cancellationToken)
        {
            var data =
              await _dbContext.Orders
              .Include(x => x.ProductRanges)
              .ProjectTo<OrderDetailsDto>(_mapper.ConfigurationProvider)
              .FirstOrDefaultAsync(x => x.OrderId == request.OrderId);

            var ProductIds = data!.ProductRanges.Select(x => x.ProductId).Distinct().ToList();

            var productDetails = _productService
                .GetProductsAsync(QueryBuilder.ConvertToIdString(ProductIds, nameof(ProductDto.ProductId))
                    + QueryBuilder.GeneratePaginationParam(ProductIds.Count()));

            var user = _userService.GetUserAsync(data.User.UserId);

            await Task.WhenAll(productDetails, user);

            foreach (var product in data.ProductRanges)
                AddProductDetails(product, productDetails.Result);

            AddUserDetails(data.User, user.Result);

            return new OrderDetailsResponse(data!);
        }

        private ProductRangeDetailsDto AddProductDetails(
            ProductRangeDetailsDto product,
             IEnumerable<ProductDto> productDetails)
        {
            var pr = productDetails.First(x => x.ProductId == product.ProductId);
            product.Description = pr.Description;
            product.Name = pr.Name;
            product.Available = pr.Available;
            return product;
        }

        private void AddUserDetails(UserOrderDetailsDto user, UserDto userDetails)
        {
            user.UserName = userDetails.UserName;
            user.Email = userDetails.Email;
        }
    }
}
