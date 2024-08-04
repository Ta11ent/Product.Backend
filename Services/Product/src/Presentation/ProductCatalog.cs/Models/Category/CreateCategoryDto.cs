using ProductCatalog.Application.Application.Commands.Category.CreateCategory;

namespace ProductCatalog.cs.Models.Category;

public class CreateCategoryDto : IMapWith<CreateCategoryCommand>
{
    public string Name { get; set; }
    public string Description { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<CreateCategoryDto, CreateCategoryCommand>()
            .ForMember(x => x.Name,
                opt => opt.MapFrom(y => y.Name))
            .ForMember(x => x.Description,
                opt => opt.MapFrom(y => y.Description));
    }
}
        
