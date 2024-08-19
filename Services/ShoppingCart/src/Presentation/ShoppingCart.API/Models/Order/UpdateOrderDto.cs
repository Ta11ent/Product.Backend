using ShoppingCart.Application.Application.Commands.Order.UpdateOrder;

namespace ShoppingCart.API.Models.Order
{
    public class UpdateOrderDto : IMapWith<UpdateOrderCommand>
    {
        public Guid OrderId { get; set; }
        public string Status { get; set; } = string.Empty;
        public void Mapping(Profile profile)
        {
            profile.CreateMap<UpdateOrderDto, UpdateOrderCommand>()
                .ForMember(x => x.OrderId,
                    opt => opt.MapFrom(y => y.OrderId))
                .ForMember(x => x.Status,
                    opt => opt.MapFrom(y => y.Status));
        }
    }
}
