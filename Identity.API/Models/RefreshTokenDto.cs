using AutoMapper;
using Identity.Application.Common.Models.Access;

namespace Identity.API.Models
{
    public class RefreshTokenDto : IMapWith<RefreshCommand>
    {
        public string AccessToken { get; set; } = string.Empty;
        public string? RefreshToken { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<RefreshTokenDto, RefreshCommand>()
                .ForMember(x => x.AccessToken,
                    opt => opt.MapFrom(y => y.AccessToken))
                .ForMember(x => x.RefreshToken,
                    opt => opt.MapFrom(y => y.RefreshToken));
        }
    }
}
