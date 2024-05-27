using Consumer.Models;
using RabbitMQ.Client;

namespace Consumer.Connection
{
    public class RabbitMqConnection : IRabbitMqConnection, IDisposable
    {
        public IConnection Connection { get; private set; }
        private readonly RabbitMqConfig _config;

        public RabbitMqConnection(RabbitMqConfig config)
        {
            _config = config;
            InitializeConnection();
        }

        private void InitializeConnection()
        {
            var factory = new ConnectionFactory
            {
                HostName = _config.HostName,
                Port = _config.Port,
            };
            Connection = factory.CreateConnection();
        }
        public void Dispose()
        {
            Connection?.Dispose();
        }
    }
}
