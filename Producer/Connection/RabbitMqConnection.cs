namespace Producer.Connection
{
    public class RabbitMqConnection : IRabbitMqConnection, IDisposable
    {
        public IConnection Connection { get; private set; }
        private readonly RabbitMqConfig _config;

        public RabbitMqConnection(IOptions<RabbitMqConfig> config) {

            _config = config.Value;
            InitializeConnection();
        }

        private void InitializeConnection()
        {
            var factory = new ConnectionFactory {
                HostName = _config.HostName
            };
            Connection = factory.CreateConnection();
        }

        public void Dispose()
        {
            Connection?.Dispose();
        }
    }
}
