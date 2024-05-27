using RabbitMQ.Client;

namespace Consumer.Connection
{
    public interface IRabbitMqConnection
    {
        IConnection Connection { get; }
    }
}
