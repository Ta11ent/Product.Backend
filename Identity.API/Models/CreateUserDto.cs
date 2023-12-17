using AutoMapper;
using Identity.Application.Common.Models.User.Create;

namespace Identity.API.Models
{
    public class CreateUserDto : IMapWith<CreateUserCommand>
    {
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }

    public void Mappin(Profile profile)
    {
        profile.CreateMap<CreateUserDto, CreateUserCommand>()
            .ForMember(x => x.UserName,
                opt => opt.MapFrom(y => y.Name))
            .ForMember(x => x.Email,
                opt => opt.MapFrom(y => y.Email))
            .ForMember(x => x.Password,
                opt => opt.MapFrom(y => y.Password));
    }
}
