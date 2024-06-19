using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ShoppingCart.Application.Application.Queries.ProductRange.GetProductRangeDetails;
using ShoppingCart.Application.Common.Abstractions;
using ShoppingCart.Application.Common.Helpers;
using ShoppingCart.Application.Common.Models.Product;
using ShoppingCart.Application.Common.Models.User;
using ShoppingCart.Application.Common.Predicate;
using ShoppingCart.Application.Queries.Order.GetOrderList;

namespace ShoppingCart.Application.Application.Queries.Order.GetOrderList
{
    public class GetOrderListQueryHandler : IRequestHandler<GetOrderListQuery, OrderListResponse>
    {
        private readonly IOrderDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IProductService _productService;
        public GetOrderListQueryHandler(
            IOrderDbContext dbContext, 
            IMapper mapper,
            IProductService productService)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException("OrderDbContext");
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _productService = productService;
        }
        public async Task<OrderListResponse> Handle(GetOrderListQuery request, CancellationToken cancellationToken)
        {
            var predicate = PredicateBuilder.True<Domain.Order>();
            var data =
                   await _dbContext.Orders
                   .Include(x => x.ProductRanges)
                   .Where(predicate
                    .And(x => x.OrderTime >= request.DateFrom, request.DateFrom)
                    .And(x => x.OrderTime <= request.DateTo, request.DateTo)
                    .And(x => x.UserId == request.UserId, request.UserId)
                    .And(x => x.IsPaid == request.IsPaid, request.IsPaid))
                   .Skip((request.Page - 1) * request.PageSize)
                   .Take(request.PageSize)
                   .ProjectTo<OrderListDto>(_mapper.ConfigurationProvider)
                   .ToListAsync();


            var ProductIds = data.SelectMany(x => x.ProductRanges.Select(x => x.ProductId)).Distinct().ToList();

            var productDetails = await _productService
                .GetProductsAsync(QueryBuilder.ConvertToIdString(ProductIds, nameof(ProductDto.ProductId))
                    + QueryBuilder.GeneratePaginationParam(ProductIds.Count()));

            foreach (var item in data)
                foreach (var product in item.ProductRanges)
                    AddProductDetails(product, ref productDetails);

            return new OrderListResponse(data, request);
        }

        private ProductRangeDetailsDto AddProductDetails(
           ProductRangeDetailsDto product,
           ref IEnumerable<ProductDto> productDetails)
        {
            var pr = productDetails.First(x => x.ProductId == product.ProductId);
            product.Description = pr.Description;
            product.Name = pr.Name;
            product.Available = pr.Available;
            return product;
        }

        private void AddUserDetails(UserOrderDetailsDto user, ref UserDto userDetails)
        {
            user.UserName = userDetails.UserName;
            user.Email = userDetails.Email;
        }
    }
}
