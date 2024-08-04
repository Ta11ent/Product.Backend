using ProductCatalog.Application.Application.Commands.SubCategory.UpdateSubCategory;

namespace ProductCatalog.API.Models.SubCategory
{
    public class UpdateSubCategoryDto :  IMapWith<UpdateSubCategoryCommand>
    {
        public Guid CategoryId { get; set; }
        public Guid SubCategoryId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public void Mapping(Profile profile)
        {
            profile.CreateMap<UpdateSubCategoryDto, UpdateSubCategoryCommand>()
                .ForMember(x => x.CategoryId,
                    opt => opt.MapFrom(y => y.CategoryId))
                .ForMember(x => x.SubCategoryId,
                    opt => opt.MapFrom(y => y.SubCategoryId))
                .ForMember(x => x.Name,
                    opt => opt.MapFrom(y => y.Name))
                .ForMember(x => x.Description,
                    opt => opt.MapFrom(y => y.Description));
        }
    }
}
