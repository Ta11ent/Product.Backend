using Identity.Application.Common.Abstractions;
using Identity.Application.Common.Models.User.Create;
using Identity.Application.Common.Models.User.Login;
using Identity.Application.Common.Models.User.Common;
using Identity.Domain;
using Identity.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Identity.Application.Common.Models.User.Password;

namespace Identity.Application.Application
{
    public class UserService : IUserService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ITokenService _tokenService;
        private readonly AuthDbContext _dbContext;

        //private readonly SignInManager<IdentityUser> _signInManager;
        public UserService(UserManager<AppUser> userManager, ITokenService tokenService, AuthDbContext dbContext) { 
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _tokenService = tokenService ?? throw new ArgumentNullException(nameof(_tokenService));
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(AuthDbContext));
        }

        public async Task<CreateUserResponse> CreateUserAsync(CreateUserCommand userCommand)
        {
            var user = new AppUser()
            {
                Id = Guid.NewGuid().ToString(),
                UserName = userCommand.UserName,
                Email = userCommand.Email,
                PhoneNumber = userCommand.PhoneNumber,
                PhoneNumberConfirmed = true,
                LockoutEnabled =  false,
                Enabled = true
            };
            var result = await _userManager.CreateAsync(user, userCommand.Password);
            if(!result.Succeeded)
                return new CreateUserResponse() { Errors = result.Errors };

            result = await _userManager.AddToRolesAsync(user, userCommand.Roles);
            if(!result.Succeeded)
            {
                await _userManager.DeleteAsync(user);
                return new CreateUserResponse() { Errors = result.Errors };
            }

            return new CreateUserResponse() { UserName = user.UserName, UserId = user.Id };
        }

        public async Task<CommonResponse> DisableUserAsync(string id) => await ChangeUserState(id, false);

        public async Task<CommonResponse> EnableUserAsync(string id) => await ChangeUserState(id, true);

        private async Task<CommonResponse> ChangeUserState(string id, bool state)
        {
            var user = await _dbContext.AppUsers.FirstOrDefaultAsync(x => x.Id == id);
            if (user is null)
                return new CommonResponse() { Error = $"User with id: {id} not found" };

            user.Enabled = state;
            await _dbContext.SaveChangesAsync();

            return new CommonResponse() { Success = true };
        }

        public async Task<CommonResponse> ResetPassword(ResetPasswordCommand entity)
        {
            var user = await _dbContext.AppUsers.FirstOrDefaultAsync(x => x.Id == entity.Id);

            if (user is null)
                return new CommonResponse() { Error = $"User with id: {entity.Id} not found" };

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            await _userManager.ResetPasswordAsync(user, token, entity.Password);

            return new CommonResponse() { Success = true };
        }

        public async Task<UserLoginResponse> LoginUserAsync(UserLoginCommand user)
        {
            //var PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(new IdentityUser(), "Password");
            throw new NotImplementedException();
        }

        private List<Claim> GEnerateClaims(ref CreateUserCommand user)
        {
            var claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.Name, user.UserName));
            foreach (var role in user.Roles)
                claims.Add(new Claim(ClaimTypes.Role, role));

            return claims;
        }
    }
}
