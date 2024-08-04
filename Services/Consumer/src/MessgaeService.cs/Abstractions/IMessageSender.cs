namespace MessageService.Abstractions
{
    public interface IMessageSender<T> where T : class
    {
        bool Send(T data);
    }
}
