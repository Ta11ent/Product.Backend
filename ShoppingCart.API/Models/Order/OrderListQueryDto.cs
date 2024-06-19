using ShoppingCart.Application.Queries.Order.GetOrderList;

namespace ShoppingCart.API.Models.Order
{
    public class OrderListQueryDto : IMapWith<GetOrderListQuery>
    {
        public int? Page { get; set; }
        public int? PageSize { get; set;}
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        public Guid? UserId { get; set; }
        public bool? IsPaid { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<OrderListQueryDto, GetOrderListQuery>()
                .ForMember(x => x.Page,
                    opt => opt.MapFrom(y => y.Page))
                .ForMember(x => x.PageSize,
                    opt => opt.MapFrom(y => y.PageSize))
                .ForMember(x => x.DateFrom,
                    opt => opt.MapFrom(y => y.DateFrom))
                .ForMember(x => x.DateTo,
                    opt => opt.MapFrom(y => y.DateTo))
                .ForMember(x => x.UserId,
                    opt => opt.MapFrom(y => y.UserId))
                .ForMember(x => x.IsPaid,
                    opt => opt.MapFrom(y => y.IsPaid));
        }
    }
}
