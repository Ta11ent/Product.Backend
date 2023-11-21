using AutoMapper;
using ShoppingCart.Application.Common.Mapping;

namespace ShoppingCart.Application.Common.Models.Order
{
    public class OrderDetailsDto : IMapWith<Domain.Order>
    {
        public Guid OrderId { get; set; }
        public Guid UserId { get; set; }
        public IEnumerable<OrderProductRange> ProductRanges { get; set; }
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

    public class OrderProductRange : IMapWith<Domain.ProductRange>
    {
        public Guid ProductRangeId { get; set; }
        public Guid ProductId { get; set; }
        public int Count { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Domain.ProductRange, OrderProductRange>()
                .ForMember(x => x.ProductRangeId,
                    opt => opt.MapFrom(y => y.ProductRangeId))
                .ForMember(x => x.ProductId,
                    opt => opt.MapFrom(y => y.ProductId))
                .ForMember(x => x.Count,
                    opt => opt.MapFrom(y => y.Count));
        }
    }
}
