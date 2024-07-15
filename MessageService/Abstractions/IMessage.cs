namespace MessageService.Abstractions
{
    public interface IMessage<T, O> 
        where T : class
        where O : class
    {
        O CreateMessage(T data);
    }
}
