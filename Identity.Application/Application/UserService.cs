using Identity.Application.Common.Abstractions;
using Identity.Application.Common.Models.User.Create;
using Identity.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Identity.Application.Common.Models.User.Password;
using Identity.Application.Common.Models.User.Get;
using AutoMapper.QueryableExtensions;
using AutoMapper;
using Identity.Application.Common.Response;

namespace Identity.Application.Application
{
    public class UserService : IUserService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IAuthDbContext _dbContext;
        private readonly IMapper _mapper;

        private bool _disposed = false;
        private List<IdentityError> _errors = new List<IdentityError>();

        public UserService(UserManager<AppUser> userManager, IAuthDbContext dbContext, IMapper mapper)
        {
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(IAuthDbContext));
            _mapper = mapper;
        }

        public async Task<CreateUserResponse> CreateUserAsync(CreateUserCommand command)
        {
            var user = new AppUser()
            {
                Id = Guid.NewGuid().ToString(),
                UserName = command.UserName,
                Email = command.Email,
                PhoneNumber = command.PhoneNumber,
                PhoneNumberConfirmed = true,
                LockoutEnabled = false,
                Enabled = true
            };
            var result = await _userManager.CreateAsync(user, command.Password);
            if (!result.Succeeded)
                return new CreateUserResponse(null!, result.Errors);

            result = await _userManager.AddToRolesAsync(user, command.Roles);
            if (!result.Succeeded)
            {
                await _userManager.DeleteAsync(user);
                return new CreateUserResponse(null!, result.Errors);
            }

            return new CreateUserResponse(new CreateUserResponseDto() { UserName = user.UserName, Id = user.Id });
        }

        public async Task<Response<string>> DisableUserAsync(string id) => await ChangeUserState(id, false);

        public async Task<Response<string>> EnableUserAsync(string id) => await ChangeUserState(id, true);

        public async Task<Response<string>> ResetPasswordAsync(ResetPasswordCommand command)
        {
            var user = await _userManager.FindByIdAsync(command.Id);

            if (user is null)
            {
                _errors.Add(new IdentityError(){ Description = $"User with id: {command.Id} not found", Code = "404"});
                return new Response<string>(string.Empty, _errors);
            }

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            await _userManager.ResetPasswordAsync(user, token, command.Password);

            return new Response<string>(command.Id);
        }

        public async Task<bool> CheckPasswordAsync(CheckPasswordCommand command) 
        {
            var user = _userManager.FindByIdAsync(command.Id).Result;
            return await _userManager.CheckPasswordAsync(user!, command.Password);
        }   
        public async Task<UsersResponse> GetUsersAsync() {
            var users =
                await _dbContext.AppUsers
                .ProjectTo<UserDto>(_mapper.ConfigurationProvider)
                .AsNoTracking()
                .ToListAsync();

            return new UsersResponse(users);
        }

        public async Task<UserResponse> GetUserByIdAsync(string id) {
             var user =
                 await _dbContext.AppUsers
                 .Include(x => x.AppUserRoles)
                    .ThenInclude(x => x.AppRole)
                 .Where(x => x.Id == id)
                 .ProjectTo<UserDto>(_mapper.ConfigurationProvider)
                 .AsNoTracking()
                 .FirstOrDefaultAsync();

            if (user is null)
            {
                _errors.Add(new IdentityError() { Description = $"User with id: {id} not found", Code = "404" });
                return new UserResponse(default!, _errors);
            }
            return new UserResponse(user);
        }

        public async Task<UserResponse> GetUserByNameAsync(string name)
        {
            var user =
                 await _dbContext.AppUsers
                 .Include(x => x.AppUserRoles)
                    .ThenInclude(x => x.AppRole)
                 .Where(x => x.UserName == name)
                 .ProjectTo<UserDto>(_mapper.ConfigurationProvider)
                 .AsNoTracking()
                 .FirstOrDefaultAsync();

            if (user is null)
            {
                _errors.Add(new IdentityError() { Description = $"User with name: {name} not found", Code = "404" });
                return new UserResponse(default!, _errors);
            }
            return new UserResponse(user);
        }
        public async Task SetUserTokenAsync(string userId, string loginProvider, string tokenName, string? tokenValue, DateTime expDate)
        {
            var userToken =
                await _dbContext.AppUserTokens
                .FirstOrDefaultAsync(x => x.UserId == userId
                    && x.LoginProvider == loginProvider
                    && x.Name == tokenName);

            if (userToken is not null)
            {
                userToken.Value = tokenValue;
                userToken.EffectiveDate = DateTime.Now;
                userToken.ExpiryDate = expDate;
            }
            else if(userToken is null) { 
                userToken = new AppUserToken()
                {
                    Id = Guid.NewGuid().ToString(),
                    UserId = userId,
                    LoginProvider = loginProvider,
                    Name = tokenName,
                    Value = tokenValue,
                    EffectiveDate = DateTime.Now,
                    ExpiryDate = expDate
                };
                await _dbContext.AppUserTokens.AddAsync(userToken);
            }
           
            await _dbContext.SaveChangesAsync();
        }

        public async Task<UserTokenResponse> GetUserTokenAsync(string userId, string loginProvider, string tokenName)
        {
            var data =
                await _dbContext.AppUserTokens
                .Where(x => x.UserId == userId && x.LoginProvider == loginProvider && x.Name == tokenName)
                .ProjectTo<UserTokenDto>(_mapper.ConfigurationProvider)
                .AsNoTracking()
                .FirstOrDefaultAsync();

            return new UserTokenResponse(data!);
        }

        public async Task<UserTokenResponse> GetUserTokenAsync(string token)
        {
            var data =
                await _dbContext.AppUserTokens
                .Where(x => x.Value == token)
                .ProjectTo<UserTokenDto>(_mapper.ConfigurationProvider)
                .AsNoTracking()
                .FirstOrDefaultAsync();

            return new UserTokenResponse(data!);
        }
        public async Task<bool> RemoveUserTokenAsync(string id)
        {
            var token = await _dbContext.AppUserTokens.FindAsync(new object[] { id });
            if (token == null) return false; 

            _dbContext.AppUserTokens.Remove(token);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        private async Task<Response<string>> ChangeUserState(string id, bool state)
        {
            var user = await _dbContext.AppUsers.FirstOrDefaultAsync(x => x.Id == id);
            if (user is null)
            {
                _errors.Add(new IdentityError() { Description = $"User with id: {id} not found", Code = "404" });
                return new Response<string>(string.Empty, _errors);
            }

            user.Enabled = state;
            await _dbContext.SaveChangesAsync();

            return new Response<string>(id);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing) _dbContext.Dispose();
            }
            _disposed = true;
        }

        ~UserService() => Dispose(false);
    }
}
