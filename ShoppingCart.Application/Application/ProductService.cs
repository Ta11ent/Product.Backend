using ShoppingCart.Application.Common.Abstractions;
using ShoppingCart.Application.Common.Models.Product;
using Newtonsoft.Json;
using ShoppingCart.Application.Common.Response;

namespace ShoppingCart.Application.Application
{
    public class ProductService : IProductService
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly HttpClient _httpClient;

        public ProductService(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
            _httpClient = clientFactory.CreateClient("Product");
        }

        public async Task<ProductDto> GetProductByIdAsync(Guid id)
        {
            var response = await _httpClient.GetAsync($"api/v1.0/product/{id}");
            var apiContent = await response.Content.ReadAsStringAsync();
            var data = JsonConvert.DeserializeObject<Response<ProductDto>>(apiContent);
            if (data.isSuccess)
                return JsonConvert.DeserializeObject<ProductDto>(Convert.ToString(data));
            return new ProductDto();
        }

        public async Task<IEnumerable<ProductDto>> GetProductsAsync()
        {
           // var client = _clientFactory.CreateClient("Product"); // for test
            var response = await _httpClient.GetAsync($"api/v1.0/product"); //for test
            var apiContent = await response.Content.ReadAsStringAsync();
            var data = JsonConvert.DeserializeObject<PageResponse<List<ProductDto>>>(apiContent); //need to update/right now just for test
            if (data.isSuccess)
                return JsonConvert.DeserializeObject<IEnumerable<ProductDto>>(Convert.ToString(data));
            return new List<ProductDto>();
        }

        
    }
}
