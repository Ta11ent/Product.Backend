using ShoppingCart.Application.Common.Abstractions;
using ShoppingCart.Application.Common.Models.Product;
using Newtonsoft.Json;
using ShoppingCart.Application.Common.Response;

namespace ShoppingCart.Application.Application
{
    public class ProductService : IProductService
    {
        private readonly HttpClient _httpClient;
        private readonly string getProductByIdRequest = "api/v1.0/products/";
        private readonly string getProductsRequest = "api/v1.0/products?";

        public ProductService(IHttpClientFactory httpClientFactory) =>
               _httpClient = httpClientFactory.CreateClient(nameof(ProductService));

        public async Task<IEnumerable<ProductDto>> GetProductsAsync(string param)
        {
            var response = await _httpClient.GetAsync(getProductsRequest+param);
            var apiContent = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<PageResponse<List<ProductDto>>>(apiContent) is var dataContent
                ? dataContent!.data
                : new List<ProductDto>();
        }

        public async Task<ProductDto> GetProductByIdAsync(Guid id)
        {
            var response = await _httpClient.GetAsync(getProductByIdRequest+id);
            var apiContent = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Response<ProductDto>>(apiContent) is var dataContent
                ? dataContent!.data
                : new ProductDto();
        }
    }
}
