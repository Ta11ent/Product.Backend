using ProductCatalog.Application.Application.Queries.Product.GetProductList;

namespace ProductCatalog.APIcs.Models.Product
{
    public class GetProductListDto : IMapWith<GetProductListQuery>
    {
        public int? Page { get; set; }
        public int? PageSize { get; set; }
        public Guid? CategoryId { get; set; }
        public Guid[]? ProductId { get; set; }
        public bool? Available { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<GetProductListDto, GetProductListQuery>()
                .ForMember(x => x.Page,
                    opt => opt.MapFrom(y => y.Page))
                .ForMember(x => x.PageSize,
                    opt => opt.MapFrom(y => y.PageSize))
                .ForMember(x => x.CategoryId,
                    opt => opt.MapFrom(y => y.CategoryId))
                .ForMember(x => x.ProductIds,
                    opt => opt.MapFrom(y => y.ProductId))
                .ForMember(x => x.Available,
                    opt => opt.MapFrom(y => y.Available));
        }
    }
}
