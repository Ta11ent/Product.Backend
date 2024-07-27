using AutoMapper;
using ProductCatalog.Application.Common.Mapping;

namespace ProductCatalog.Application.Application.Queries.Manufacturer.GetManufacturerDetails
{
    public class ManufacturerDetailsDto : IMapWith<Domain.Manufacturer>
    {
        public Guid ManufacturerId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public void Mapping(Profile profile)
        {
            profile.CreateMap<Domain.Manufacturer, ManufacturerDetailsDto>()
                .ForMember(x => x.ManufacturerId,
                    opt => opt.MapFrom(y => y.ManufacturerId))
                .ForMember(x => x.Name,
                    opt => opt.MapFrom(y => y.Name))
                .ForMember(x => x.Description,
                    opt => opt.MapFrom(y => y.Description));
        }
    }
}
