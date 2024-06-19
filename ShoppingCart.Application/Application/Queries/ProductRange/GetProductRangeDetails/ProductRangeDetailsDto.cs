using AutoMapper;
using ShoppingCart.Application.Common.Mapping;

namespace ShoppingCart.Application.Application.Queries.ProductRange.GetProductRangeDetails
{
    public class ProductRangeDetailsDto : IMapWith<Domain.ProductRange>
    {
        public Guid ProductRangeId { get; set; }
        public Guid ProductId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public bool Available { get; set; }
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
