using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using ShoppingCart.Application.Common.Abstractions;
using ShoppingCart.Application.Common.Exceptions;
using ShoppingCart.Application.Common.Models.Product;
using ShoppingCart.Application.Common.Response;
using ShoppingCart.ShoppingCart.Infrastructure.Options;

namespace ShoppingCart.Infrastructure.Services
{
    public class ProductService : IProductService
    {
        private readonly HttpClient _httpClient;
        private readonly Endpoints _config;

        public ProductService(IHttpClientFactory httpClientFactory, IOptions<Endpoints> config)
        {
            _httpClient = httpClientFactory.CreateClient(nameof(ProductService));
            _config = config.Value;
        }

        public async Task<IEnumerable<ProductDto>> GetProductsAsync(string param)
        {
            string apiContent = default!;
            try
            {
                var response = await _httpClient.GetAsync($"{_config.API["ProductAPI"]}?{param}");
                apiContent = await response.Content.ReadAsStringAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            
            return JsonConvert.DeserializeObject<PageResponse<List<ProductDto>>>(apiContent) is var dataContent
                ? dataContent!.data
                : new List<ProductDto>();
        }
    }
}
