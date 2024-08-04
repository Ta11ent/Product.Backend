using AutoMapper;
using ShoppingCart.Application.Application.Queries.ProductRange.GetProductRangeDetails;
using ShoppingCart.Application.Common.Mapping;
using ShoppingCart.Application.Common.Models.User;

namespace ShoppingCart.Application.Application.Queries.Order.GetOrderDetails
{
    public class OrderDetailsDto : IMapWith<Domain.Order>
    {
        public Guid OrderId { get; set; }
        private string UserId
        {
            set { User = new UserOrderDetailsDto(value); }
            get { return User.UserId; }
        }
        public UserOrderDetailsDto User { get; set; }
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
