using AutoMapper;
using ProductCatalog.Application.Application.Queries.Category.GetCategoryDetails;
using ProductCatalog.Application.Common.Mapping;

namespace ProductCatalog.Application.Application.Queries.Product.GetProductDetails
{
    public class ProductDetailsDto : IMapWith<Domain.Product>
    {
        public Guid ProductId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool Available { get; set; }
        public CategoryDetailsDto CategoryDetails { get; set; }
        public void Mapping (Profile profile)
        {
            profile.CreateMap<Domain.Product, ProductDetailsDto>()
                .ForMember(x => x.ProductId,
                    opt => opt.MapFrom(y => y.ProductId))
                .ForMember(x => x.Name,
                    opt => opt.MapFrom(y => y.Name))
                .ForMember(x => x.Description,
                    opt => opt.MapFrom(y => y.Description))
                .ForMember(x => x.CategoryDetails,
                    opt => opt.MapFrom(y => y.Category))
                .ForMember(x => x.Available,
                    opt => opt.MapFrom(y => y.Available));

        }
    }
}
