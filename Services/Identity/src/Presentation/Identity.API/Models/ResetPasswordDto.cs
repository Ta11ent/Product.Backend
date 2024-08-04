using AutoMapper;
using Identity.Application.Common.Models.User.Create;
using Identity.Application.Common.Models.User.Password;

namespace Identity.API.Models
{
    public class ResetPasswordDto : IMapWith<ResetPasswordCommand>
    {
        public string Password { get; set; }
        public void Mapping(Profile profile) =>
            profile.CreateMap<ResetPasswordDto, ResetPasswordCommand>()
                .ForMember(x => x.Password,
                    opt => opt.MapFrom(y => y.Password));
    }
}
