using AutoMapper;
using ShoppingCart.Application.Application.Queries.ProductRange.GetProductRangeDetails;
using ShoppingCart.Application.Common.Mapping;

namespace ShoppingCart.Application.Queries.Order.GetOrderList
{
    public class OrderListDto : IMapWith<Domain.Order>
    {
        public Guid OrderId { get; set; }
        public Guid UserId { get; set; }
        public List<ProductRangeDetailsDto> ProductRanges { get; set; }
        public DateTime? OrderTime { get; set; }
        public decimal Price { get; set; }
        public bool IsPaid { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Domain.Order, OrderListDto>()
                .ForMember(x => x.OrderId,
                    opt => opt.MapFrom(y => y.OrderId))
                 .ForMember(x => x.UserId,
                    opt => opt.MapFrom(y => y.UserId))
                 .ForMember(x => x.ProductRanges,
                    opt => opt.MapFrom(y => y.ProductRanges))
                  .ForMember(x => x.OrderTime,
                    opt => opt.MapFrom(y => y.OrderTime))
                  .ForMember(x => x.Price,
                    opt => opt.MapFrom(y => y.Price))
                  .ForMember(x => x.IsPaid,
                    opt => opt.MapFrom(y => y.IsPaid));
        }
    }
}
