using RabbitMQ.Client;

namespace RabbitMqConsumer.Connection
{
    public interface IMqConnection
    {
        IConnection Connection { get; }
    }
}
