using ShoppingCart.Application.Common.Abstractions;
using ShoppingCart.Application.Common.Models.Product;
using Newtonsoft.Json;
using ShoppingCart.Application.Common.Response;

namespace ShoppingCart.Application.Application
{
    internal class ProductService : IProductService
    {
        private readonly IHttpClientFactory _clientFactory;

        internal ProductService(IHttpClientFactory clientFactory) => _clientFactory = clientFactory;

        public async Task<IEnumerable<ProductDto>> GetProducts()
        {
            var client = _clientFactory.CreateClient("Product"); // for test
            var response = await client.GetAsync($"api/v1.0/product"); //for test
            var apiContent = await response.Content.ReadAsStringAsync();
            var data = JsonConvert.DeserializeObject<PageResponse<List<ProductDto>>>(apiContent); //need to update/right now just for test
            if (data.isSuccess)
                return JsonConvert.DeserializeObject<IEnumerable<ProductDto>>(Convert.ToString(data));
            return new List<ProductDto>();
        }
    }
}
