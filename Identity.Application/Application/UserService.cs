﻿using Identity.Application.Common.Abstractions;
using Identity.Application.Common.Models.User.Create;
using Identity.Application.Common.Models.User.Login;
using Identity.Domain;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace Identity.Application.Application
{
    public class UserService : IUserService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ITokenService _tokenService;

        //private readonly SignInManager<IdentityUser> _signInManager;
        public UserService(UserManager<AppUser> userManager, ITokenService tokenService) { 
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _tokenService = tokenService ?? throw new ArgumentNullException(nameof(_tokenService));
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
