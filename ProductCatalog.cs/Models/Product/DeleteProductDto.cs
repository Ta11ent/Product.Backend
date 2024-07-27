using ProductCatalog.Application.Application.Commands.Product.DeleteProduct;

namespace ProductCatalog.API.Models.Product
{
    public class DeleteProductDto : ProductPath, IMapWith<DeleteProductCommand>
    {
        public Guid Id { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<DeleteProductDto, DeleteProductCommand>()
                .ForMember(x => x.CategoryId,
                    opt => opt.MapFrom(y => y.CategoryId))
                .ForMember(x => x.SubCategoryId,
                    opt => opt.MapFrom(y => y.SubCategoryId))
                .ForMember(x => x.ProductId,
                    opt => opt.MapFrom(y => y.Id));
        }
    }
}
