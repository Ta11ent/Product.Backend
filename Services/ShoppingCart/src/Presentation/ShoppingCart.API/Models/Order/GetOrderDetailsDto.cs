using ShoppingCart.Application.Application.Queries.Order.GetOrderDetails;

namespace ShoppingCart.API.Models.Order
{
    public class GetOrderDetailsDto : IMapWith<GetOrderDetailsQuery>
    {
        public Guid? OrderId {  get; set; }
        public string? Ccy { get; set; } = string.Empty;
        public void Mapping(Profile profile)
        {
            profile.CreateMap<GetOrderDetailsDto, GetOrderDetailsQuery>()
                .ForMember(x => x.Ccy,
                    opt => opt.MapFrom(y => y.Ccy))
                .ForMember(x => x.OrderId,
                    opt => opt.MapFrom(y => y.OrderId));
        }
    }
}
