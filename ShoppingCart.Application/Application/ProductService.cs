using ShoppingCart.Application.Common.Abstractions;
using ShoppingCart.Application.Common.Models.Product;
using Newtonsoft.Json;
using ShoppingCart.Application.Common.Response;
using ShoppingCart.Application.Common.Models.Options;
using Microsoft.Extensions.Options;

namespace ShoppingCart.Application.Application
{
    public class ProductService : IProductService
    {
        private readonly HttpClient _httpClient;
        private readonly Endpoints _config;


        public ProductService(IHttpClientFactory httpClientFactory, IOptions<Endpoints> config) {
            _httpClient = httpClientFactory.CreateClient(nameof(ProductService));
            _config = config.Value;
        }

        public async Task<IEnumerable<ProductDto>> GetProductsAsync(string param)
        {
            var response = await _httpClient.GetAsync($"{_config.API["ProductAPI"]}?{param}");
            var apiContent = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<PageResponse<List<ProductDto>>>(apiContent) is var dataContent
                ? dataContent!.data
                : new List<ProductDto>();
        }

        public async Task<ProductDto> GetProductByIdAsync(Guid id)
        {
            var response = await _httpClient.GetAsync($"{_config.API["ProductAPI"]}/{id}");
            var apiContent = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Response<ProductDto>>(apiContent) is var dataContent
                ? dataContent!.data
                : new ProductDto();
        }
    }
}
