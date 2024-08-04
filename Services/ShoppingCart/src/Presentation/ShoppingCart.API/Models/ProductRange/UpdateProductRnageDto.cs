using AutoMapper;
using ShoppingCart.Application.Application.Commands.ProductRange.UpdateProductRange;

namespace ShoppingCart.API.Models.ProductRange
{
    public class UpdateProductRnageDto : IMapWith<UpdateProductRangeCommand>
    {
        public Guid ProductRangeId { get; set; }
        public int Count { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<UpdateProductRnageDto, UpdateProductRangeCommand>()
                .ForMember(x => x.ProductRangeId,
                    opt => opt.MapFrom(y => y.ProductRangeId))
                .ForMember(x => x.Count,
                    opt => opt.MapFrom(y => y.Count));
        }
    }
}
