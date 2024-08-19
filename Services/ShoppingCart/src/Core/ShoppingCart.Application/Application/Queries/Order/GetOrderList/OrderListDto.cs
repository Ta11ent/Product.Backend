using AutoMapper;
using ShoppingCart.Application.Common.Mapping;
using ShoppingCart.Application.Common.Models.Order;

namespace ShoppingCart.Application.Queries.Order.GetOrderList
{
    public class OrderListDto : IMapWith<Domain.Order>
    {
        public Guid OrderId { get; set; }
        public Guid UserId { get; set; }
        public string Status { get; set; } = string.Empty;
        public List<OrderItemDto> OrderItems { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Domain.Order, OrderListDto>()
                .ForMember(x => x.OrderId,
                    opt => opt.MapFrom(y => y.OrderId))
                 .ForMember(x => x.UserId,
                    opt => opt.MapFrom(y => y.UserId))
                 .ForMember(x => x.OrderItems,
                    opt => opt.MapFrom(y => y.OrderItems))
                 .ForMember(x => x.Status,
                    opt => opt.MapFrom(y => y.Statuses.FirstOrDefault()!.TypeOfStatus));
        }
    }
}
