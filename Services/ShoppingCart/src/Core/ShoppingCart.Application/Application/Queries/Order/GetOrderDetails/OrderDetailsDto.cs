using AutoMapper;
using ShoppingCart.Application.Common.Mapping;
using ShoppingCart.Application.Common.Models.Order;
using ShoppingCart.Application.Common.Models.Status;
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
        public IEnumerable<StatusDto> StatusHistory { get; set; }
        public IEnumerable<OrderItemDto> OrderItems { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Domain.Order, OrderDetailsDto>()
               .ForMember(x => x.OrderId,
                   opt => opt.MapFrom(y => y.OrderId))
                .ForMember(x => x.UserId,
                   opt => opt.MapFrom(y => y.UserId))
                .ForMember(x => x.OrderItems,
                   opt => opt.MapFrom(y => y.OrderItems))
                .ForMember(x => x.StatusHistory,
                   opt => opt.MapFrom(y => y.Statuses));
        }
    }
}
