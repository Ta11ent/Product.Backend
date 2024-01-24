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

        public UserService(UserManager<AppUser> userManager, IAuthDbContext dbContext, IMapper mapper)
        { 
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(IAuthDbContext));
            _mapper = mapper;
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
                return new CreateUserResponse(null!, result.Errors);

            result = await _userManager.AddToRolesAsync(user, userCommand.Roles);
            if(!result.Succeeded)
            {
                await _userManager.DeleteAsync(user);
                return new CreateUserResponse(null!, result.Errors);
            }

            return new CreateUserResponse(new CreateUserResponseDto() { UserName = user.UserName, Id = user.Id }, null!);
        }

        public async Task<Response<string>> DisableUserAsync(string id) => await ChangeUserState(id, false);

        public async Task<Response<string>> EnableUserAsync(string id) => await ChangeUserState(id, true);

        public async Task<Response<string>> ResetPassword(ResetPasswordCommand entity)
        {
            var user = await _userManager.FindByIdAsync(entity.Id);

            if (user is null)
                return new Response<string>(string.Empty, new List<IdentityError>() {
                        new IdentityError() {
                            Description = $"User with id: {entity.Id} not found",
                            Code = "404"
                        }
                });

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            await _userManager.ResetPasswordAsync(user, token, entity.Password);

            return new Response<string>(entity.Id, null!);
        }

        public async Task<UsersResponse> GetUsersAsync() {
            var users =
                await _dbContext.AppUsers
                .ProjectTo<UserDto>(_mapper.ConfigurationProvider)
                .AsNoTracking()
                .ToListAsync();

            return new UsersResponse(users, null!);
        }

        public async Task<UserResponse> GetUserAsync(string id) {
             var user =
                 await _dbContext.AppUsers
                 .Include(x => x.AppUserRoles)
                    .ThenInclude(x => x.AppRole)
                 .Where(x => x.Id == id)
                 .ProjectTo<UserDto>(_mapper.ConfigurationProvider)
                 .AsNoTracking()
                 .FirstOrDefaultAsync();

            if (user is null)
                return new UserResponse(default!, new List<IdentityError>() {
                        new IdentityError() {
                            Description = $"User with id: {id} not found",
                            Code = "404"
                        }
                });
            return new UserResponse(user, null!);
        }

        private async Task<Response<string>> ChangeUserState(string id, bool state)
        {
            var user = await _dbContext.AppUsers.FirstOrDefaultAsync(x => x.Id == id);
            if (user is null)
                return new Response<string>(string.Empty, new List<IdentityError>() {
                        new IdentityError() {
                            Description = $"User with id: {id} not found",
                            Code = "404"
                        }
                });

            user.Enabled = state;
            await _dbContext.SaveChangesAsync();

            return new Response<string>(id, null!);
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
