using AutoMapper;
using Identity.Application.Common.Mapping;
using Identity.Domain;

namespace Identity.Application.Common.Models.User.Get
{
    public class UserDto : IMapWith<AppUser>
    {
        public string Id { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public bool Enable { get; set; } 
        public List<string> Roles { get; set; } = new List<string>();

        public void Mapping(Profile profile)
        {
            profile.CreateMap<AppUser, UserDto>()
                .ForMember(x => x.Id,
                    opt => opt.MapFrom(y => y.Id))
                .ForMember(x => x.UserName,
                    opt => opt.MapFrom(y => y.UserName))
                .ForMember(x => x.Email,
                    opt => opt.MapFrom(y => y.Email))
                .ForMember(x => x.Enable,
                    opt => opt.MapFrom(y => y.Enabled))
                .ForMember(x => x.Roles,
                    opt => opt.MapFrom(y => y.Roles));
        }
    }
}
