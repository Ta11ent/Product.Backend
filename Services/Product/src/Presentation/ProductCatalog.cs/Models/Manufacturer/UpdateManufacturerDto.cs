using ProductCatalog.Application.Application.Commands.Manufacturer.UpdateManufacturer;

namespace ProductCatalog.API.Models.Manufacturer
{
    public class UpdateManufacturerDto : IMapWith<UpdateManufacturerCommand>
    {
        public Guid ManufacturerId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public void Mapping(Profile profile)
        {
            profile.CreateMap<UpdateManufacturerDto, UpdateManufacturerCommand>()
                 .ForMember(x => x.ManufacturerId,
                    opt => opt.MapFrom(y => y.ManufacturerId))
                .ForMember(x => x.Name,
                    opt => opt.MapFrom(y => y.Name))
                .ForMember(x => x.Description,
                    opt => opt.MapFrom(y => y.Description));
        }
    }
}
