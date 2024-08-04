using ShoppingCart.Application.Common.Models.User;

namespace ShoppingCart.Application.Common.Abstractions
{
    public interface IUserService
    {
        Task<UserDto> GetUserAsync(string Id);
    }
}
