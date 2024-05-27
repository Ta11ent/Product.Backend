using RabbitMQ.Client;

namespace Consumer.Consumer
{
    internal interface IRabbitMqConsumer
    {
        void HearChannel();
    }
}
