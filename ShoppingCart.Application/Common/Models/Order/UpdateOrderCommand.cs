namespace ShoppingCart.Application.Common.Models.Order
{
    public class UpdateOrderCommand
    {
        public Guid OrderId { get; set; }
        public bool IsPaid { get; set; }
    }
}
