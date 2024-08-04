using SharedModels;
using ShoppingCart.Application.Application.Queries.Order.GetOrderDetails;
using ShoppingCart.Application.Application.Queries.ProductRange.GetProductRangeDetails;

namespace ShoppingCart.API.Models.Order
{
    public class OrderPaidDto : OrderDetails, IMapWith<OrderDetailsDto>
    {
        public new IEnumerable<ProductRangeDetailsDto> ProductRanges { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<OrderDetailsDto, OrderPaidDto>()
                .ForPath(x => x.UserName,
                    opt => opt.MapFrom(y => y.User.UserName))
                .ForPath(x => x.Email,
                    opt => opt.MapFrom(y => y.User.Email))
                .ForPath(x => x.ProductRanges,
                    opt => opt.MapFrom(y => y.ProductRanges))
                .ForPath(x => x.OrderTime,
                    opt => opt.MapFrom(y => y.OrderTime))
                .ForPath(x => x.Price,
                    opt => opt.MapFrom(y => y.Price));
        }
    }

    public class OrderProductRangeDetails : ProductRangeDetails
    {
        public void Mapping(Profile profile)
        {
            profile.CreateMap<ProductRangeDetailsDto, ProductRangeDetails>()
                .ForPath(x => x.Name,
                    opt => opt.MapFrom(y => y.Name))
                .ForPath(x => x.Description,
                    opt => opt.MapFrom(y => y.Description))
                .ForPath(x => x.Count,
                    opt => opt.MapFrom(y => y.Count));
        }
    }
}
