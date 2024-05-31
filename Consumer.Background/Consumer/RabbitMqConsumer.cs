using Consumer.Abstractions;
using MessageService.Abstractions;

namespace Consumer.Background.Consumer
{
    public class RabbitMqConsumer : BackgroundService, IMqConnection, IMqConsumer
    {
        public IConnection Connection { get; private set; }
        public IModel Channel { get; private set; }
        private readonly RabbitMqConfig _opt;
        private readonly ISender _sender;

        public RabbitMqConsumer(IOptions<RabbitMqConfig> opt, ISender sender)
        {
            _opt = opt.Value ?? throw new ArgumentNullException(nameof(opt));
            _sender = sender;
            DeclareConnection();
            DeclareChannel();   
        }
        private void DeclareConnection()
        {
            var factory = new ConnectionFactory
            {
                HostName = _opt.Hostname,
                Port = _opt.Port,
            };
            Connection = factory.CreateConnection();
        }

        private void DeclareChannel()
        {
            Channel = Connection.CreateModel();
            Channel.QueueDeclare(
                queue: _opt.Queue,
                durable: false,
                exclusive: false,
                autoDelete: false,
                arguments: null
                );
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            stoppingToken.ThrowIfCancellationRequested();

            var consumer = new EventingBasicConsumer(Channel);
            consumer.Received += (ch, ea) =>
            {
                var content = Encoding.UTF8.GetString(ea.Body.ToArray());

                _sender.SendMessage(content);

                Channel.BasicAck(ea.DeliveryTag, false);
            };

            Channel.BasicConsume(
                _opt.Queue,
                false, consumer
                );

            return Task.CompletedTask;
        }

        public override void Dispose()
        {
            Connection.Close();
            Channel.Close();
            base.Dispose();
        }
    }
}
