namespace Consumer.Abstractions
{
    public interface IMqConnection
    {
        public IConnection Connection { get; }
    }
}
