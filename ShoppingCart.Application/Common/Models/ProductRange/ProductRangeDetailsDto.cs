using AutoMapper;
using ShoppingCart.Application.Common.Mapping;

namespace ShoppingCart.Application.Common.Models.ProductRange
{
    public class ProductRangeDetailsDto : IMapWith<Domain.ProductRange>
    {
        public Guid ProductRangeId { get; set; }
        public Guid ProductId { get; set; }
        public string Name { get; set; } // need to understand how I can map the data from another microservice
        public string Description { get; set; } // the same
        public int Count { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Domain.ProductRange, ProductRangeDetailsDto>()
                .ForMember(x => x.ProductRangeId,
                    opt => opt.MapFrom(y => y.ProductRangeId))
                .ForMember(x => x.ProductId,
                    opt => opt.MapFrom(y => y.ProductId))
                .ForMember(x => x.Count,
                    opt => opt.MapFrom(y => y.Count));
        }
    }
}
