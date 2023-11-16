using ProductCatalog.Application.Application.Queries.Product.GetProductList;

namespace ProductCatalog.APIcs.Models.Product
{
    public class GetProductListDto : IMapWith<GetProductListQuery>
    {
        public int? Page { get; set; }
        public int? PageSize { get; set; }
        public Guid? CategoryId { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<GetProductListDto, GetProductListQuery>()
                .ForMember(x => x.Page,
                    opt => opt.MapFrom(y => y.Page))
                .ForMember(x => x.PageSize,
                    opt => opt.MapFrom(y => y.PageSize));
        }
    }
}
