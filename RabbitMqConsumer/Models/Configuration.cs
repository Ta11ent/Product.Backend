namespace RabbitMqConsumer.Models
{
    public class Configuration
    {
        public string Hostname { get; private set; } = string.Empty;
        public string Queue { get; private set; } = string.Empty;
        public int Port { get; private set;}
    }
}
