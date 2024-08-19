using AutoMapper;
using ShoppingCart.Application.Common.Mapping;

namespace ShoppingCart.Application.Common.Models.Status
{
    public class StatusDto : IMapWith<Domain.Status>
    {
        public string Status { get; set; } = string.Empty;
        public DateTime Date { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<Domain.Status, StatusDto>()
                .ForMember(x => x.Status,
                    opt => opt.MapFrom(y => y.TypeOfStatus))
                .ForMember(x => x.Date,
                    opt => opt.MapFrom(y => y.StatusDate));
        }
    }
}
