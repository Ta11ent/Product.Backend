using SharedModels;
using ShoppingCart.Application.Application.Queries.Order.GetOrderDetails;
using ShoppingCart.Application.Common.Models.Order;

namespace ShoppingCart.API.Models.Order
{
    public class OrderPaidDto : OrderDetails, IMapWith<OrderDetailsDto>
    {
        public new IEnumerable<OrderItemDto> OrderItems { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<OrderDetailsDto, OrderPaidDto>()
                .ForPath(x => x.UserName,
                    opt => opt.MapFrom(y => y.User.UserName))
                .ForPath(x => x.Email,
                    opt => opt.MapFrom(y => y.User.Email))
                .ForPath(x => x.OrderItems,
                    opt => opt.MapFrom(y => y.OrderItems))
                .ForPath(x => x.Price,
                    opt => opt.MapFrom(y => y.OrderItems.Sum(x => x.Price)))
                .ForPath(x => x.Status,
                    opt => opt.MapFrom(y => y.StatusHistory.FirstOrDefault()));
        }
    }

    public class OrderProductRangeDetails : OrderItemDetails
    {
        public void Mapping(Profile profile)
        {
            profile.CreateMap<OrderItemDto, OrderItemDetails>()
                .ForPath(x => x.Name,
                    opt => opt.MapFrom(y => y.Name))
                .ForPath(x => x.Description,
                    opt => opt.MapFrom(y => y.Description))
                .ForPath(x => x.Count,
                    opt => opt.MapFrom(y => y.Count));
        }
    }
}
