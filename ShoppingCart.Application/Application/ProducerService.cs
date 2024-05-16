using Microsoft.Extensions.Options;
using ShoppingCart.Application.Common.Abstractions;
using ShoppingCart.Application.Common.Models.Options;
using System.Text;
using System.Text.Json;

namespace ShoppingCart.Application.Application
{
    public class ProducerService : IRabbitMqProducerService
    {
        private readonly HttpClient _httpClient;
        private readonly Endpoints _config;
        // private readonly IOrderReppository _repos;
        public ProducerService(IHttpClientFactory httpClientFactory, IOptions<Endpoints> config)
        {
            _httpClient = httpClientFactory.CreateClient(nameof(ProducerService));
            _config = config.Value;
        }
        public async Task SendProducerMessage(Guid Id)
        {
            //  var order = _repos.GetOrderDetailsAsync(Id);

            var dataJson = JsonSerializer.Serialize(Id);
            var body = new StringContent(dataJson, Encoding.UTF8, "application/json");

            using var httpResponseMessage = await _httpClient.PostAsync(_config.API["ProducerAPI"], body);
            httpResponseMessage.EnsureSuccessStatusCode();
        }

        
    }
}
