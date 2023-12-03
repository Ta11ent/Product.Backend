using AutoMapper;
using ShoppingCart.Application.Common.Mapping;
using ShoppingCart.Application.Common.Models.ProductRange;

namespace ShoppingCart.Application.Common.Models.Order
{
    public class OrderListDto : IMapWith<Domain.Order>
    {
        public Guid OrderId { get; set; }
        public Guid UserId { get; set; } // need to add more information about user
        public IEnumerable<ProductRangeDetailsDto> ProductRanges { get; set; }
        public DateTime? OrderTime { get; set; }
        public decimal Price { get; set; }
        public bool IsPaid { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Domain.Order, OrderDetailsDto>()
                .ForMember(x => x.OrderId,
                    opt => opt.MapFrom(y => y.OrderId))
                 .ForMember(x => x.UserId,
                    opt => opt.MapFrom(y => y.UserId))
                 .ForMember(x => x.ProductRanges,
                    opt => opt.MapFrom(y => y.ProductRanges))
                  .ForMember(x => x.OrderTime,
                    opt => opt.MapFrom(y => y.OrderTime))
                  .ForMember(x => x.IsPaid,
                    opt => opt.MapFrom(y => y.IsPaid));
        }
    }
}
