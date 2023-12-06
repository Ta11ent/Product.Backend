using ShoppingCart.Application.Common.Abstractions;
using ShoppingCart.Application.Common.Models.Product;
using Newtonsoft.Json;
using ShoppingCart.Application.Common.Response;

namespace ShoppingCart.Application.Application
{
    public class ProductService : IProductService
    {
        private readonly IHttpClientFactory _clientFactory;

        public ProductService(IHttpClientFactory httpClientFactory) =>
               _clientFactory = httpClientFactory;

        public async Task<IEnumerable<ProductDto>> GetProductsAsync(ProductQuery filter)
        {
            var httpClient = _clientFactory.CreateClient(nameof(ProductService));
            var response = await httpClient.GetAsync($"api/v1.0/product?{filter.ProductId}");
            var apiContent = await response.Content.ReadAsStringAsync();
            var dataContent = JsonConvert.DeserializeObject<PageResponse<List<ProductDto>>>(apiContent);
            if (dataContent.isSuccess)
                return dataContent.data;
            return new List<ProductDto>();
        }

        public async Task<ProductDto> GetProductByIdAsync(Guid id)
        {
            var httpClient = _clientFactory.CreateClient(nameof(ProductService));
            var response = await httpClient.GetAsync($"api/v1.0/product/{id}");
            var apiContent = await response.Content.ReadAsStringAsync();
            var dataContent = JsonConvert.DeserializeObject<Response<ProductDto>>(apiContent);
            if (dataContent.isSuccess)
                return dataContent.data;
            return new ProductDto();
        }
    }
}
