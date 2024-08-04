using AutoMapper;
using Identity.Application.Common.Mapping;
using Identity.Domain;

namespace Identity.Application.Common.Models.User.Get
{
    public class UserTokenDto : IMapWith<AppUserToken>
    {
        public string Id { get; set; } = string.Empty;
        public string UserId { get; set; } = string.Empty;
        public string LoginProvider { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Value { get; set; } = string.Empty;
        public DateTime ExpiryDate { get; set; } = DateTime.MinValue;
        public void Mapping(Profile profile)
        {
            profile.CreateMap<AppUserToken, UserTokenDto>()
                .ForMember(x => x.Id,
                    opt => opt.MapFrom(y => y.Id))
                .ForMember(x => x.UserId,
                    opt => opt.MapFrom(y => y.UserId))
                .ForMember(x => x.LoginProvider,
                    opt => opt.MapFrom(y => y.LoginProvider))
                .ForMember(x => x.Name,
                    opt => opt.MapFrom(y => y.Name))
                .ForMember(x => x.Value,
                    opt => opt.MapFrom(y => y.Value))
                 .ForMember(x => x.ExpiryDate,
                    opt => opt.MapFrom(y => y.ExpiryDate));
        }
    }
}
