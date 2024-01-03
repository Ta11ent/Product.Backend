using AutoMapper;
using Identity.Application.Common.Models.Access.Login;
using Identity.Application.Common.Models.User.Create;

namespace Identity.API.Models
{
    public class LoginDto : IMapWith<UserLoginCommand>
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public void Mapping (Profile profile)
        {
            profile.CreateMap<LoginDto, UserLoginCommand>()
                .ForMember(x => x.UserName,
                    opt => opt.MapFrom(y => y.UserName))
                .ForMember(x => x.Password,
                    opt => opt.MapFrom(y => y.Password));
        }
    }
}
