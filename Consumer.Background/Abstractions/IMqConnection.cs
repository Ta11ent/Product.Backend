namespace Consumer.Abstractions
{
    public interface IMqConnection
    {
        IConnection Connection { get; }
    }
}
