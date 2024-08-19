using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ShoppingCart.Application.Common.Abstractions;
using ShoppingCart.Application.Common.Helpers;
using ShoppingCart.Application.Common.Models.Order;
using ShoppingCart.Application.Common.Models.Product;
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
                   .Include(x => x.OrderItems)
                   .Include(x => x.Statuses.OrderByDescending(x => x.StatusDate).Take(1))
                   .Where(predicate
                    .And(x => x.Statuses.LastOrDefault()!.StatusDate >= request.DateFrom, request.DateFrom)
                    .And(x => x.Statuses.LastOrDefault()!.StatusDate <= request.DateTo, request.DateTo)
                    .And(x => x.UserId == request.UserId, request.UserId)
                    .And(x => x.Statuses.LastOrDefault()!.TypeOfStatus == request.Status, request.Status))
                   .Skip((request.Page - 1) * request.PageSize)
                   .Take(request.PageSize)
                   .ProjectTo<OrderListDto>(_mapper.ConfigurationProvider)
                   .ToListAsync();


            var ProductIds = data.SelectMany(x => x.OrderItems.Select(x => x.ProductId)).Distinct().ToList();

            var productDetails = await _productService
                .GetProductsAsync(QueryBuilder.ConvertToIdString(ProductIds, nameof(ProductDto.ProductId))
                    + QueryBuilder.GeneratePaginationParam(ProductIds.Count()));

            foreach (var item in data)
                foreach (var product in item.OrderItems)
                    AddProductDetails(product, productDetails.FirstOrDefault(x => x.ProductId == product.ProductId)!);

            return new OrderListResponse(data, request);
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
    }
}
