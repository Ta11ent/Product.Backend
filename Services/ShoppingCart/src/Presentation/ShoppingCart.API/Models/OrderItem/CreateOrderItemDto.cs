using ShoppingCart.Application.Application.Commands.ProductRange.CreateProductRange;

namespace ShoppingCart.API.Models.ProductRange
{
    public class CreateOrderItemDto : IMapWith<CreateOrderItemCommand>
    {
        public Guid ProductSaleId { get; set; }
        public Guid UserId { get; set; }
        public int Count { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateOrderItemDto, CreateOrderItemCommand>()
                .ForMember(x => x.ProductId,
                    opt => opt.MapFrom(y => y.ProductSaleId))
                .ForMember(x => x.UserId,
                    opt => opt.MapFrom(y => y.UserId))
                .ForMember(x => x.Count,
                    opt => opt.MapFrom(y => y.Count));
        }
    }
}
