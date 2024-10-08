﻿namespace ShoppingCart.Domain
{
    public class OrderItem
    {
        public Guid OrderItemId { get; set; }
        public Guid ProductId { get; set; }
        public int Count { get; set; }
        public Guid OrderId { get; set; }
        public Order Order { get; set; }
    }
}
