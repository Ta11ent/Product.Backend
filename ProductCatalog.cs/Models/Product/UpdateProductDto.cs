using ProductCatalog.API.Models.Product;
using ProductCatalog.Application.Application.Commands.Product.UpdateProduct;

namespace ProductCatalog.cs.Models.Product
{
    public class UpdateProductDto : ProductPath, IMapWith<UpdateProductCommand>
    {
        public Guid ProductId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public bool? Available { get; set; }
        public decimal Price { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<UpdateProductDto, UpdateProductCommand>()
                .ForMember(x => x.ProductId,
                    opt => opt.MapFrom(y => y.ProductId))
                .ForMember(x => x.Name,
                    opt => opt.MapFrom(y => y.Name))
                .ForMember(x => x.Description,
                    opt => opt.MapFrom(y => y.Description))
                .ForMember(x => x.CategoryId,
                    opt => opt.MapFrom(y => y.CategoryId))
                 .ForMember(x => x.SubCategoryId,
                    opt => opt.MapFrom(y => y.SubCategoryId))
                .ForMember(x => x.Available,
                    opt => opt.MapFrom(y => y.Available));
        }
    }
}
