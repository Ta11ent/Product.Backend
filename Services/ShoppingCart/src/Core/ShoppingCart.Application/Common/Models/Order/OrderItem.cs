using AutoMapper;
using ShoppingCart.Application.Common.Mapping;

namespace ShoppingCart.Application.Common.Models.Order
{
    public class OrderItemDto : IMapWith<Domain.OrderItem>
    {
        public Guid OrderItemId { get; set; }
        public Guid ProductId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Manufacturer { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string Ccy { get; set; } = string.Empty;
        public int Count { get; set; }


        public void Mapping(Profile profile)
        {
            profile.CreateMap<Domain.OrderItem, OrderItemDto>()
                .ForMember(x => x.OrderItemId,
                    opt => opt.MapFrom(y => y.OrderItemId))
                .ForMember(x => x.ProductId,
                    opt => opt.MapFrom(y => y.ProductId))
                .ForMember(x => x.Count,
                    opt => opt.MapFrom(y => y.Count));
        }
    }
}
