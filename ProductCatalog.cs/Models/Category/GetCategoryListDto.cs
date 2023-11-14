using ProductCatalog.Application.Application.Queries.Category.GetCategoryList;

namespace ProductCatalog.APIcs.Models.Category
{
    public class GetCategoryListDto : IMapWith<GetCategoryListQuery>
    {
        public int Page { get; set; }
        public int PageSize { get; set; }       
        public void Mapping(Profile profile) 
        {
            profile.CreateMap<GetCategoryListDto, GetCategoryListQuery>()
                .ForMember(x => x.Page,
                    opt => opt.MapFrom(y => y.Page))
                .ForMember(x => x.PageSize,
                    opt => opt.MapFrom(y => y.PageSize));
        }
    }
}
