namespace Producer.Connection
{
    public interface IRabbitMqConnection
    {
        IConnection Connection { get; }
    }
}
