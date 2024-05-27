using Consumer.Models;
using Consumer.Recipient;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace Consumer.Consumer
{
    internal class RabbitMqConsumer : IRabbitMqConsumer, IDisposable
    {
        private IModel channel;
        private readonly IConnection _connection;
        private readonly RabbitMqConfig _config;
        private readonly ISender _sender;
        public RabbitMqConsumer(RabbitMqConfig config, IConnection connection, ISender sender)
        {
            _connection = connection ?? throw new ArgumentNullException(nameof(connection));
            _config = config;
            _sender = sender;
        }

        public void HearChannel()
        {
            channel = _connection.CreateModel();
            channel.QueueDeclare(queue: _config.Queue,
                    durable: false,
                    exclusive: false,
            autoDelete: false,
            arguments: null);

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (sender, arg) =>
            {
                var body = arg.Body.ToArray();
                _sender.SendToRecipient(Encoding.UTF8.GetString(body));
                
            };

            channel.BasicConsume(queue: _config.Queue,
                                 autoAck: true,
                                 consumer: consumer);
            Console.ReadKey();
        }
        
        public void Dispose()
        {
            channel?.Dispose();
        }

    }
}
