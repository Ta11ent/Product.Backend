﻿using AutoMapper;
using ShoppingCart.Application.Common.Models.Order;

namespace ShoppingCart.API.Models.Order
{
    public class UpdateOrderDto : IMapWith<UpdateOrderCommand>
    {
        public Guid? OrderId { get; set; }
        public bool IsPaid { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<UpdateOrderDto, UpdateOrderCommand>()
                .ForMember(x => x.OrderId,
                    opt => opt.MapFrom(y => y.OrderId))
                .ForMember(x => x.IsPaid,
                    opt => opt.MapFrom(y => y.IsPaid));
        }
    }
}
