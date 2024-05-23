namespace ShoppingCart.Application.Common.Models.User
{
    public class UserOrderDetailsDto
    {
        public UserOrderDetailsDto(string id) => UserId = id;
        public string UserId { get; init;}
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
    }
   
}
