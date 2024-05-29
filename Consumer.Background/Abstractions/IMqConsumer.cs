namespace Consumer.Abstractions
{
    public interface IMqConsumer
    {
        IModel Channel { get; }
    }
}
