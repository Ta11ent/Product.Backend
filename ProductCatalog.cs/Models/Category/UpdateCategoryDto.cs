using ProductCatalog.Application.Application.Commands.Category.UpdateCategory;

namespace ProductCatalog.cs.Models.Category
{
    public class UpdateCategoryDto : IMapWith<UpdateCategoryCommand>
    {
        public Guid CategoryId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<UpdateCategoryDto, UpdateCategoryCommand>()
                .ForMember(x => x.CategoryId,
                    opt => opt.MapFrom(y => y.CategoryId))
                .ForMember(x => x.Name,
                    opt => opt.MapFrom(y => y.Name))
                .ForMember(x => x.Description,
                    opt => opt.MapFrom(y => y.Description));
        }
    }
}
