using ProductCatalog.Application.Application.Commands.Product.CreateProduct;

namespace ProductCatalog.cs.Models.Product
{
    public class CreateProductDto : IMapWith<CreateProductCommand>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid CategoryId { get; set; }
        public decimal Price { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateProductDto, CreateProductCommand>()
                .ForMember(x => x.Name,
                    opt => opt.MapFrom(y => y.Name))
                .ForMember(x => x.Description,
                    opt => opt.MapFrom(y => y.Description))
                .ForMember(x => x.CategoryId,
                    opt => opt.MapFrom(y => y.CategoryId));
        }
    }
}
