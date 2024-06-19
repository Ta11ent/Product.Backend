using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using ShoppingCart.Application.Common.Abstractions;
using ShoppingCart.Application.Common.Models.User;
using ShoppingCart.Application.Common.Response;
using ShoppingCart.ShoppingCart.Infrastructure.Options;

namespace ShoppingCart.Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly HttpClient _httpClient;
        private readonly Endpoints _config;
        public UserService(IHttpClientFactory httpClientFactory, IOptions<Endpoints> config)
        {
            _httpClient = httpClientFactory.CreateClient(nameof(UserService));
            _config = config.Value;
        }

        public async Task<UserDto> GetUserAsync(string Id)
        {
            var response = await _httpClient.GetAsync($"{_config.API["UserAPI"]}/{Id}");
            var apiContent = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Response<UserDto>>(apiContent) is var dataContent
                ? dataContent!.data
                : new UserDto();
        }
    }
}
