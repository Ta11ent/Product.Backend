namespace Producer.Producer
{
    public interface IRabbitMqMessageProducer
    {
        void SendMessage<T>(T message);
    }
}
