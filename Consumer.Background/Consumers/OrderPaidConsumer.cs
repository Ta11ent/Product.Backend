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

/*
 *  //  _emailtemplatePath = @"..\MessageService\Templates\email.html";
 *  
 *  
 *       //StringBuilder mailText = new();
            //using (var str = new StreamReader(_emailtemplatePath))
            //{
            //    mailText.Append(str.ReadToEnd());
            //    str.Close();
            //}

            //mailText = mailText
            //    .Replace("[User]", data.User.UserName)
            //    .Replace("[Items]", string.Join("", data.ProductRanges.Select(x => $"<li>{x.Name}</li>")))
            //    .Replace("[Price]", data.Price.ToString());

*/

