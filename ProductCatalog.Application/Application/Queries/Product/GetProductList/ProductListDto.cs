using AutoMapper;
using ProductCatalog.Application.Common.Mapping;

namespace ProductCatalog.Application.Application.Queries.Product.GetProductList
{
    public class ProductListDto : IMapWith<Domain.Product>
    {
        public Guid ProductId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public bool Available { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<Domain.Product, ProductListDto>()
                .ForMember(x => x.ProductId,
                    opt => opt.MapFrom(y => y.ProductId))
                .ForMember(x => x.Name,
                    opt => opt.MapFrom(y => y.Name))
                .ForMember(x => x.Description,
                    opt => opt.MapFrom(y => y.Description))
                .ForMember(x => x.Available,
                    opt => opt.MapFrom(y => y.Available))
                .ForMember(x => x.Price,
                    opt => opt.MapFrom(y => y.Costs.First().Price));
        }
    }
}
