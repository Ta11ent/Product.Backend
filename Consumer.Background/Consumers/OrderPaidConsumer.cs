using MessageService.Abstractions;

namespace Consumer.Consumers
{
    public sealed class OrderPaidConsumer : IConsumer<OrderDetails>
    {
        private readonly ILogger<OrderPaidConsumer> _logger;
        public OrderPaidConsumer(ILogger<OrderPaidConsumer> logger) => _logger = logger;
        public async Task Consume(ConsumeContext<OrderDetails> context)
        {
            var jsonMessage = JsonConvert.SerializeObject(context.Message);
            _logger.LogInformation(jsonMessage);


            //Console.WriteLine($"OrderCreated message: {jsonMessage}");
        }
    }
}
