using AutoMapper;
using ProductCatalog.Application.Common.Mapping;

namespace ProductCatalog.Application.Application.Queries.ROE.GetROEDetails
{
    public class ROEDetailsDto : IMapWith<Domain.ROE>
    {
        public Guid ROEId { get; set; }
        public decimal Rate { get; set; }
        public DateTime DateFrom { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Domain.ROE, ROEDetailsDto>()
                .ForMember(x => x.ROEId,
                    opt => opt.MapFrom(y => y.ROEId))
                .ForMember(x => x.Rate,
                    opt => opt.MapFrom(y => y.Rate))
                .ForMember(x => x.DateFrom,
                    opt => opt.MapFrom(y => y.DateFrom));
        }
    }
}
