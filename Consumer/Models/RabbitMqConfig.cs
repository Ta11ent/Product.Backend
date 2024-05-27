namespace Consumer.Models
{
    public class RabbitMqConfig
    {
        public string HostName { get; init; }
        public string Queue { get; init; }
        public int Port { get; init; }
    }
}
