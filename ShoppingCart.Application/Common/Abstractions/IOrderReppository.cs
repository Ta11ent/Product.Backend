using ShoppingCart.Application.Common.Models.Order;

namespace ShoppingCart.Application.Common.Abstractions
{
    public interface IOrderReppository : IDisposable
    {
        Task<Guid> CreateOrderAsync(Guid userId);
        Task UpdateOrderAsync(UpdateOrderCommand command);
        Task DeleteOrderAsync(Guid OrderId);
        Task<OrderDetailsResponse> GetOrderDetailsAsync(Guid OrderId);
        Task<OrderPageResponse> GetOrderListAsync(OrderListQuery query);
    }
}
