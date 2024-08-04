using ProductCatalog.Application.Application.Commands.SubCategory.CreateSubCategory;

namespace ProductCatalog.API.Models.SubCategory
{
    public class CreateSubCategoryDto : IMapWith<CreateSubCategoryCommand>
    {
        public Guid CategoryId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateSubCategoryDto, CreateSubCategoryCommand>()
                .ForMember(x => x.CategoryId,
                    opt => opt.MapFrom(y => y.CategoryId))
                .ForMember(x => x.Name,
                    opt => opt.MapFrom(y => y.Name))
                .ForMember(x => x.Description,
                    opt => opt.MapFrom(y => y.Description));
        }
    }
}
