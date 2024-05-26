using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMqConsumer.Models;

namespace RabbitMqConsumer.Connection
{
    public class MqConnection : IMqConnection
    {
        private readonly Configuration _options;
        public IConnection Connection { get; private set; }
        public MqConnection(IOptions<Configuration> options)
        {
            _options = options.Value ?? throw new ArgumentNullException(nameof(options));
            ConnectionInitialization();
        }

        private void ConnectionInitialization()
        {
            var factory = new ConnectionFactory
            {
                HostName = _options.Hostname,
                Port = _options.Port,
            };
            Connection = factory.CreateConnection();
        }
    }
}
