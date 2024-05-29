namespace Consumer.Background.Models
{
    public class RabbitMqConfig
    {
        public string Hostname { get; init; }
        public string Queue { get; init; }
        public int Port { get;init; }
    }
}
