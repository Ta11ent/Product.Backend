using AutoMapper;
using ShoppingCart.Application.Common.Models.ProductRange;

namespace ShoppingCart.API.Models.ProductRange
{
    public class CreateProductRangeDto : IMapWith<CreateProductRangeCommand>
    {
        public Guid ProductId { get; set; }
        public int Count { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateProductRangeDto, CreateProductRangeCommand>()
                .ForMember(x => x.ProductId,
                    opt => opt.MapFrom(y => y.ProductId))
                .ForMember(x => x.Count,
                    opt => opt.MapFrom(y => y.Count));
        }
    }
}
