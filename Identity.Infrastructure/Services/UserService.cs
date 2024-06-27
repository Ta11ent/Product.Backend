using AutoMapper;
using AutoMapper.QueryableExtensions;
using Identity.Application.Common.Abstractions;
using Identity.Application.Common.Exceptions;
using Identity.Application.Common.Models.User.Create;
using Identity.Application.Common.Models.User.Get;
using Identity.Application.Common.Models.User.Password;
using Identity.Application.Common.Response;
using Identity.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Identity.Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;
        private readonly IAuthDbContext _dbContext;
        private bool _disposed = false;
        public UserService(UserManager<AppUser> userManager, IMapper mapper, IAuthDbContext dbContext)
        {
            _userManager = userManager;
            _mapper = mapper;
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task<bool> CheckPasswordAsync(CheckPasswordCommand command)
        {
            var user = _userManager.FindByIdAsync(command.Id).Result;
            if (user is null)
                throw new NotFoundExceptions("User", command.Id);

            return await _userManager.CheckPasswordAsync(user, command.Password);
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
                throw new AgregareExceptions(result.Errors);

            result = await _userManager.AddToRolesAsync(user, command.Roles);
            if (!result.Succeeded)
            {
                await _userManager.DeleteAsync(user);
                throw new AgregareExceptions(result.Errors);
            }

            return new CreateUserResponse(new CreateUserResponseDto() { UserName = user.UserName, Id = user.Id });
        }

        public async Task<Response<string>> DisableUserAsync(string id) => new Response<string>(await ChangeUserState(id, false));

        public async Task<Response<string>> EnableUserAsync(string id) => new Response<string>(await ChangeUserState(id, true));

        public async Task<UserResponse> GetUserByIdAsync(string id)
        {
            var user =
                 await _dbContext.AppUsers
                 .Include(x => x.AppUserRoles)
                    .ThenInclude(x => x.AppRole)
                 .Where(x => x.Id == id)
                 .ProjectTo<UserDto>(_mapper.ConfigurationProvider)
                 .FirstOrDefaultAsync();

            if (user is null)
                throw new NotFoundExceptions("User", id);

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
                throw new NotFoundExceptions("User", name);

            return new UserResponse(user);
        }

        public async Task<UsersResponse> GetUsersAsync()
        {
            var users =
              await _dbContext.AppUsers
              .ProjectTo<UserDto>(_mapper.ConfigurationProvider)
              .AsNoTracking()
              .ToListAsync();

            return new UsersResponse(users);
        }

        public async Task<Response<string>> ResetPasswordAsync(ResetPasswordCommand command)
        {
            var user = await _userManager.FindByIdAsync(command.Id);
            if (user is null)
                throw new NotFoundExceptions("User", command.Id);

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);

            var result = await _userManager.ResetPasswordAsync(user, token, command.Password);
            if (!result.Succeeded)
                throw new AgregareExceptions(result.Errors);

            return new Response<string>(command.Id);
        }
        private async Task<string> ChangeUserState(string id, bool state)
        {
           // var user = await _dbContext.AppUsers.FirstOrDefaultAsync(x => x.Id == id);
            var user = await _userManager.FindByIdAsync(id);
            if (user is null)
                throw new NotFoundExceptions("User", id);

            await _userManager.UpdateAsync(user!);
            //user.Enabled = state;
            //await _dbContext.SaveChangesAsync();

            return user.Id;
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
    }
}
