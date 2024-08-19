using ShoppingCart.Application.Application.Commands.ProductRange.UpdateProductRange;

namespace ShoppingCart.API.Models.ProductRange
{
    public class UpdateOrderItemDto : IMapWith<UpdateOrderItemCommand>
    {
        public Guid OrderItemId { get; set; }
        public int Count { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<UpdateOrderItemDto, UpdateOrderItemCommand>()
                .ForMember(x => x.OrderItemId,
                    opt => opt.MapFrom(y => y.OrderItemId))
                .ForMember(x => x.Count,
                    opt => opt.MapFrom(y => y.Count));
        }
    }
}
