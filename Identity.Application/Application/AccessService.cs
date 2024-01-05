using Identity.Application.Common.Abstractions;
using Identity.Application.Common.Models.Access.Login;
using Identity.Domain;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace Identity.Application.Application
{
    public class AccessService : IAccessService
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly ITokenService _tokenService;
        public AccessService( 
            UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, ITokenService tokenService)
        {
            _signInManager = signInManager ?? throw new ArgumentNullException(nameof(signInManager));
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _tokenService = tokenService ?? throw new ArgumentNullException(nameof(tokenService));
        }
        public async Task<UserLoginResponse> LoginUserAsync(UserLoginCommand entity)
        {
            var user = await _userManager.FindByNameAsync(entity.UserName);
            if (user is null)
                return new UserLoginResponse(null!, new List<IdentityError>() {
                        new IdentityError() {
                            Description = $"User with name: {entity.UserName} not found",
                            Code = "404"
                        }
                });

            var succeeded = await _userManager.CheckPasswordAsync(user, entity.Password);
            if(!succeeded || !user.Enabled) 
                return new UserLoginResponse(null!, new List<IdentityError>() {
                        new IdentityError() {
                            Description = $"User unauthorized",
                            Code = "401"
                        }
                 });

            await _signInManager.PasswordSignInAsync(user, entity.Password, false, false);

            var claims = GenerateClaims(ref user);

            return new UserLoginResponse(new UserLoginDto() { 
                AccessToken = _tokenService.GenerateAccessToken(claims),
                RefreshToken = _tokenService.GenerateRefreshToken(),
                RefreshTokenExpTime = _tokenService.RefreshTokenExpiryTime()
            }, null!);

        }

        private List<Claim> GenerateClaims(ref AppUser user)
        {
            var roles = _userManager.GetRolesAsync(user).Result;
            var claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.Name, user.UserName!));
            foreach (var role in roles)
                claims.Add(new Claim(ClaimTypes.Role, role));

            return claims;
        }
    }
}
