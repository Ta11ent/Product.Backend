namespace Producer.Producer
{
    public class RabbitMqMessageProducer : IRabbitMqMessageProducer
    {
        private readonly IRabbitMqConnection _connection;
        private readonly RabbitMqConfig _config;

        public RabbitMqMessageProducer(IRabbitMqConnection connection, IOptions<RabbitMqConfig> config)
        {
            _connection = connection ?? throw new ArgumentNullException(nameof(connection));
            _config = config.Value;
        }

        public void SendMessage<T>(T message)
        {
            using (var channel = _connection.Connection.CreateModel())
            {
                channel.QueueDeclare(
                    queue: _config.Queue,
                    durable: false,
                    exclusive: false,
                    autoDelete: false,
                    arguments: null
                );

                var json = JsonSerializer.Serialize(message);
                var body = Encoding.UTF8.GetBytes(json);

                channel.BasicPublish(
                   exchange: "",
                   routingKey: _config.Queue,
                   basicProperties: null,
                   body: body
               );
            }
        }
    }
}
