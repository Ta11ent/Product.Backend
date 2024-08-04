using AutoMapper;
using Identity.Application.Common.Models.Access.Login;

namespace Identity.API.Models
{
    public class LoginDto : IMapWith<LoginCommand>
    {
        public string UserName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public void Mapping (Profile profile)
        {
            profile.CreateMap<LoginDto, LoginCommand>()
                .ForMember(x => x.UserName,
                    opt => opt.MapFrom(y => y.UserName))
                .ForMember(x => x.Password,
                    opt => opt.MapFrom(y => y.Password));
        }
    }
}
