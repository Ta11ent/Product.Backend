using ProductCatalog.Application.Application.Queries.SubCategory.GetSubCategoryList;

namespace ProductCatalog.API.Models.SubCategory
{
    public class GetSubCategoryListDto : SubCategoryPath, IMapWith<GetSubCategoryListQuery>
    {
        public int? Page { get; set; }
        public int? PageSize { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<GetSubCategoryListDto, GetSubCategoryListQuery>()
                .ForMember(x => x.CategoryId,
                    opt => opt.MapFrom(y => y.CategoryId))
                .ForMember(x => x.Page,
                    opt => opt.MapFrom(y => y.Page))
                .ForMember(x => x.PageSize,
                    opt => opt.MapFrom(y => y.PageSize));
        }
    }
}
