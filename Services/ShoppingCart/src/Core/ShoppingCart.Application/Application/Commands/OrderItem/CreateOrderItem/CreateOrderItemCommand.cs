﻿using MediatR;

namespace ShoppingCart.Application.Application.Commands.ProductRange.CreateProductRange
{
    public class CreateOrderItemCommand : IRequest<Guid>
    {
        public Guid ProductId { get; set; }
        public Guid UserId { get; set; }
        public int Count { get; set; }
    }
}
