using Identity.Application.Common.Abstractions;
using Identity.Application.Common.Models.Access;
using Identity.Application.Common.Models.Access.Login;
using Identity.Application.Common.Models.User.Get;
using Identity.Domain;
using Identity.src.Core.Application.Common.Abstractions;
using JwtAuthenticationManager.Abstractions;
using Microsoft.AspNetCore.Identity;
using System.Security;
using System.Security.Claims;


namespace Identity.Infrastructure.Services
{
    public class AccessService : IAccessService
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IUserService _userService;
        private readonly IUserTokenService _userTokenService;
        private readonly ITokenService _tokenService;
        public AccessService(
            SignInManager<AppUser> signInManager,
            IUserService userService,
            IUserTokenService userTokenService,
            ITokenService tokenService
            )
        {
            _signInManager = signInManager ?? throw new ArgumentNullException(nameof(signInManager));
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
            _userTokenService = userTokenService ?? throw new ArgumentNullException(nameof(userTokenService));
            _tokenService = tokenService ?? throw new ArgumentNullException(nameof(tokenService));
        }
        public async Task<TokenResponse> LoginUserAsync(LoginCommand command)
        {
            var user = await _userService.GetUserByNameAsync(command.UserName);
            if (!user.data.Enable)
                throw new SecurityException("Unauthorized");

            var result = await _signInManager.PasswordSignInAsync(user.data.UserName, command.Password, false, false);
            if (!result.Succeeded)
                throw new SecurityException("Unauthorized");

            var claims = GenerateClaims(user.data);

            var accessToken = _tokenService.GenerateAccessToken(claims);
            var refreshToken = _tokenService.GenerateRefreshToken();
            var expTime = _tokenService.RefreshTokenExpiryTime();

            await _userTokenService.SetUserTokenAsync(user.data.Id, nameof(AccessService), nameof(refreshToken), refreshToken, expTime);

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
            if (!user.data.Enable)
                throw new SecurityException("Unauthorized");

            var token = await _userTokenService.GetUserTokenAsync(user.data.Id, nameof(AccessService), "refreshToken");

            if (!token.isSuccess || token.data.Value != command.RefreshToken || token.data.ExpiryDate <= DateTime.Now)
                throw new SecurityException("Unauthorized");

            var accessToken = _tokenService.GenerateAccessToken(principal.Claims);
            var refreshToken = _tokenService.GenerateRefreshToken();
            var expTime = _tokenService.RefreshTokenExpiryTime();

            await _userTokenService.SetUserTokenAsync(user.data.Id, nameof(AccessService), nameof(refreshToken), refreshToken, expTime);

            return new TokenResponse(new TokenDto()
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken,
                RefreshTokenExp = expTime
            });
        }

        public async Task<bool> LogoutUserAsync(string refreshToken)
        {
            var token = await _userTokenService.GetUserTokenAsync(refreshToken);
            if (!token.isSuccess) return false;

            await _userTokenService.RemoveUserTokenAsync(token.data.Id);
            return true;
        }

        private List<Claim> GenerateClaims(UserDto user)
        {
            var claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.Name, user.UserName!));
            foreach (var role in user.Roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
                claims.Add(new Claim("Role", role));
            }

            return claims;
        }
    }
}
