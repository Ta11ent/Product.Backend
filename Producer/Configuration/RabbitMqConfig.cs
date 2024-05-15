namespace Producer.Configuration
{
    public class RabbitMqConfig
    {
        public string HostName { get; set; } = string.Empty;
        public string Queue { get; set; } =string.Empty;
        public int Port { get; set; }
    }
}
