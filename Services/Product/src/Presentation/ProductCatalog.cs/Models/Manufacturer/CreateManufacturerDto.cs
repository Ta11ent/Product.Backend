using ProductCatalog.Application.Application.Commands.Manufacturer.CreateManufacturer;

namespace ProductCatalog.API.Models.Manufacturer
{
    public class CreateManufacturerDto : IMapWith<CreateManufacturerCommand>
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateManufacturerDto, CreateManufacturerCommand>()
                .ForMember(x => x.Name,
                    opt => opt.MapFrom(y => y.Name))
                .ForMember(x => x.Description,
                    opt => opt.MapFrom(y => y.Description));
        }
    }
}
