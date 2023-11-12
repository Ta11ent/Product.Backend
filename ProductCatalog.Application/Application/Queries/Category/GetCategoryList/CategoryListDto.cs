using AutoMapper;
using ProductCatalog.Application.Common.Mapping;

namespace ProductCatalog.Application.Application.Queries.Category.GetCategoryList
{
    public class CategoryListDto : IMapWith<Domain.Category>
    {
        public Guid CategoryId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<Domain.Category, CategoryListDto>()
                .ForMember(x => x.CategoryId,
                    opt => opt.MapFrom(y => y.CategoryId))
                 .ForMember(x => x.Name,
                    opt => opt.MapFrom(y => y.Name))
                  .ForMember(x => x.Description,
                    opt => opt.MapFrom(y => y.Description));
        }
    }
}
