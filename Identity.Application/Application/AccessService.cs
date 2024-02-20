using Identity.Application.Common.Abstractions;
using Identity.Application.Common.Models.Access;
using Identity.Application.Common.Models.Access.Login;
using Identity.Application.Common.Models.User.Get;
using Identity.Domain;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace Identity.Application.Application
{
    public class AccessService : IAccessService
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IUserService _userService;
        private readonly ITokenService _tokenService;

        private List<IdentityError> _errors = new List<IdentityError>();
        public AccessService( 
            IUserService userService, SignInManager<AppUser> signInManager, ITokenService tokenService)
        {
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
            _signInManager = signInManager ?? throw new ArgumentNullException(nameof(signInManager));
            _tokenService = tokenService ?? throw new ArgumentNullException(nameof(tokenService));
        }
        public async Task<TokenResponse> LoginUserAsync(LoginCommand command)
        {
            var user = await _userService.GetUserByNameAsync(command.UserName);
            if (!user.isSuccess)
                return new TokenResponse(null!, user.errors);
            else if (!user.data.Enable)
            {
                _errors.Add(new IdentityError() { Description = $"User unauthorized", Code = "401" });
                return new TokenResponse(null!, _errors);
            }

            var result = await _signInManager.PasswordSignInAsync(user.data.UserName, command.Password, false, false);
            if (!result.Succeeded)
            {
                _errors.Add(new IdentityError() { Description = $"User disabled", Code = "403" });
                return new TokenResponse(null!, _errors);
            }

            var claims = GenerateClaims(user.data);

            var accessToken = _tokenService.GenerateAccessToken(claims);
            var refreshToken = _tokenService.GenerateRefreshToken();
            var expTime = _tokenService.RefreshTokenExpiryTime();

            await _userService.SetUserTokenAsync(user.data.Id, nameof(AccessService), nameof(refreshToken), refreshToken, expTime);

            return new TokenResponse(new TokenDto()
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken,
                RefreshTokenExp = expTime
            });
        }

        public async Task<TokenResponse> RefreshUserAsync(RefreshCommand command)
        {
            var principal = _tokenService.GetPrincipalFromExpiredToken(command.AccessToken);
            var username = principal.Identity!.Name;
            var user = await _userService.GetUserByNameAsync(username!);
            if (!user.isSuccess)
                return new TokenResponse(null!, user.errors);
            else if (!user.data.Enable)
            {
                _errors.Add(new IdentityError() { Description = $"User unauthorized", Code = "401" });
                return new TokenResponse(null!, _errors);
            }

            var token = await _userService.GetUserTokenAsync(user.data.Id, nameof(AccessService), "refreshToken");
            if(!token.isSuccess || token.data.Value != command.RefreshToken || token.data.ExpiryDate <= DateTime.Now)
            {
                _errors.Add(new IdentityError() { Description = "Invalid client request", Code = "403" });
                return new TokenResponse(null!, _errors);
            }

            var accessToken = _tokenService.GenerateAccessToken(principal.Claims);
            var refreshToken = _tokenService.GenerateRefreshToken();
            var expTime = _tokenService.RefreshTokenExpiryTime();

            await _userService.SetUserTokenAsync(user.data.Id, nameof(AccessService), nameof(refreshToken), refreshToken, expTime);

            return new TokenResponse(new TokenDto()
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken,
                RefreshTokenExp = expTime
            });
        }

        public async Task<bool> LogoutUserAsync(string refreshToken)
        {
            var token = await _userService.GetUserTokenAsync(refreshToken);
            if(!token.isSuccess) return false;

            return await _userService.RemoveUserTokenAsync(token.data.Id);
        }
        private List<Claim> GenerateClaims(UserDto user)
        {
            var claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.Name, user.UserName!));
            foreach (var role in user.Roles)
                claims.Add(new Claim(ClaimTypes.Role, role));

            return claims;
        }
    }
}
