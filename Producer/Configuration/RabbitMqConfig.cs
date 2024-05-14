namespace Producer.Configuration
{
    public class RabbitMqConfig
    {
        public string HostName { get;} = string.Empty;
        public string Queue { get; } =string.Empty;
        public int Port { get; }
    }
}
