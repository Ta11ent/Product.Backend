using Identity.API.Models;

namespace Identity.API.Validation
{
    public class RefreshTokenValidation : AbstractValidator<RefreshTokenDto>
    {
        public RefreshTokenValidation() { 
          //  RuleFor(x => x.RefreshToken).NotEmpty();
            RuleFor(x => x.AccessToken).NotEmpty();
        }
    }
}
