using AutoMapper;
using ProductCatalog.Application.Application.Queries.Category.GetCategoryDetails;
using ProductCatalog.Application.Common.Mapping;

namespace ProductCatalog.Application.Application.Queries.SubCategory.GetSubCategoryDetails
{
    public class SubCategoryDetailsDto : IMapWith<Domain.SubCategory>
    {
        public Guid SubCategoryId { get; set; }
        public Guid CategoryId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public CategoryDetailsDto Category { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<Domain.SubCategory, SubCategoryDetailsDto>()
                .ForMember(x => x.SubCategoryId,
                    opt => opt.MapFrom(y => y.SubCategoryId))
                .ForMember(x => x.CategoryId,
                    opt => opt.MapFrom(y => y.CategoryId))
                .ForMember(x => x.Name,
                    opt => opt.MapFrom(y => y.Name))
                .ForMember(x => x.Description,
                    opt => opt.MapFrom(y => y.Description))
                .ForMember(x => x.Category,
                    opt => opt.MapFrom(y => y.Category));
        }
    }
}
