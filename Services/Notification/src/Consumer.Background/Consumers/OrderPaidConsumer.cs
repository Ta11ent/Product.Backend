using MessageService.Abstractions;
using Newtonsoft.Json;
using System.Text;

namespace Consumer.Consumers
{
    public sealed class OrderPaidConsumer : IConsumer<OrderDetails>
    {
        private readonly ILogger<OrderPaidConsumer> logger;
        private readonly IEmailSender emailSender;
        private readonly IEmailBuilder emailBuilder;
        public OrderPaidConsumer(
            ILogger<OrderPaidConsumer> logger,
            IEmailSender emailSender,
            IEmailBuilder emailBuilder)
        {
            this.logger = logger;
            this.emailSender = emailSender;
            this.emailBuilder = emailBuilder;
        }
           
        public async Task Consume(ConsumeContext<OrderDetails> context)
        {
            var jsonMessage = JsonConvert.SerializeObject(context.Message);
            logger.LogInformation(jsonMessage);

            StringBuilder products = new();
            foreach(var item in context.Message.OrderItems)
                products.Append($"{item.Name}, Count: {item.Count}\n");

            var email = emailBuilder
                 .AddTo(context.Message.Email)
                 .AddSubject("Information about the paid bill")
                 .AddBody($"{context.Message.UserName}, Congratulations on your purchase! \n " +
                    $"{products.ToString()} \n" +
                    $"Total price: {context.Message.Price} {context.Message.Currency} \n" +
                    $"Status: {context.Message.Status}")
                 .Build();

            emailSender.Send(email);
        }
    }
}

