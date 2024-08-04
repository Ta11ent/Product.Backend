using ProductCatalog.Application.Application.Queries.Product.GetProductDetails;

namespace ProductCatalog.API.Models.Product
{
    public class GetProductDto : ProductPath, IMapWith<GetProductDetailsQuery>
    {
        public Guid Id { get; set; }
        public string? Ccy {  get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<GetProductDto, GetProductDetailsQuery>()
                .ForMember(x => x.CategoryId,
                    opt => opt.MapFrom(y => y.CategoryId))
                .ForMember(x => x.SubCategoryId,
                    opt => opt.MapFrom(y => y.SubCategoryId))
                .ForMember(x => x.ProductId,
                    opt => opt.MapFrom(y => y.Id))
                .ForMember(x => x.CurrencyCode,
                    opt => opt.MapFrom(y => y.Ccy));
        }
    }
}
