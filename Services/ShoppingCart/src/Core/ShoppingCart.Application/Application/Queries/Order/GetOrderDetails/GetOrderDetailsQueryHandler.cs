using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ShoppingCart.Application.Common.Abstractions;
using ShoppingCart.Application.Common.Helpers;
using ShoppingCart.Application.Common.Models.Order;
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
              .Include(x => x.Statuses)
              .Include(x => x.OrderItems)
              .ProjectTo<OrderDetailsDto>(_mapper.ConfigurationProvider)
              .FirstOrDefaultAsync(x => x.OrderId == request.OrderId);

            if(data is not null) { 
                var ProductIds = data!.OrderItems.Select(x => x.ProductId).Distinct().ToList();

                var productDetails = _productService
                    .GetProductsAsync(new ParamQueryBuilder()
                    .AddIdAsParams(nameof(ProductDto.ProductId), ProductIds)
                    .AddPaginationAsParams(ProductIds.Count())
                    .AddCustomParam(nameof(request.Ccy), request.Ccy)
                    .GetParamAsString());

                var user = _userService.GetUserAsync(data.User.UserId);

                await Task.WhenAll(productDetails, user);

                foreach (var product in data.OrderItems)
                    AddProductDetails(product, productDetails.Result.FirstOrDefault(x => x.ProductId == product.ProductId)!);

                AddUserDetails(data.User, user.Result);
            }

            return new OrderDetailsResponse(data!);
        }

        private void AddProductDetails(
             OrderItemDto product,
             ProductDto productDetails)
        {
            product.Name = productDetails.Name;
            product.Description = productDetails.Description;
            product.Manufacturer = productDetails.Manufacturer;
            product.Price = productDetails.Price;
            product.Ccy = productDetails.Ccy;
        }

        private void AddUserDetails(UserOrderDetailsDto user, UserDto userDetails)
        {
            user.UserName = userDetails.UserName;
            user.Email = userDetails.Email;
        }
    }
}
