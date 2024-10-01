using ProductCatalog.API.Models.Product;
using ProductCatalog.Application.Application.Commands.Product.CreateProduct;

namespace ProductCatalog.cs.Models.Product
{
    public class CreateProductDto : ProductPath, IMapWith<CreateProductCommand>
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public Guid CurrencyId { get; set; }
        public Guid ManufacturerId { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateProductDto, CreateProductCommand>()
                .ForMember(x => x.CategoryId,
                    opt => opt.MapFrom(y => y.CategoryId))
                .ForMember(x => x.SubCategoryId,
                    opt => opt.MapFrom(y => y.SubCategoryId))
                .ForMember(x => x.Name,
                    opt => opt.MapFrom(y => y.Name))
                .ForMember(x => x.Description,
                    opt => opt.MapFrom(y => y.Description))
                .ForMember(x => x.Price,
                    opt => opt.MapFrom(y => y.Price))
                .ForMember(x => x.CurrencyId,
                    opt => opt.MapFrom(y => y.CurrencyId))
                .ForMember(x => x.ManufacturerId,
                    opt => opt.MapFrom(y => y.ManufacturerId));
        }
    }
}
